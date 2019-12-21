using System;
using System.Collections.Generic;
using System.Linq;
using UiScenario.Concrete.Data;
using UiScenario.Data;
using UiScenario.Factory;
using UiScenario.Fade;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UiScenario
{
    public class WindowHandler : IWindowHandler
    {
        private readonly IWindowBuilder _windowBuilder;
        private readonly IWindowScenarioFactory _scenarioFactory;
        private readonly IWindowFadeManager _windowFadeManager;

        private List<IWindowController> _windowCtrls = new List<IWindowController>();
        private readonly Stack<IWindowScenario> _scenarios = new Stack<IWindowScenario>();

        private Action _currentAction;
        private readonly Queue<Action> _actions = new Queue<Action>();

        public WindowHandler(IWindowFadeManager windowFadeManager, IWindowBuilder windowBuilder,
            IWindowScenarioFactory scenarioFactory)
        {
            _windowFadeManager = windowFadeManager;
            _scenarioFactory = scenarioFactory;
            _windowBuilder = windowBuilder;
        }

        public void OpenWindow(WindowType type, Dictionary<string, object> callData = null)
        {
            RunAction(() =>
            {
                AddScenario(type);
                if (_windowCtrls.Count > 0)
                {
                    var topWindowController = _windowCtrls[_windowCtrls.Count - 1];
                    topWindowController.Block();
                }

                var ignoredSorted = _windowCtrls.Where(c => !c.View.Attributes.Contains(WindowAttribute.IgnoreSort));
                var topWindowOrder = ignoredSorted.Sum(window => window.View.CanvasConfig.OrderRange);

                var controller = _windowBuilder.Create(type, topWindowOrder);
                controller.Closed += OnWindowClosedCallback;

                controller.Open(callData);
                controller.View.Transform.localPosition = Vector3.zero;

                if (controller.View.Attributes.Contains(WindowAttribute.Modal) && _windowCtrls.Count > 0)
                {
                    var topController = _windowCtrls.FirstOrDefault(w => w.Type == type);
                    if (topController?.View.Attributes.Contains(WindowAttribute.Modal) == true)
                    {
                        topController.Block();
                        OnWindowStateChanged(WindowActionType.Blocked, topController);
                    }

                    foreach (var ctrl in _windowCtrls.Where(c => !c.View.Attributes.Contains(WindowAttribute.Modal)))
                    {
                        ctrl.Block();
                        OnWindowStateChanged(WindowActionType.Blocked, ctrl);
                    }
                }

                if (!controller.View.Attributes.Contains(WindowAttribute.Modal))
                    MoveBottomModal(controller);
                else
                    _windowCtrls.Add(controller);

                OnWindowStateChanged(WindowActionType.Open, controller);
                ActionComplete();
                _windowFadeManager.OnUIActionDone();
                ActionComplete();
            });
        }

        private void MoveBottomModal(IWindowController controller)
        {
            if (_windowCtrls.Count > 0 && _windowCtrls[_windowCtrls.Count - 1].View.Attributes.Contains(WindowAttribute.Modal))
            {
                var modalCtrl = _windowCtrls[_windowCtrls.Count - 1];
                _windowCtrls.RemoveAt(_windowCtrls.Count - 1);
                MoveBottomModal(controller);
                _windowCtrls.Add(modalCtrl);
            }
            else
                _windowCtrls.Add(controller);
        }

        public IWindowController GetTopWindow()
        {
            if (_windowCtrls.Count > 0)
            {
                var topWindow = _windowCtrls[_windowCtrls.Count - 1];
                return topWindow;
            }

            return null;
        }

        public bool IsOpen(WindowType type)
        {
            return _windowCtrls.Any(ctrl => ctrl.Type == type);
        }

        public void CloseWindow(WindowType type)
        {
            RunAction(() => RemoveWindowInternal(type));
        }

        private void OnWindowStateChanged(WindowActionType actionType, IWindowController controller)
        {
            //            Debug.Log($"<color=green>{actionType}    {controller.GetType()}</color>");
            _scenarios.First(s => s.Type == controller.Type).OnWindowAction(actionType, controller);
        }

        private void OnWindowClosedCallback(IWindowController controller)
        {
            OnWindowStateChanged(WindowActionType.Closed, controller);
            RemoveWindowInternal(controller.Type);
        }

        private void RunAction(Action action)
        {
            if (_currentAction == null)
            {
                _currentAction = action;
                action.Invoke();
            }
            else
            {
                _actions.Enqueue(action);
            }
        }

        private void ActionComplete()
        {
            _currentAction = null;

            if (_actions.Count > 0)
                RunAction(_actions.Dequeue());
        }


        private void RemoveWindowInternal(WindowType type)
        {
            if (_windowCtrls.Count > 0)
            {
                var windowForRemoving = _windowCtrls.FirstOrDefault(w => w.Type == type);
                if (windowForRemoving != null)
                {
                    _windowCtrls.Remove(windowForRemoving);
                    windowForRemoving.Closed -= OnWindowClosedCallback;

                    if (_windowCtrls.Count > 0)
                    {
                        var topWindowController = _windowCtrls[_windowCtrls.Count - 1];
                        if (topWindowController.View.Attributes.Contains(WindowAttribute.Modal))
                        {
                            topWindowController.Unblock();
                            OnWindowStateChanged(WindowActionType.Unblocked, topWindowController);
                        }

                        if (_windowCtrls.All(c => !c.View.Attributes.Contains(WindowAttribute.Modal)))
                            foreach (var ctrl in _windowCtrls)
                            {
                                ctrl.Unblock();
                                OnWindowStateChanged(WindowActionType.Unblocked, ctrl);
                            }
                    }

                    windowForRemoving.OnClose();
                }
            }

            ActionComplete();
        }
        private void RemoveWindowInternalPeek(WindowType type)
        {
            if (_windowCtrls.Count > 0)
            {
                var windowForRemoving = _windowCtrls[_windowCtrls.Count - 1];
                var windowForRemovingType = windowForRemoving.Type;
                if (windowForRemovingType == type)
                {
                    _windowCtrls.RemoveAt(_windowCtrls.Count - 1);
                    windowForRemoving.Closed -= OnWindowClosedCallback;

                    if (_windowCtrls.Count > 0)
                    {
                        var topWindowController = _windowCtrls[_windowCtrls.Count - 1];
                        if (topWindowController.View.Attributes.Contains(WindowAttribute.Modal))
                        {
                            topWindowController.Unblock();
                            OnWindowStateChanged(WindowActionType.Unblocked, topWindowController);
                        }

                        if (_windowCtrls.All(c => !c.View.Attributes.Contains(WindowAttribute.Modal)))
                            foreach (var ctrl in _windowCtrls)
                            {
                                ctrl.Unblock();
                                OnWindowStateChanged(WindowActionType.Unblocked, ctrl);
                            }
                    }

                    windowForRemoving.OnClose();
                }
            }

            ActionComplete();
        }

        #region Scenario

        private void OnScenarioCompleted(WindowType nextScenarioType)
        {
            RunAction(() =>
            {
                _scenarios.Pop();
                if (_scenarios.Count > 0)
                {
                    var scenario = _scenarios.Peek();
                    scenario.Resume();
                    SubscribeScenario(scenario);
                }
                else
                {
                    AddScenario(nextScenarioType);
                }

                ActionComplete();
            });
        }

        private void FadeIn(WindowFadeSettings settings)
        {
            RunAction(() => _windowFadeManager.RunFadeIn(settings, ActionComplete));
        }

        private void FadeOut(WindowFadeSettings settings)
        {
            RunAction(() => _windowFadeManager.RunFadeOut(settings, ActionComplete));
        }

        private void AddScenario(WindowType type, Dictionary<string, object> callData = null)
        {
            var scenario = _scenarioFactory.CreateWindowScenario(type);
            _scenarios.Push(scenario);
            SubscribeScenario(scenario);
            scenario.Execute(callData);
        }

        private void SubscribeScenario(IWindowScenario scenario)
        {
            scenario.Completed += OnScenarioCompleted;
            scenario.FadeIn += FadeIn;
            scenario.FadeOut += FadeOut;
        }

        #endregion
    }
}