namespace EventAggregator
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : EventHubBase;
    }
}