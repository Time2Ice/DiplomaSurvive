using System;
using System.Collections.Generic;
using UiScenario.Concrete.Data;
using Pool;
using UiScenario.CanvasPart;
using UiScenario.Data;
using UiScenario.Fade;
using UnityEngine;
using VBCM;

namespace UiScenario
{
    public abstract class Contractor
    {
        public abstract class View : UnityEnumPoolObject<WindowType>, IWindowView
        {
            [SerializeField] private WindowAttribute[] _attributes = {WindowAttribute.None};
            public WindowAttribute[] Attributes => _attributes;

            public new RectTransform Transform => !_transform.IsNull()
                ? _transform
                : _transform = GameObject.GetComponent<RectTransform>();
            private RectTransform _transform;

            public GameObject GameObject => gameObject;
            public IWindowCanvasConfig CanvasConfig => _canvasConfig;
            public IWindowInfrastructure Infrastructure { get; private set; }
            [SerializeField] private WindowCanvasConfig _canvasConfig;

            public virtual void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
            {
                Infrastructure = infrastructure;
                var localScale = Transform.localScale;
                var localPosition = Transform.localPosition;
                var offsetMin = Transform.offsetMin;
                var offsetMax = Transform.offsetMax;

                Transform.SetParent(parent);
                Transform.localScale = localScale;
                Transform.localPosition = localPosition;

                Transform.offsetMin = offsetMin;
                Transform.offsetMax = offsetMax;
            }

            public virtual void Destroy()
            {
            }

            public virtual void Block()
            {
            }

            public virtual void Unblock()
            {
            }

            public override void Push()
            {
                UnityPoolManager.Push(this, Transform.parent);
            }
        }

        public abstract class Controller<TView> : IWindowController  where TView : View
        {
            public event Action<IWindowController> Closed;
            public TView ConcreteView;
            public IWindowView View { get; private set; }
            public abstract WindowType Type { get; }

            public void Init(IWindowView view)
            {
                View = view;
                ConcreteView = (TView) View;
            }

            public virtual void Open(Dictionary<string, object> callData)
            {
            }

            public virtual void OnClose()
            {
                ConcreteView.Push();
                Closed?.Invoke(this);
            }

            public virtual void Block()
            {
                View.Block();
            }

            public virtual void Unblock()
            {
                View.Unblock();
            }
        }

        public abstract class Scenario : IWindowScenario
        {
            public event Action<WindowType> Completed;
            public event Action<WindowFadeSettings> FadeIn;
            public event Action<WindowFadeSettings> FadeOut;

            public WindowState State { get; private set; }
            public abstract WindowType Type { get; }

            protected readonly IWindowHandler WindowHandler;

            protected Scenario(IWindowHandler windowHandler)
            {
                WindowHandler = windowHandler;
            }
            
            protected void AddWindow(WindowType type, Dictionary<string, object> callData = null)
            {
                WindowHandler.OpenWindow(type, callData);
            }

            protected void OnFadeIn(WindowFadeSettings settings)
            {
                FadeIn?.Invoke(settings);
            }

            protected void OnFadeOut(WindowFadeSettings settings)
            {
                FadeOut?.Invoke(settings);
            }

            protected void Complete(WindowType nextScenarioType = WindowType.Undefenit)
            {
                UpdateState(WindowState.Completed);
                Completed?.Invoke(nextScenarioType);
            }

            private void UpdateState(WindowState state)
            {
                State = state;
            }

            public virtual void Execute(Dictionary<string, object> callData = null)
            {
            }

            public void Resume()
            {
                UpdateState(WindowState.InProgress);
            }

            public void OnWindowAction(WindowActionType actionType, IWindowController controller)
            {
                switch (actionType)
                {
                    case WindowActionType.Undefined:
                        break;
                    case WindowActionType.Open:
                        UpdateState(WindowState.InProgress);
                        break;
                    case WindowActionType.Blocked:
                        UpdateState(WindowState.Paused);
                        break;
                    case WindowActionType.Unblocked:
                        UpdateState(WindowState.InProgress);
                        break;
                    case WindowActionType.Closed:
                        UpdateState(WindowState.Completed);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
                }
            }
        }
    }
}