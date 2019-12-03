using System;
using Core.View;
using Pool;
using Pool.Interfaces;
using UnityEngine;

namespace Pool
{
    [Serializable]
    public abstract class UnityEnumPoolObject<TEnum> : View<TEnum>, IPoolObject<TEnum>
        where TEnum : struct, IConvertible
    {
        public virtual TEnum Group => Type;

        public IPush<TEnum> UnityPoolManager { protected get; set; }

        public virtual void OnPop() // Constructor for the pool
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPush() // Destructor for the pool
        {
            gameObject.SetActive(false);
        }

        /// <inheritdoc />
        public virtual void Push()
        {
            UnityPoolManager.Push(this);
        }
    }
}