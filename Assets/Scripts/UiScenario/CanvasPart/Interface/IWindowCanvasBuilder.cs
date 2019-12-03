using UnityEngine;

namespace UiScenario.CanvasPart
{
    public interface IWindowCanvasBuilder
    {
        Canvas Create(int topWindowOrder, IWindowCanvasConfig config);
    }
}
