using EventAggregator;
using UnityEngine;
using VBCM.Interfaces;

namespace UiScenario
{
    public class WindowInfrastructure : IWindowInfrastructure
    {
        public IBinder Binder { get; }
        public IBinderAgg BinderAgg { get; }
        public ISubscriber Subscriber { get; }
        public Camera Camera { get; }
       
        public WindowInfrastructure(IBinder binder, IBinderAgg binderAgg, ISubscriber subscriber, Camera camera)
        {
            Binder = binder;
            BinderAgg = binderAgg;
            Subscriber = subscriber;
            Camera = camera;
           
        }
    }
}