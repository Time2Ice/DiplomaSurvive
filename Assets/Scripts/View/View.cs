using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.View
{
    public abstract class View<TEnum> : MonoBehaviour, IView<TEnum>
        where TEnum : struct, IConvertible
    {
        [SerializeField] private TEnum _type;
        private Transform _myTransform;
        public Transform Transform => _myTransform ? _myTransform : _myTransform = GetComponent<Transform>();
        public TEnum Type => _type;
        public event Action<string, object> ViewEvent;

        [SerializeField] private ComponentContainer[] _componentContainers;
        public Dictionary<string, Component> Components { get; private set; }

        public TComponent GetComponent<TComponent>(string componentId) where TComponent : Component
        {
            return Components[componentId] as TComponent;
        }

        protected virtual void Awake()
        {
            Components = _componentContainers.ToDictionary(c => c.ComponentId, c => c.Component);
        }

        protected void OnViewEvent(string eventName, object obj)
        {
            ViewEvent?.Invoke(eventName, obj);
        }
    }
}