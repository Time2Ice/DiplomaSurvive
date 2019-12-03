using System;

namespace UiScenario.Fade
{
    
    public interface IWindowFadeManager
    {
        event EventHandler<FadeEventArgs> FadeStarted; 
        event EventHandler<FadeEventArgs> FadeCompleted; 
        
        void RunFadeIn(WindowFadeSettings settings, Action onComplete);
        void RunFadeOut(WindowFadeSettings settings, Action onComplete);
        void OnUIActionDone();
    }
}