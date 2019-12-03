// <copyright>
// <author>Sergey Orlov, sergey.orlov@ximxim.com</author>
// <author>Ivan Bondarenko, wivanw@gmail.com</author>
// </copyright>

namespace VBCM.Interfaces
{
    /// <summary>
    /// Translate logic to IController part
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Lazy subscription to the event current EventHub
        /// </summary>
        void Bind<TEventHub>(EventSource<TEventHub> eventSource)
            where TEventHub : EventHubBase<TEventHub>;

        /// <summary>
        /// Lazy unsubscription to the event current EventHub
        /// </summary>
        void UnBind<TEventHub>(EventSource<TEventHub> eventSource)
            where TEventHub : EventHubBase<TEventHub>;

        /// <summary>
        /// Lazy subscription to the event current EventHub
        /// </summary>
        void Bind<TEventHub>(IPublisher<TEventHub> publisher)
            where TEventHub : EventHubBase<TEventHub>;

        /// <summary>
        /// Lazy unsubscription to the event current EventHub
        /// </summary>
        void UnBind<TEventHub>(IPublisher<TEventHub> publisher)
            where TEventHub : EventHubBase<TEventHub>;

        #region BigPublishers

        /// <summary>
        /// Lazy subscription to the event current EventHub
        /// </summary>
        void Bind<TEventHub1, TEventHub2>(IPublisher<TEventHub1, TEventHub2> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>;

        void Bind<TEventHub1, TEventHub2, TEventHub3>(IPublisher<TEventHub1, TEventHub2, TEventHub3> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>;

        void Bind<TEventHub1, TEventHub2, TEventHub3, TEventHub4>(
            IPublisher<TEventHub1, TEventHub2, TEventHub3, TEventHub4> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>
            where TEventHub4 : EventHubBase<TEventHub4>;

        void Bind<TEventHub1, TEventHub2, TEventHub3, TEventHub4, TEventHub5>(
            IPublisher<TEventHub1, TEventHub2, TEventHub3, TEventHub4, TEventHub5> publisher)
            where TEventHub1 : EventHubBase<TEventHub1>
            where TEventHub2 : EventHubBase<TEventHub2>
            where TEventHub3 : EventHubBase<TEventHub3>
            where TEventHub4 : EventHubBase<TEventHub4>
            where TEventHub5 : EventHubBase<TEventHub5>;

        #endregion
    }
}