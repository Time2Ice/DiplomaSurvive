using System;

namespace UiScenario.Fade
{
    public interface IFadeData
    {
        WindowFadeSettings Settings { get; }
        bool IsFadeOut { get; set; }
        Action OnComplete { get; set; }
        IFadeEffect Effect { get; set; }
    }
    
    public class FadeData : IFadeData
    {
        public WindowFadeSettings Settings { get; private set; }
        public bool IsFadeOut { get; set; }
        public Action OnComplete { get; set; }
        public IFadeEffect Effect { get; set; }
        
        public FadeData(WindowFadeSettings settings, bool isFadeOut, Action onComplete)
        {
            Settings = settings;
            IsFadeOut = isFadeOut;
            OnComplete = onComplete;
        }
    }
}