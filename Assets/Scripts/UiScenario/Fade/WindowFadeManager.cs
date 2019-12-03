using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UiScenario.Fade
{
    public class WindowFadeManager : IWindowFadeManager
    {
        private readonly WindowFadeData _data;
        private readonly IFadeDataBuilder _fadeDataBuilder;
        private readonly IFadeEffectBuilder _fadeEffectBuilder;

        private readonly Queue<IFadeData> _fadeDatas = new Queue<IFadeData>();

        private IFadeData _nextWindowFadeData;
        private IFadeData _curentFadeData;


        public WindowFadeManager(IFadeDataBuilder fadeDataBuilder, IFadeEffectBuilder effectBuilder,
            WindowFadeData data)
        {
            _fadeEffectBuilder = effectBuilder;
            _fadeDataBuilder = fadeDataBuilder;
            _data = data;
        }

        public event EventHandler<FadeEventArgs> FadeStarted;
        public event EventHandler<FadeEventArgs> FadeCompleted;

        public void RunFadeIn(WindowFadeSettings settings, Action onComplete)
        {
            RunFade(_fadeDataBuilder.BuildFadeIn(settings, onComplete));
        }

        public void RunFadeOut(WindowFadeSettings settings, Action onComplete)
        {
            RunFade(_fadeDataBuilder.BuildFadeOut(settings, onComplete));
        }

        public void OnUIActionDone()
        {
            if (_nextWindowFadeData != null)
            {
                _nextWindowFadeData.IsFadeOut = !_nextWindowFadeData.IsFadeOut;
                _nextWindowFadeData.Settings.PlayInvertedOnNextWindow = false;
                _nextWindowFadeData.Settings.Callback = null;
                _nextWindowFadeData.OnComplete = null;
                RunFade(_nextWindowFadeData);
                _nextWindowFadeData = null;
            }
        }


        private void RunFade(IFadeData data)
        {
            if (_curentFadeData != null)
            {
                _fadeDatas.Enqueue(data);
                return;
            }

            if (data.Settings.Type == WindowFadeType.None)
            {
                data.OnComplete?.Invoke();
                return;
            }

            WindowFadeData.MaskData mask;

            if (data.Settings.Type == WindowFadeType.Random)
            {
                mask = _data.Masks.ElementAt(Random.Range(0, _data.Masks.Length));
            }
            else
            {
                mask = _data.Masks.FirstOrDefault(x => x.Type == data.Settings.Type);
                if (mask == null)
                {
                    throw new Exception($"Mask for {data.Settings.Type} not found");
                }
            }

            var effectData = new FadeEffectData
            {
                Mask = mask.Mask,
                Color = _data.Color,
                Duration = _data.Duration,
                MaskSpread = _data.MaskSpread,
                MaskStep = _data.MaskStep,
                Invert = mask.Invert,
                From = 0.0f,
                To = 1.0f,
                Callback = OnCurrentFadeCompleted,
                Destroy = true,
            };

            if (data.Settings.OverrideInvert)
            {
                effectData.Invert = data.Settings.Invert;
            }

            if (data.Settings.OverrideColor)
            {
                effectData.Color = data.Settings.Color;
            }

            if (data.Settings.OverrideDuration)
            {
                effectData.Duration = data.Settings.Duration;
            }

            if (data.IsFadeOut)
            {
                effectData.From = 1.0f;
                effectData.To = 0.0f;
            }

            effectData.MaskScale = Vector2.one;
            effectData.MaskOffset = Vector2.zero;

            var aspect = Screen.height / (float) Screen.width;
            effectData.MaskScale.y *= aspect;
            effectData.MaskOffset.y = (1 - effectData.MaskScale.y) * .5f;

            _curentFadeData = data;
            effectData.Shader = _fadeEffectBuilder.GetShader();
            data.Effect = _fadeEffectBuilder.Build();
            OnFadeStarted(new FadeEventArgs(data.Settings, !data.IsFadeOut));
            data.Effect.Run(effectData);
        }

        private void OnCurrentFadeCompleted()
        {
            _curentFadeData.Effect.Destroy();
            _curentFadeData.Effect = null;

            OnFadeCompleted(new FadeEventArgs(_curentFadeData.Settings, !_curentFadeData.IsFadeOut));

            if (_curentFadeData.Settings.PlayInvertedOnNextWindow)
            {
                _nextWindowFadeData = _curentFadeData;
            }

            _curentFadeData.Settings.Callback?.Invoke();
            _curentFadeData.OnComplete?.Invoke();

            _curentFadeData = null;
            if (_fadeDatas.Count == 0) return;
            RunFade(_fadeDatas.Dequeue());
        }

        protected virtual void OnFadeStarted(FadeEventArgs e)
        {
            FadeStarted?.Invoke(this, e);
        }

        protected virtual void OnFadeCompleted(FadeEventArgs e)
        {
            FadeCompleted?.Invoke(this, e);
        }
    }
}