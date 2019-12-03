using UnityEngine;

namespace UiScenario.Fade.Effects
{
    public class WindowFadeEffect : MonoBehaviour, IFadeEffect
    {
        [SerializeField]
        private bool _debug;
        private bool _enabled = false;
        
        [SerializeField]
        private FadeEffectData _data;
        private float _current;
        [SerializeField]
        private float _value;
        [SerializeField]
        private Material _material;
        public void Run(FadeEffectData data)
        {
            
            _data = data;
            _current = 0;
            _material = new Material(_data.Shader);
            
            _material.SetFloat("_OffsetX", _data.MaskOffset.x);
            _material.SetFloat("_OffsetY", _data.MaskOffset.y);
            _material.SetFloat("_ScaleX", _data.MaskScale.x);
            _material.SetFloat("_ScaleY", _data.MaskScale.y);
            
            _material.SetColor("_MaskColor", _data.Color);
            _material.SetFloat("_MaskSpread", _data.MaskSpread);
            _material.SetInt("_MaskStep", _data.MaskStep);
            _material.SetTexture("_MaskTex", _data.Mask);
            
            if (_material.IsKeywordEnabled("INVERT_MASK") != _data.Invert)
            {
                if (_data.Invert)
                    _material.EnableKeyword("INVERT_MASK");
                else
                    _material.DisableKeyword("INVERT_MASK");
            }
            
            UpdateValue();
            
            _enabled = true;
        }

        public void Destroy()
        {
            Destroy(this);
        }

        public void Update()
        {
            if (_debug) return;
            if (!_enabled) return;
            _current += Time.deltaTime;
            UpdateValue();
            if (_current < _data.Duration) return;
            _enabled = false;
            _data.Callback();
        }

        private void UpdateValue()
        {
            _value = Mathf.Lerp(_data.From, _data.To, _current / _data.Duration);
            _material.SetFloat("_MaskValue", _value);
        }
        
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!_enabled)
            {
                Graphics.Blit(source, destination);
                return;
            }

            if (_debug)
            {
                _material.SetFloat("_OffsetX", _data.MaskOffset.x);
                _material.SetFloat("_OffsetY", _data.MaskOffset.y);
                _material.SetFloat("_ScaleX", _data.MaskScale.x);
                _material.SetFloat("_ScaleY", _data.MaskScale.y);
                
                _material.SetColor("_MaskColor", _data.Color);
                _material.SetFloat("_MaskSpread", _data.MaskSpread);
                _material.SetInt("_MaskStep", _data.MaskStep);
                _material.SetTexture("_MaskTex", _data.Mask);
            
                if (_material.IsKeywordEnabled("INVERT_MASK") != _data.Invert)
                {
                    if (_data.Invert)
                        _material.EnableKeyword("INVERT_MASK");
                    else
                        _material.DisableKeyword("INVERT_MASK");
                }
                _material.SetFloat("_MaskValue", _value);
            }

            _material.SetTexture("_MainTex", source);

            Graphics.Blit(source, destination, _material);
        }
        
    }
}