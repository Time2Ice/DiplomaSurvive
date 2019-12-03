using System;
using System.Globalization;
using DefaultNamespace;
using EventAggregator;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Views
{
    public class AbilitiesView:Contractor.View, IPublisherAgg<Abilities.BuyClickEvent, Abilities.CloseClickEvent>
    {        
        
        [SerializeField] private Ability[] _abilities;
      
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _balanceText;

        public Func<Abilities.BuyClickEvent> Event1 { get; set; }
        public Func<Abilities.CloseClickEvent> Event2 { get; set; }
        
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            Infrastructure.BinderAgg.Bind(this);
            for (int i = 0; i < _abilities.Length; i++)
            {
                BindBuyButton(i);
            }
            _closeButton.onClick.AddListener(()=>Event2().Publish());
        }

        private void BindBuyButton(int index)
        {
            _abilities[index]._buyButton.onClick.AddListener(()=>Event1().Publish(index));
        }

        public void SetAbilitiesData(AbilityDto[] abilities)
        {
            for (int i = 0; i < 0; i++)
            {
                _abilities[i]._nameText.text = abilities[i].name;
                _abilities[i]._priceText.text = abilities[i].price.ToString(CultureInfo.InvariantCulture);
            }
        }
        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString(CultureInfo.InvariantCulture);
        }

       
    }

    [Serializable]
    public struct Ability
    {
        public TMP_Text _nameText;
        public Button _buyButton;
        public TMP_Text _priceText;

    }
    
}