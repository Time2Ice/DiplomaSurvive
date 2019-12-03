using System;
using System.Collections.Generic;
using Zenject;

namespace EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private readonly DiContainer _container;

        private readonly IDictionary<Type, EventHubBase> _eventPool =
            new Dictionary<Type, EventHubBase>(TypeComparer);

        public EventAggregator(DiContainer container)
        {
            _container = container;
        }

        public TEvent GetEvent<TEvent>() where TEvent : EventHubBase
        {
            EventHubBase eventAggr;
            var type = typeof(TEvent);
            var isExist = _eventPool.TryGetValue(type, out eventAggr);
            if (isExist)
                return (TEvent) eventAggr;

            var eventHub = _container.Resolve<TEvent>();
            _eventPool.Add(type, eventHub);
            return eventHub;
        }
        
        private sealed class TypeEqualityComparer : EqualityComparer<Type>
        {
            public override bool Equals(Type x, Type y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                return y == x;
            }

            public override int GetHashCode(Type type)
            {
                return type != null ? type.GetHashCode() : 0;
            }
        }

        public static EqualityComparer<Type> TypeComparer { get; } = new TypeEqualityComparer();
    }
}