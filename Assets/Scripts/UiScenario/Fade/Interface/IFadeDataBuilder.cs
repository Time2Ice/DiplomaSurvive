using System;

namespace UiScenario.Fade
{
    public interface IFadeDataBuilder
    {
        IFadeData BuildFadeIn(WindowFadeSettings settings, Action onComplete);
        IFadeData BuildFadeOut(WindowFadeSettings settings, Action onComplete);
    }
}