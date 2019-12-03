using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UiScenario.CanvasPart
{    
    public class WindowCanvasBuilder : IWindowCanvasBuilder
    {
        private readonly IWindowCanvasConfigurator _canvasConfigurator;
        private readonly Settings _settings;

        public WindowCanvasBuilder(IWindowCanvasConfigurator canvasConfigurator, Settings settings)
        {
            _canvasConfigurator = canvasConfigurator;
            _settings = settings;       
        }

        public Canvas Create(int topWindowOrder, IWindowCanvasConfig config)
        {
            var canvas = Object.Instantiate(_settings.DefualtCanvas);
            canvas.transform.localPosition = Vector3.zero;
            canvas.transform.localScale = Vector3.one;
            canvas.sortingOrder = topWindowOrder + 1;
            _canvasConfigurator.UpdateCanvasSettings(canvas, config);
            return canvas;
        }

        [Serializable]
        public class Settings
        {
            public Canvas DefualtCanvas;
        }
    }
}
