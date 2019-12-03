using UiScenario.CanvasPart;
using UiScenario.Concrete;
using UiScenario.Concrete.Data;
using UiScenario.Data;
using UnityEngine;

namespace UiScenario
{
    public interface IWindowView
    {
        RectTransform Transform { get; }
        WindowType Type { get; }
        WindowAttribute[] Attributes { get; }
        GameObject GameObject { get; }
        IWindowCanvasConfig CanvasConfig { get; }

        void Initialize(IWindowInfrastructure infrastructure, RectTransform parent);
        void Block();
        void Unblock();

        /// <summary>
        /// Called by IWindowController when View is being destroyed.
        /// This is opposite to Initialize method
        /// </summary>
        void Destroy();
    }
}