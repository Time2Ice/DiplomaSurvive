using System;
using System.Collections.Generic;
using System.Linq;
using Pool.Interfaces;
using Pool;
using UnityEngine;
using Zenject;

namespace Pool
{
    public class UnityEnumPool<TEnum, TComponent> : IPush<TEnum>
        where TEnum : struct, IConvertible
        where TComponent : UnityEnumPoolObject<TEnum>
    {
        private readonly Dictionary<TEnum, List<TComponent>> _objects =
            new Dictionary<TEnum, List<TComponent>>(EnumComparer);

        private readonly Factory _factory;
//        private readonly Transform _root;

        public UnityEnumPool(Factory factory /*, Transform root*/)
        {
            _factory = factory;
//            _root = root;
        }

        public void Push(UnityEnumPoolObject<TEnum> obj, Transform parent = null)
        {
            Push((TComponent) obj, parent);
        }

        public void Push(TComponent value, Transform parent)
        {
            value.OnPush();
//            value.Transform.SetParent(parent ? parent : _root, false);
            List<TComponent> list;
            if (!_objects.TryGetValue(value.Group, out list))
                list = new List<TComponent>();

            if (!list.Contains(value))
                list.Add(value);
            _objects[value.Group] = list;
        }

        public TComponent Pop(TEnum type)
        {
            return Pop(type, Vector3.zero, Quaternion.identity);
        }

        public TComponent Pop(TEnum type, Vector3 position, Quaternion rotation, Transform parentTransform = null)
        {
            List<TComponent> list;
            if (!_objects.TryGetValue(type, out list))
                list = new List<TComponent>();

            TComponent component;
            if (list.Count > 0)
            {
                component = list[0];
                list.RemoveAt(0);
//                component.Transform.position = position;
//                component.Transform.rotation = rotation;
//                component.Transform.SetParent(parentTransform ? parentTransform : _root, false);
            }
            else
            {
                component = _factory.Create(type /*, position, rotation, parentTransform ? parentTransform : _root*/);
                component.UnityPoolManager = this;
            }

            component.OnPop();
            return component;
        }

        public class Factory
        {
            private readonly DiContainer _container;
            private readonly Dictionary<TEnum, TComponent> _prefabs;
            private readonly Dictionary<TEnum, Type[]> _bindTypes;

            public Factory(TComponent[] prefabs, DiContainer container, Dictionary<TEnum, Type[]> bindTypes = null)
            {
                _bindTypes = bindTypes;
                _container = container;
                _prefabs = prefabs.ToDictionary(o => o.Type);
            }

            public TComponent Create(
                TEnum type /*, Vector3 position, Quaternion rotation, Transform parentTransform = null*/)
            {
                var component = _container.InstantiatePrefab(_prefabs[type] /*, position, rotation, parentTransform*/)
                    .GetComponent<TComponent>();
                if (_prefabs != null&&_bindTypes!=null)
                    _container.Bind(_bindTypes[type]).FromInstance(component);
                return component;
            }
        }

        private sealed class EnumEqualityComparer : IEqualityComparer<TEnum>
        {
            public bool Equals(TEnum x, TEnum y)
            {
                if (x.GetType() != y.GetType())
                    return false;

                return x.Equals(y);
            }

            public int GetHashCode(TEnum obj)
            {
                return obj.GetHashCode();
            }
        }

        public static IEqualityComparer<TEnum> EnumComparer { get; } = new EnumEqualityComparer();
    }
}