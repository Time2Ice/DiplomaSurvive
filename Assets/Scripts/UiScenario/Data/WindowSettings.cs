using UiScenario.CanvasPart;
using UiScenario.Concrete.Factory;
using UiScenario.Fade;
using UnityEngine;

namespace UiScenario.Data
{
    [CreateAssetMenu(menuName = "Assets/Create/WindowSettings", fileName = "WindowSettings")]
    public class WindowSettings : ScriptableObject
    {
        [SerializeField] private WindowFadeData _windowFadeData;
        [SerializeField] private WindowCanvasBuilder.Settings _windowCanvasSettings;

        public WindowFadeData WindowFadeData => _windowFadeData;
        public WindowCanvasBuilder.Settings WindowCanvasSettings => _windowCanvasSettings;
    }
}