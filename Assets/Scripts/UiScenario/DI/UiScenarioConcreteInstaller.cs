using System;
using System.Collections.Generic;
using System.ComponentModel;
using UiScenario.Concrete.Data;
using Pool;
using EventAggregator;
using Game;
using UiScenario.Data;
using UiScenario.Factory;
using UnityEngine;
using VBCM;
using Zenject;

namespace UiScenario.Concrete.Factory
{
    public abstract class UiScenarioConcreteInstaller : MonoInstaller<UiScenarioConcreteInstaller>,
        IWindowControllerFactory, IWindowScenarioFactory
    {
        #region Init

        [SerializeField] protected new UnityEngine.Camera camera;
        [SerializeField] protected WindowSettings windowSettings;
        [SerializeField] protected Contractor.View[] windowPrefabs;
        protected readonly Dictionary<WindowType, Type[]> _bindTypes = new Dictionary<WindowType, Type[]>();

        public override void InstallBindings()
        {
            Container.BindInstance(camera);
            Container.BindInstance(windowSettings.WindowCanvasSettings);
            Container.BindInstance(windowSettings.WindowFadeData);
            VbcmInstaller.Install(Container);
            EventAggregatorInstaller.Install(Container);
            UiScenarioInstaller.Install(Container);
            Container.BindInstance(windowPrefabs).AsSingle();
            Container.Bind<UnityEnumPool<WindowType, Contractor.View>.Factory>().AsSingle();
            Container.Bind<UnityEnumPool<WindowType, Contractor.View>>().AsSingle();
            Container.Bind<IWindowInfrastructure>().To<WindowInfrastructure>().AsSingle();
            Container.Bind(typeof(IWindowScenarioFactory), typeof(IWindowControllerFactory)).FromInstance(this)
                .AsSingle();
            //========================================
            BindViews();
            Container.BindInstance(_bindTypes);
            BindControllers();
            BindScenarios();
            BindEvents();
            Container.Bind<LobbyLoader>().AsSingle().NonLazy();

        }

        #endregion

        protected abstract void BindViews();

        protected abstract void BindControllers();

        protected abstract void BindScenarios();

        protected abstract void BindEvents();
        
        protected void BindView(WindowType type, params Type[] types)
        {
            _bindTypes.Add(type, types);
        }

        public abstract IWindowController CreateWindowController(WindowType type);

        public abstract IWindowScenario CreateWindowScenario(WindowType type);
    }
}