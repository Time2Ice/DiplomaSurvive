namespace EventAggregator
{
    public interface IValidator
    {
        void Add<TEvent>(EventAggHubV<TEvent>.IValidated subscribed) where TEvent : EventAggHubV<TEvent>;

        void Remove<TEvent>(EventAggHubV<TEvent>.IValidated subscribed) where TEvent : EventAggHubV<TEvent>;

        void Add<TEvent, TValue>(EventAggHubV<TEvent, TValue>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue>;

        void Add<TEvent, TValue1, TValue2>(EventAggHubV<TEvent, TValue1, TValue2>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2>;

        void Add<TEvent, TValue1, TValue2, TValue3>(
            EventAggHubV<TEvent, TValue1, TValue2, TValue3>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2, TValue3>;

        void Remove<TEvent, TValue>(EventAggHubV<TEvent, TValue>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue>;

        void Remove<TEvent, TValue1, TValue2>(EventAggHubV<TEvent, TValue1, TValue2>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2>;

        void Remove<TEvent, TValue1, TValue2, TValue3>(
            EventAggHubV<TEvent, TValue1, TValue2, TValue3>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2, TValue3>;
    }
}