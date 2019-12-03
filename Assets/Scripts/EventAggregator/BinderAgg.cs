using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventAggregator
{
    public class BinderAgg : IBinderAgg
    {
        private readonly IEventAggregator _eventAggregator;
        private static readonly Type EventAggregatorType = typeof(EventAggregator);
        private static readonly Type PublishedType = typeof(IPublisherAgg);

        public BinderAgg(IEventAggregator eventAggregator, List<IPublisherAgg> publisheds)
        {
            _eventAggregator = eventAggregator;
            foreach (var published in publisheds)
                Bind(published);
        }

        public void Publish<TEvent>() where TEvent : EventAggHub<TEvent>
        {
            _eventAggregator.GetEvent<TEvent>().Publish();
        }

        public void Publish<TEvent, TValue>(TValue value) where TEvent : EventAggHub<TEvent, TValue>
        {
            _eventAggregator.GetEvent<TEvent>().Publish(value);
        }

        public void Publish<TEvent, TValue1, TValue2>(TValue1 value1, TValue2 value2)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>
        {
            _eventAggregator.GetEvent<TEvent>().Publish(value1, value2);
        }

        public void Publish<TEvent, TValue1, TValue2, TValue3>(TValue1 value1, TValue2 value2, TValue3 value3)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>
        {
            _eventAggregator.GetEvent<TEvent>().Publish(value1, value2, value3);
        }
        
        #region Bind

        public void Bind(IPublisherAgg published)
        {
            var type = published.GetType();
            var properties = EventProperties(type);
            var metod = EventAggregatorType.GetMethod("GetEvent", BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                var returnType = propertyType.GetGenericArguments()[0];
                var eventResolve = metod.MakeGenericMethod(returnType);
                var del = Delegate.CreateDelegate(propertyType, _eventAggregator, eventResolve);
                propertyInfo.SetValue(published, del);
            }
        }

        private static IEnumerable<PropertyInfo> EventProperties(Type type)
        {
            var types = type.GetInterfaces().Where(i => i.IsGenericType && PublishedType.IsAssignableFrom(i));
            var list = new List<PropertyInfo>();
            foreach (var @interface in types)
            {
                var count = @interface.GenericTypeArguments.Length;
                for (var i = 1; i <= count; i++)
                    list.Add(@interface.GetProperty($"Event{i}"));
            }

            return list;
        }
        
        #endregion
    }
}