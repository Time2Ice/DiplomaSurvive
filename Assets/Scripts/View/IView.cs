using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.View
{
    public interface IView<out TEnum> : IView where TEnum : struct, IConvertible
    {
        TEnum Type { get; }
    }

    public interface IView
    {
        Transform Transform { get; }
        TComponent GetComponent<TComponent>(string componentId) where TComponent : Component;
    }
}