using System;

namespace UiScenario.Fade
{
    public class FadeEventArgs : EventArgs
    {
        public WindowFadeSettings Settings { get; }
        public bool IsFadeIn { get; }
        
        public FadeEventArgs(WindowFadeSettings settings, bool isFadeIn)
        {
            IsFadeIn = isFadeIn;
            Settings = settings;
        }
    }
}