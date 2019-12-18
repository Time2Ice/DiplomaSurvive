using Assets.Scripts.UI.Controllers;
using EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Views
{
    public class EventPopupView : Contractor.View, IPublisherAgg<EventPopup.SelectAction>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _eventText;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        public Func<EventPopup.SelectAction> Event1 { get; set; }
       
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);
            Infrastructure.BinderAgg.Bind(this);
            _yesButton.onClick.AddListener(() => Event1().Publish(true));
            _noButton.onClick.AddListener(() => Event1().Publish(false));
        }

        public void SetEventText(string text)
        {
            _eventText.text = text;
        }
    }

   
}
