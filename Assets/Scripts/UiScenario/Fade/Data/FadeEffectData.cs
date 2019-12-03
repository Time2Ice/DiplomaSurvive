using System;
using UnityEngine;

namespace UiScenario.Fade
{
    [Serializable]
    public struct FadeEffectData
    {
        public Shader Shader;
        public Texture2D Mask;
        public float Duration;
        public bool Invert;
        public Color Color;
        public float MaskSpread;
        public int MaskStep;
        public float From;
        public float To;
        public Action Callback;
        public Vector2 MaskOffset;
        public Vector2 MaskScale;
        public bool Destroy;
    }
}