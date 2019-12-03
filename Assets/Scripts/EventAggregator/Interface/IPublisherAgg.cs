using System;

namespace EventAggregator
{
    public interface IPublisherAgg<in TEvent> : IPublisherAgg
        where TEvent : EventHubBase
    {
        Func<TEvent> Event1 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5, in TEvent6> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
        where TEvent6 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
        Func<TEvent6> Event6 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5, in TEvent6, in TEvent7> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
        where TEvent6 : EventHubBase
        where TEvent7 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
        Func<TEvent6> Event6 { set; }
        Func<TEvent7> Event7 { set; }
    }


    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5, in TEvent6, in TEvent7, in TEvent8> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
        where TEvent6 : EventHubBase
        where TEvent7 : EventHubBase
        where TEvent8 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
        Func<TEvent6> Event6 { set; }
        Func<TEvent7> Event7 { set; }
        Func<TEvent8> Event8 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5, in TEvent6, in TEvent7, in TEvent8, in TEvent9> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
        where TEvent6 : EventHubBase
        where TEvent7 : EventHubBase
        where TEvent8 : EventHubBase
        where TEvent9 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
        Func<TEvent6> Event6 { set; }
        Func<TEvent7> Event7 { set; }
        Func<TEvent8> Event8 { set; }
        Func<TEvent9> Event9 { set; }
    }

    public interface IPublisherAgg<in TEvent1, in TEvent2, in TEvent3, in TEvent4, in TEvent5, in TEvent6, in TEvent7, in TEvent8, in TEvent9,
        in TEvent10> : IPublisherAgg
        where TEvent1 : EventHubBase
        where TEvent2 : EventHubBase
        where TEvent3 : EventHubBase
        where TEvent4 : EventHubBase
        where TEvent5 : EventHubBase
        where TEvent6 : EventHubBase
        where TEvent7 : EventHubBase
        where TEvent8 : EventHubBase
        where TEvent9 : EventHubBase
        where TEvent10 : EventHubBase
    {
        Func<TEvent1> Event1 { set; }
        Func<TEvent2> Event2 { set; }
        Func<TEvent3> Event3 { set; }
        Func<TEvent4> Event4 { set; }
        Func<TEvent5> Event5 { set; }
        Func<TEvent6> Event6 { set; }
        Func<TEvent7> Event7 { set; }
        Func<TEvent8> Event8 { set; }
        Func<TEvent9> Event9 { set; }
        Func<TEvent10> Event10 { set; }
    }

    public interface IPublisherAgg
    {
    }
}