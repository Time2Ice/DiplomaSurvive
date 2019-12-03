namespace EventAggregator
{
    /// <summary>
    /// Collecting knowledge from clever people ...
    /// </summary>
    public sealed class Subscriber : ISubscriber
    {
        private readonly IEventAggregator _eventAggregator;

        public Subscriber(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        
        public void Subscribe<TEvent>(EventAggHub<TEvent>.ISubscribed subscribed) where TEvent : EventAggHub<TEvent>
        {
            _eventAggregator.GetEvent<TEvent>().Listen(subscribed);
        }
        
        public void UnSubscribe<TEvent>(EventAggHub<TEvent>.ISubscribed subscribed) where TEvent : EventAggHub<TEvent>
        {
            _eventAggregator.GetEvent<TEvent>().UnListen(subscribed);
        }
        
        #region Subscribe

        public void Subscribe<TEvent, TValue>(EventAggHub<TEvent, TValue>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue>
        {
            _eventAggregator.GetEvent<TEvent>().Listen(subscribed);
        }

        public void Subscribe<TEvent, TValue1, TValue2>(EventAggHub<TEvent,TValue1, TValue2>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>
        {
            _eventAggregator.GetEvent<TEvent>().Listen(subscribed);
        }

        public void Subscribe<TEvent, TValue1, TValue2, TValue3>(
            EventAggHub<TEvent,TValue1, TValue2, TValue3>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>
        {
            _eventAggregator.GetEvent<TEvent>().Listen(subscribed);
        }

        #endregion Subscribe

        #region UnSubscribe

        public void UnSubscribe<TEvent, TValue>(EventAggHub<TEvent,TValue>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue>
        {
            _eventAggregator.GetEvent<TEvent>().UnListen(subscribed);
        }

        public void UnSubscribe<TEvent, TValue1, TValue2>(EventAggHub<TEvent,TValue1, TValue2>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2>
        {
            _eventAggregator.GetEvent<TEvent>().UnListen(subscribed);
        }

        public void UnSubscribe<TEvent, TValue1, TValue2, TValue3>(
            EventAggHub<TEvent,TValue1, TValue2, TValue3>.ISubscribed subscribed)
            where TEvent : EventAggHub<TEvent, TValue1, TValue2, TValue3>
        {
            _eventAggregator.GetEvent<TEvent>().UnListen(subscribed);
        }

        #endregion UnSubscribe
    }
}