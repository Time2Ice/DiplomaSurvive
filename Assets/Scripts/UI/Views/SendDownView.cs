using System;
using System.Globalization;
using EventAggregator;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SendDownView:Contractor.View, IPublisherAgg<SendDown.RestudyEvent>
    {
        [SerializeField] private Image _reasonImage;
            
        [SerializeField] private TMP_Text _reasonText;
        [SerializeField] private TMP_Text _reasonName;
        [SerializeField] private TMP_Text _stipendiumText;
        [SerializeField] private TMP_Text _semesterText;
        [SerializeField] private TMP_Text _continuePossibilityText;

        [SerializeField] private Button _restudyButton;
        
        public Func<SendDown.RestudyEvent> Event1 { get; set; }
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

             Infrastructure.BinderAgg.Bind(this);
             
             _restudyButton.onClick.AddListener(()=>Event1().Publish());
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
        
        public  void SetStipendium(int value)
        {
            _stipendiumText.text = value.ToString(CultureInfo.InvariantCulture);
        }
        public  void SetCourse(int value)
        {
            _semesterText.text = value.ToString(CultureInfo.InvariantCulture);
        }
        public  void SetContinuePossibility(int value)
        {
            _continuePossibilityText.text = value.ToString(CultureInfo.InvariantCulture);
        }

       
    }
}