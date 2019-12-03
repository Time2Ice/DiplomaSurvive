namespace EventAggregator
{
    public interface IBinderAgg
    {
        void Bind(IPublisherAgg published);
        
        void Publish<TEvent>() where TEvent : EventAggHub<TEvent>;
        
        void Publish<TEvent, TValue>(TValue value) where TEvent : EventAggHub<TEvent, TValue>;

        void Publish<TEvent, TValue1, TValue2>(TValue1 value1, TValue2 value2)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>;

        void Publish<TEvent, TValue1, TValue2, TValue3>(TValue1 value1, TValue2 value2, TValue3 value3)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>;
    }
}