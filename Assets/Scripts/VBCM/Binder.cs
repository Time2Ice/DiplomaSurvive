using System;
using System.Collections.Generic;
using VBCM.Interfaces;
using Zenject;

// <copyright>
// <author>Sergey Orlov, sergey.orlov@ximxim.com</author>
// <author>Ivan Bondarenko, wivanw@gmail.com</author>
// </copyright>

namespace VBCM
{
    public class Binder : IBinder
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<Type, Action> _hubs = new Dictionary<Type, Action>();

        protected Binder(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        /// <inheritdoc />
        public void Bind<TEventHub>(EventSource<TEventHub> eventSource)
            where TEventHub : EventHubBase<TEventHub>
        {
            eventSource.Event += GetHubAction<TEventHub>;
        }

        /// <inheritdoc />
        public void UnBind<TEventHub>(EventSource<TEventHub> eventSource)
            where TEventHub : EventHubBase<TEventHub>
        {
            eventSource.Event -= GetHubAction<TEventHub>;
        }

        /// <inheritdoc />
        public void Bind<TEventHub>(IPublisher<TEventHub> publisher)
            where TEventHub : EventHubBase<TEventHub>
        {
            publisher.GetEventSource1().Event += GetHubAction<TEventHub>;
        }

        /// <inheritdoc />
        public void UnBind<TEventHub>(IPublisher<TEventHub> publisher)
            where TEventHub : EventHubBase<TEventHub>
        {
            publisher.GetEventSource1().Event -= GetHubAction<TEventHub>;
        }

        #region BigPublishers

        /// <inheritdoc />
        public void Bind<TEventHub1, TEventHub2>(IPublisher<TEventHub1, TEventHub2> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
        {
            publisher.GetEventSource1().Event += GetHubAction<TEventHub1>;
            publisher.GetEventSource2().Event += GetHubAction<TEventHub2>;
        }
        
        /// <inheritdoc />
        public void Bind<TEventHub1, TEventHub2, TEventHub3>(IPublisher<TEventHub1, TEventHub2, TEventHub3> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>
        {
            publisher.GetEventSource1().Event += GetHubAction<TEventHub1>;
            publisher.GetEventSource2().Event += GetHubAction<TEventHub2>;
            publisher.GetEventSource3().Event += GetHubAction<TEventHub3>;
        }
        
        /// <inheritdoc />
        public void Bind<TEventHub1, TEventHub2, TEventHub3, TEventHub4>(IPublisher<TEventHub1, TEventHub2, TEventHub3, TEventHub4> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>
            where TEventHub4 : EventHubBase<TEventHub4>
        {
            publisher.GetEventSource1().Event += GetHubAction<TEventHub1>;
            publisher.GetEventSource2().Event += GetHubAction<TEventHub2>;
            publisher.GetEventSource3().Event += GetHubAction<TEventHub3>;
            publisher.GetEventSource4().Event += GetHubAction<TEventHub4>;
        }
        
        /// <inheritdoc />
        public void Bind<TEventHub1, TEventHub2, TEventHub3, TEventHub4, TEventHub5>(IPublisher<TEventHub1, TEventHub2, TEventHub3, TEventHub4, TEventHub5> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>
            where TEventHub4 : EventHubBase<TEventHub4>
            where TEventHub5 : EventHubBase<TEventHub5>
        {
            publisher.GetEventSource1().Event += GetHubAction<TEventHub1>;
            publisher.GetEventSource2().Event += GetHubAction<TEventHub2>;
            publisher.GetEventSource3().Event += GetHubAction<TEventHub3>;
            publisher.GetEventSource4().Event += GetHubAction<TEventHub4>;
            publisher.GetEventSource5().Event += GetHubAction<TEventHub5>;
        }

        #endregion

        private void GetHubAction<TEventHub>()
            where TEventHub : EventHubBase<TEventHub>
        {
            var hubType = typeof(TEventHub);
            Action action;

            if (!_hubs.TryGetValue(hubType, out action))
            {
                var eventHub = _diContainer.Resolve<TEventHub>();
                action = eventHub.Action;
                _hubs.Add(hubType, eventHub.Action);
            }

            action.Invoke();
        }
    }
}