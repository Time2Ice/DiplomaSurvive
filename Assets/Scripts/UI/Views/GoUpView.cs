using System;
using EventAggregator;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GoUpView:Contractor.View, IPublisherAgg<GoUp.CloseClickEvent>
    {

        [SerializeField] private TMP_Text _successText;
       
        [SerializeField] private Button _okButton;
        
        public Func<GoUp.CloseClickEvent> Event1 { get; set; }
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

             Infrastructure.BinderAgg.Bind(this);
             _okButton.onClick.AddListener(()=>Event1().Publish());
        }

        public void SetSuccessText(string sucessText)
        {
            _successText.text = sucessText;
        }

     
    }
}