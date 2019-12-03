namespace EventAggregator
{
    public class Validator : IValidator
    {
        private readonly IEventAggregator _eventAggregator;

        public Validator(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Add<TEvent>(EventAggHubV<TEvent>.IValidated subscribed) where TEvent : EventAggHubV<TEvent>
        {
            _eventAggregator.GetEvent<TEvent>().AddValidator(subscribed);
        }

        public void Remove<TEvent>(EventAggHubV<TEvent>.IValidated subscribed) where TEvent : EventAggHubV<TEvent>
        {
            _eventAggregator.GetEvent<TEvent>().RemoveValidator(subscribed);
        }

        #region Add

        public void Add<TEvent, TValue>(EventAggHubV<TEvent, TValue>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue>
        {
            _eventAggregator.GetEvent<TEvent>().AddValidator(subscribed);
        }

        public void Add<TEvent, TValue1, TValue2>(EventAggHubV<TEvent, TValue1, TValue2>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2>
        {
            _eventAggregator.GetEvent<TEvent>().AddValidator(subscribed);
        }

        public void Add<TEvent, TValue1, TValue2, TValue3>(
            EventAggHubV<TEvent, TValue1, TValue2, TValue3>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2, TValue3>
        {
            _eventAggregator.GetEvent<TEvent>().AddValidator(subscribed);
        }

        #endregion Add

        #region Remove

        public void Remove<TEvent, TValue>(EventAggHubV<TEvent, TValue>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue>
        {
            _eventAggregator.GetEvent<TEvent>().RemoveValidator(subscribed);
        }

        public void Remove<TEvent, TValue1, TValue2>(EventAggHubV<TEvent, TValue1, TValue2>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2>
        {
            _eventAggregator.GetEvent<TEvent>().RemoveValidator(subscribed);
        }

        public void Remove<TEvent, TValue1, TValue2, TValue3>(
            EventAggHubV<TEvent, TValue1, TValue2, TValue3>.IValidated subscribed)
            where TEvent : EventAggHubV<TEvent, TValue1, TValue2, TValue3>
        {
            _eventAggregator.GetEvent<TEvent>().RemoveValidator(subscribed);
        }

        #endregion Remove
    }
}