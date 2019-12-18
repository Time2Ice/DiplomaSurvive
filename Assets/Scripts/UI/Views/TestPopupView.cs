
using UiScenario;
using UnityEngine;

namespace Assets.Scripts.UI.Views
{
   public class TestPopupView: Contractor.View
    {
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
    }
}
