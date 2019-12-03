using System;
using System.Collections.Generic;
using System.Globalization;
using EventAggregator;
using Prefabs;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ReasonsView:Contractor.View, IPublisherAgg<Reasons.CloseClickEvent, Reasons.ChooseReasonEvent>
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _continuePossibilityText;
        [SerializeField] private GridLayoutGroup _reasonsArea;
        [SerializeField] private Button _closeButton;

        private Dictionary<string, Reason> _reasons;
        
        public Func<Reasons.CloseClickEvent> Event1 { get; set; }
        public Func<Reasons.ChooseReasonEvent> Event2 { get; set; }
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

             Infrastructure.BinderAgg.Bind(this);
             
             _closeButton.onClick.AddListener(()=>Event1().Publish());
        }
        
        public void SetProgress(int currentValue, int maxValue)
        {
            _progressSlider.value = (float) currentValue / maxValue;
            _progressText.text = currentValue.ToString(CultureInfo.InvariantCulture) +
                                 "/" + maxValue.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetContinuePossibility(string phrase)
        {
            _continuePossibilityText.text = phrase;
        }

        public void AddReason(Reason reason)
        {
            if (_reasons.ContainsKey(reason.Id)) return;
            reason.transform.SetParent(_reasonsArea.transform);
            _reasons.Add(reason.Id, reason);
            reason.SelectButton.onClick.RemoveAllListeners();
            reason.SelectButton.onClick.AddListener(()=>Event2().Publish(reason.Id));
        }
        
        public void ChangeReason(string id, string reasonName, Sprite image)
        {
            var reason = _reasons[id];
            reason.Name.text = reasonName;
            reason.Image.sprite = image;
        }

       
    }
}