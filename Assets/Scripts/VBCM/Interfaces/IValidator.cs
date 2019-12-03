// <copyright>
// <author>Sergey Orlov, sergey.orlov@ximxim.com</author>
// <author>Ivan Bondarenko, wivanw@gmail.com</author>
// </copyright>

using System;
using System.Collections.Generic;

namespace VBCM.Interfaces
{
    /// <summary>
    /// Main logic validation interface
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Add unit for logic unit validation.
        /// </summary>
        void Add<TCallBackValue, TEventHub, TSendValue>(
            EventHub<TCallBackValue, TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>;

        /// <summary>
        /// Remove unit for logic unit validation.
        /// </summary>
        void Remove<TCallBackValue, TEventHub, TSendValue>(
            EventHub<TCallBackValue, TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>;
//============================================
        /// <summary>
        /// Add unit for logic unit validation.
        /// </summary>
        void Add<TEventHub, TSendValue>(
            EventHub<TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TEventHub, TSendValue>;

        /// <summary>
        /// Remove unit for logic unit validation.
        /// </summary>
        void Remove<TEventHub, TSendValue>(
            EventHub<TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TEventHub, TSendValue>;

        List<WeakReference> Remove<TEventHub>()
            where TEventHub : EventHubBase<TEventHub>;
    }
}