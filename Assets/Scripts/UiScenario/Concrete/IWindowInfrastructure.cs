using EventAggregator;
using UnityEngine;
using VBCM.Interfaces;

namespace UiScenario
{
    public interface IWindowInfrastructure
    {
        IBinder Binder { get; }
        IBinderAgg BinderAgg { get; }
        ISubscriber Subscriber { get; }
        Camera Camera { get; }
       
    }
}