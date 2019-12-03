using UiScenario.Fade.Effects;
using UnityEngine;

namespace UiScenario.Fade
{
    public class WindowFadeCanvasMaskEffectBuilder : IFadeEffectBuilder
    {

        private readonly Shader _shader;
        private readonly GameObject _canvas;
        
        public WindowFadeCanvasMaskEffectBuilder(Shader shader, GameObject canvas)
        {
            _shader = shader;
            _canvas = canvas;
        }
        
        public Shader GetShader()
        {
            return _shader;
        }

        public IFadeEffect Build()
        {
            var go = GameObject.Instantiate(_canvas);
            return go.AddComponent<WindowFadeMask>();
        }
    }
}