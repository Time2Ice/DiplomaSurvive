using TMPro;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SettingsView:Contractor.View
    {
        
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _languageButton;
        [SerializeField] private Button _relesButton;
        [SerializeField] private Button _creatorsButton;
        [SerializeField] private Button _closeButton;
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
        
    }
}