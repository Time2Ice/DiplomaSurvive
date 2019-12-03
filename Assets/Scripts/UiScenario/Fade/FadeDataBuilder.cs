using System;

namespace UiScenario.Fade
{
    public class FadeDataBuilder : IFadeDataBuilder
    {
        public IFadeData BuildFadeIn(WindowFadeSettings settings, Action onComplete)
        {
            return new FadeData(settings, false, onComplete);
        }

        public IFadeData BuildFadeOut(WindowFadeSettings settings, Action onComplete)
        {
            return new FadeData(settings, true, onComplete);
        }
    }
}