using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LanguagesView:Contractor.View
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private  Toggle[] _languageToggles;
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
    }
}