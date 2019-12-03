using System;
using UnityEngine;

namespace UiScenario.Fade
{
    [CreateAssetMenu(menuName = "Assets/Create/WindowFadeData", fileName = "fade")]
    [Serializable]
    public class WindowFadeData : ScriptableObject
    {
        [Serializable]
        public class MaskData
        {
            [SerializeField] private WindowFadeType _type;
            [SerializeField] private Texture2D _mask;
            [SerializeField] private bool _invert;

            public bool Invert => _invert;
            public WindowFadeType Type => _type;
            public Texture2D Mask => _mask;
        }

        [SerializeField] private Shader _shaderImageEffect;
        [SerializeField] private Shader _shaderMask;
        [SerializeField] private GameObject _maskCanvas;
        [Range(0, 1)] [SerializeField] private float _maskSpread;
        [SerializeField] private int _maskStep;
        [SerializeField] private float _duration;
        [SerializeField] private Color _color;
        [SerializeField] private MaskData[] _masks;

        public GameObject MaskCanvas => _maskCanvas;
        public Shader ShaderImageEffect => _shaderImageEffect;
        public Shader ShaderMask => _shaderMask;
        public Color Color => _color;
        public float MaskSpread => _maskSpread;
        public int MaskStep => _maskStep;
        public float Duration => _duration;
        public MaskData[] Masks => _masks;
    }
}