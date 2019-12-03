using System;
using UnityEngine;

namespace Core.View
{
    [Serializable]
    public struct ComponentContainer
    {
        public string ComponentId;
        public Component Component;
    }
}