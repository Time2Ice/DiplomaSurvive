
using EventAggregator;
using System;
using TMPro;
using UI.Controllers;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Views
{
   public class TestPopupView: Contractor.View, IPublisherAgg<TestPopup.FirstButtonClicked, TestPopup.SecondButtonClicked>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _taskText;
        [SerializeField] private Button _firstButton;
        [SerializeField] private Button _secondButton;
        [SerializeField] private TMP_Text _firstButtonText;
        [SerializeField] private TMP_Text _secondButtonText;

        public Func<TestPopup.FirstButtonClicked> Event1 { get; set; }
        public Func<TestPopup.SecondButtonClicked> Event2 { get; set; }

        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);
            Infrastructure.BinderAgg.Bind(this);
            _firstButton.onClick.AddListener(() => Event1().Publish());
            _secondButton.onClick.AddListener(() => Event1().Publish());
        }

        public void SetTestText(string text)
        {
            _taskText.text = text;
        }

        public void SetFirstButtonText(string text)
        {
            _firstButtonText.text = text;
        }

        public void SetSecondButtonText(string text)
        {
            _secondButtonText.text = text;
        }
    }
}
