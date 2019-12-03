using System.Linq;
using Modules;
using Pool;
using UiScenario.CanvasPart;
using UiScenario.Concrete;
using UiScenario.Concrete.Data;
using UiScenario.Data;
using UiScenario.Factory;
using UnityEngine;

namespace UiScenario
{
    public class WindowBuilder : IWindowBuilder
    {
        private readonly IWindowControllerFactory _controllerFactory;
        private readonly IWindowCanvasBuilder _canvasBuilder;
        private readonly IWindowInfrastructure _windowInfrastructure;
        private readonly UnityEnumPool<WindowType, Contractor.View> _enumPool;
        private readonly ISetCanvasBounds _setCanvasBounds;

        public WindowBuilder(ISetCanvasBounds setCanvasBounds, IWindowControllerFactory windowControllerFactory,
            IWindowCanvasBuilder canvasBuilder,
            IWindowInfrastructure windowInfrastructure, UnityEnumPool<WindowType, Contractor.View> enumPool)
        {
            _controllerFactory = windowControllerFactory;
            _canvasBuilder = canvasBuilder;
            _windowInfrastructure = windowInfrastructure;
            _enumPool = enumPool;
            _setCanvasBounds = setCanvasBounds;
        }

        public IWindowController Create(WindowType type, int topWindowOrder)
        {
            var controller = _controllerFactory.CreateWindowController(type);
            var view = _enumPool.Pop(type);
            controller.Init(view);
            if (view.Transform.parent)
            {
                return controller;
            }

            var canvasConfig = view.CanvasConfig;
            var order = controller.View.Attributes.Contains(WindowAttribute.IgnoreSort)
                ? canvasConfig.OrderRange
                : topWindowOrder + canvasConfig.OrderRange;
            var canvas = _canvasBuilder.Create(order, canvasConfig);
            var canvasTransform = canvas.GetComponent<RectTransform>();
            view.Initialize(_windowInfrastructure, canvasTransform);
            //It is the view of adaptation for devices like IPhoneX
            if (view.Attributes.Contains(WindowAttribute.IphoneXAdapt))
                _setCanvasBounds.Add(view.Transform);
            return controller;
        }
    }
}