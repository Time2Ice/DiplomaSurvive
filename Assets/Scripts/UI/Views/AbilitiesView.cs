using System;
using System.Globalization;
using DefaultNamespace;
using EventAggregator;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
            _closeButton.onClick.AddListener(()=>Event2().Publish());
        }

        private void BindBuyButton(Ability ability)
        {
           ability._buyButton.onClick.RemoveAllListeners();
           ability._buyButton.onClick.AddListener(()=>Event1().Publish(ability.Id));
        }

        public void SetAbilitiesData(AbilityDto[] abilities)
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                _abilities[i]._nameText.text = abilities[i].name;
                _abilities[i]._priceText.text = abilities[i].price.ToString(CultureInfo.InvariantCulture);
                _abilities[i].Id = abilities[i].id;
                BindBuyButton(_abilities[i]);
            }
        }
        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString(CultureInfo.InvariantCulture);
        }

        internal void SetBoughtAbility(int id)
        {
            var ability = _abilities.FirstOrDefault(a => a.Id == id);
            ability._priceText.text = "Bought";
            ability._buyButton.enabled=false;
        }
    }

    [Serializable]
    public struct Ability
    {
        public TMP_Text _nameText;
        public Button _buyButton;
        public TMP_Text _priceText;
        public int Id { get; set; }

    }
    
}