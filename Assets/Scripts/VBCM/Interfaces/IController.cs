using System;

// <copyright>
// <author>Sergey Orlov, sergey.orlov@ximxim.com</author>
// <author>Ivan Bondarenko, wivanw@gmail.com</author>
// </copyright>

namespace VBCM.Interfaces
{
    /// <summary>
    /// Class-manager as main logic operations provider
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Add a function of the listener from the Ui event sources
        /// </summary>
        void Add<TEventHub>(EventHubBase<TEventHub>.IHandler handler)
            where TEventHub : EventHubBase<TEventHub>;

        /// <summary>
        /// Remove a function of the listener from the Ui event sources
        /// </summary>
        void Remove<TCallBackValue, TEventHub, TSendValue>(
            EventHub<TCallBackValue, TEventHub, TSendValue>.IHandler handler)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>;

        /// <summary>
        /// Register action for late binding and calculations
        /// </summary>
        void AddCallBack<TCallBackValue, TEventHub, TSendValue>(Action<TCallBackValue> callBack)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>;

        /// <summary>
        /// Unregister action for late binding and calculations
        /// </summary>
        void RemoveCallBack<TCallBackValue, TEventHub, TSendValue>(Action<TCallBackValue> callBack)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>;


//======================================================
        /// <summary>
        /// Add a function of the listener from the Ui event sources
        /// </summary>
        void Add<TEventHub, TSendValue>(EventHub<TEventHub, TSendValue>.IHandler handler)
            where TEventHub : EventHub<TEventHub, TSendValue>;

        /// <summary>
        /// Remove a function of the listener from the Ui event sources
        /// </summary>
        void Remove<TEventHub, TSendValue>(EventHub<TEventHub, TSendValue>.IHandler handler)
            where TEventHub : EventHub<TEventHub, TSendValue>;

        /// <summary>
        /// Main Logic work function
        /// </summary>
        void DoLogicWork<TEventHub, TSendValue>(TSendValue value, Type hubType)
            where TEventHub : EventHub<TEventHub, TSendValue>;

//======================================================
        /// <summary>
        /// Add a function of the listener from the Ui event sources
        /// </summary>
        void Add<TCallBackValue, TEventHub>(EventHubCallback<TCallBackValue, TEventHub>.IHandler handler)
            where TEventHub : EventHubCallback<TCallBackValue, TEventHub>;

        /// <summary>
        /// Remove a function of the listener from the Ui event sources
        /// </summary>
        void Remove<TCallBackValue, TEventHub>(EventHubCallback<TCallBackValue, TEventHub>.IHandler handler)
            where TEventHub : EventHubCallback<TCallBackValue, TEventHub>;

        /// <summary>
        /// Main Logic work function
        /// </summary>
        void DoLogicWork<TCallBackValue, TEventHub>(EventHubCallback<TCallBackValue, TEventHub>.IView view, Type hubType)
            where TEventHub : EventHubCallback<TCallBackValue, TEventHub>;

        /// <summary>
        /// Register action for late binding and calculations
        /// </summary>
        void AddCallBack<TCallBackValue, TEventHub>(Action<TCallBackValue> callBack)
            where TEventHub : EventHubCallback<TCallBackValue, TEventHub>;

        /// <summary>
        /// Unregister action for late binding and calculations
        /// </summary>
        void RemoveCallBack<TCallBackValue, TEventHub>(Action<TCallBackValue> callBack)
            where TEventHub : EventHubCallback<TCallBackValue, TEventHub>;
    }
}