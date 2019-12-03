using System;
using System.Globalization;
using TMPro;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class AbilityInfoView : Contractor.View
    {
        //[SerializeField] private GridLayoutGroup _upgradesArea;
        
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _balanceText;

        [SerializeField] private AbilityUpgrade[] _abilityUpgrades;
        
        [SerializeField] private Button _closeButton;

        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }

        public void SetBalance(int balance)
        {
            _balanceText.text = balance.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetAbilityName(string name)
        {
            _nameText.text = name;
        }
        
        public void SetUpgradesData(string[] names)
        {
            for (int i = 0; i < 0; i++)
            {
                _abilityUpgrades[i]._nameText.text = names[i];
            }
        }
    }

    [Serializable]
    public struct AbilityUpgrade
    {
        public TMP_Text _priceText;
        public TMP_Text _nameText;
        public Button _buyButton;
    }

}