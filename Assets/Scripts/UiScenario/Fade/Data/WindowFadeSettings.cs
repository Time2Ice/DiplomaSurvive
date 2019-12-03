using System;
using UnityEngine;

namespace UiScenario.Fade
{
    public class WindowFadeSettings
    {
        public WindowFadeType Type { get; private set;}
        
        public Action Callback { get; set; }

        public bool OverrideInvert { get; private set; }
        
        public bool Invert { get; private set; }
        
        public bool OverrideColor { get; private set; }
        
        public Color Color { get; private set; }
        
        public bool OverrideDuration { get; private set; }
                
        public float Duration { get; private set; }
        
        public bool PlayInvertedOnNextWindow { get; set; }

        
        public WindowFadeSettings(WindowFadeType type, Action callback = null)
        {
            Type = type;
            Callback = callback;
            OverrideInvert = false;
            Invert = false;
            OverrideColor = false;
            Color = Color.black;
            OverrideDuration = false;
            Duration = 1;
            PlayInvertedOnNextWindow = false;
        }

        public static WindowFadeSettings None()
        {
            return new WindowFadeSettings(WindowFadeType.None);
        }

        public WindowFadeSettings InvertMask()
        {
            OverrideInvert = true;
            Invert = true;
            return this;
        }

        public WindowFadeSettings WithColor(Color color)
        {
            OverrideColor = true;
            Color = color;
            return this;
        }

        public WindowFadeSettings WithDuration(float duration)
        {
            OverrideDuration = true;
            Duration = duration;
            return this;
        }

        public WindowFadeSettings CompleteOnNextWindow()
        {
            PlayInvertedOnNextWindow = true;
            return this;
        }

    }
}