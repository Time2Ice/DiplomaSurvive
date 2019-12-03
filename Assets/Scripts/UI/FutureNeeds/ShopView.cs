using System;
using TMPro;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ShopView : Contractor.View
    {
       // [SerializeField] private GridLayoutGroup _shopArea;

        [SerializeField] private ShopItem[] _shopItems;

        [SerializeField] private Button _closeButton;

        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
        
        public void SetItemsData(string[] names)
        {
            for (int i = 0; i < 0; i++)
            {
                _shopItems[i]._itemName.text = names[i];
            }
        }
    }

    [Serializable]
    public struct ShopItem
    {
        public Button _buyButton;
        public TMP_Text _itemName;
    }
}