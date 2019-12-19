

using Assets.Scripts.UI.Views;
using EventAggregator;
using System.Collections.Generic;
using UiScenario;
using UiScenario.Concrete.Data;

namespace UI.Controllers
{
   public class TestPopup: Contractor
    {
        public class Controller : Controller<TestPopupView>, SecondButtonClicked.ISubscribed, FirstButtonClicked.ISubscribed
        {
            public override WindowType Type => WindowType.Test;
            Controller()
            {

            }
            public override void Open(Dictionary<string, object> callData)
            {
            }

            void EventAggHub<SecondButtonClicked>.ISubscribed.OnEvent()
            {
                
            }

            void EventAggHub<FirstButtonClicked>.ISubscribed.OnEvent()
            {
               
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Test;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

        }

        public class SecondButtonClicked : EventAggHub<SecondButtonClicked>
        {
        }

        public class FirstButtonClicked : EventAggHub<FirstButtonClicked>
        {
        }
    }
}
