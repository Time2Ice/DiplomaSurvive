namespace EventAggregator
{
    public interface ISubscriber
    {
//        void Subscribe(object subscriber);
        void Subscribe<TEvent>(EventAggHub<TEvent>.ISubscribed subscribed) where TEvent : EventAggHub<TEvent>;

        void Subscribe<TEvent, TValue>(EventAggHub<TEvent, TValue>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue>;

        void Subscribe<TEvent, TValue1, TValue2>(EventAggHub<TEvent, TValue1, TValue2>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>;

        void Subscribe<TEvent, TValue1, TValue2, TValue3>(
            EventAggHub<TEvent, TValue1, TValue2, TValue3>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>;

//        void UnSubscribe(object subscriber);
        void UnSubscribe<TEvent>(EventAggHub<TEvent>.ISubscribed subscribed) where TEvent : EventAggHub<TEvent>;

        void UnSubscribe<TEvent, TValue>(EventAggHub<TEvent, TValue>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue>;

        void UnSubscribe<TEvent, TValue1, TValue2>(EventAggHub<TEvent, TValue1, TValue2>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>;

        void UnSubscribe<TEvent, TValue1, TValue2, TValue3>(
            EventAggHub<TEvent, TValue1, TValue2, TValue3>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>;
    }
}