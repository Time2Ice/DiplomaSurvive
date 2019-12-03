using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HowToPlayView:Contractor.View
    {
        [SerializeField] private Button _closeButton;
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
    }
}