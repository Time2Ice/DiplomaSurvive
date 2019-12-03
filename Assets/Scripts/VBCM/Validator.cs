using System;
using System.Collections.Generic;
using VBCM;
using VBCM.Interfaces;

// <copyright>
// <author>Sergey Orlov, sergey.orlov@ximxim.com</author>
// <author>Ivan Bondarenko, wivanw@gmail.com</author>
// </copyright>

namespace VBCM
{
    public sealed class Validator : IValidator
    {
        private readonly IDictionary<Type, List<WeakReference>> _validates = new Dictionary<Type, List<WeakReference>>();

        /// <inheritdoc />
        public void Add<TCallBackValue, TEventHub, TSendValue>(
            EventHub<TCallBackValue, TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>
        {
            var type = typeof(TEventHub);
            GetValidatesList<TEventHub>(type).Add(new WeakReference(validate));
        }

        /// <inheritdoc />
        public void Remove<TCallBackValue, TEventHub, TSendValue>(
            EventHub<TCallBackValue, TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TCallBackValue, TEventHub, TSendValue>
        {
            var type = typeof(TEventHub);
            _validates.GetList<TEventHub>(type).RemoveAll(obj => obj.IsAlive && obj.Target == validate);
        }

//============================================
        /// <inheritdoc />
        public void Add<TEventHub, TSendValue>(EventHub<TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TEventHub, TSendValue>
        {
            var type = typeof(TEventHub);
            GetValidatesList<TEventHub>(type).Add(new WeakReference(validate));
        }

        /// <inheritdoc />
        public void Remove<TEventHub, TSendValue>(EventHub<TEventHub, TSendValue>.IValidate validate)
            where TEventHub : EventHub<TEventHub, TSendValue>
        {
            var type = typeof(TEventHub);
            GetValidatesList<TEventHub>(type).RemoveAll(obj => obj.IsAlive && obj.Target == validate);
        }

        public List<WeakReference> Remove<TEventHub>() where TEventHub : EventHubBase<TEventHub>
        {
            var type = typeof(TEventHub);
            if (!_validates.ContainsKey(type))
                return null;
            
            var result = GetValidatesList<TEventHub>(type);
            _validates.Remove(type);
            return result;
        }

        private List<WeakReference> GetValidatesList<TEventHub>(Type type)
            where TEventHub : EventHubBase<TEventHub>
        {
            List<WeakReference> list;
            var isExist = _validates.TryGetValue(type, out list);
            if (isExist)
                return list;

            list = new List<WeakReference>();
            _validates.Add(type, list);
            return list;
        }
    }
}