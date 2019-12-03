using UnityEngine;

namespace UiScenario.Fade
{
    public interface IFadeEffectBuilder
    {
        Shader GetShader();
        IFadeEffect Build();
    }
}