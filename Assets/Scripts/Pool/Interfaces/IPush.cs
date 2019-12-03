using System;
using Pool;
using UnityEngine;

namespace Pool
{
    public interface IPush<TEnum>
        where TEnum : struct, IConvertible
    {
        void Push(UnityEnumPoolObject<TEnum> obj, Transform parent = null);
    }

    public interface IPush
    {
        void Push<T>(T obj, Transform parent = null) where T : UnityPoolObject;
    }
}