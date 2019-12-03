using UiScenario.Fade.Effects;
using UnityEngine;

namespace UiScenario.Fade
{
    public class WindowFadeImageEffectBuilder : IFadeEffectBuilder
    {
        private readonly Shader _shader;
        
        public WindowFadeImageEffectBuilder(WindowFadeData shader)
        {
            _shader = shader.ShaderImageEffect;
        }

        public Shader GetShader()
        {
            return _shader;
        }

        public IFadeEffect Build()
        {
            return UnityEngine.Camera.main.gameObject.AddComponent<WindowFadeEffect>();
        }
    }
}