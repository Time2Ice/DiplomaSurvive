using System;
using EventAggregator;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ReasonInfoView:Contractor.View,  IPublisherAgg<ReasonInfo.CloseClickEvent>
    {
        [SerializeField] private Image _reasonImage;
            
        [SerializeField] private TMP_Text _reasonText;
        [SerializeField] private TMP_Text _reasonName;
        
        [SerializeField] private Button _closeButton;
        
        public Func<ReasonInfo.CloseClickEvent> Event1 { get; set; }
      
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            Infrastructure.BinderAgg.Bind(this);
            _closeButton.onClick.AddListener(()=>Event1().Publish());
        }

        public void SetReasonData(string text, string name)
        {
            _reasonText.text = text;
            _reasonName.text = name;
          
        }

        public void SetReasonImage(Sprite image)
        {
            _reasonImage.sprite = image;
        }

        
    }
}