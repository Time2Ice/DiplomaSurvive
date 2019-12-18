using Assets.Scripts.UI.Views;
using EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;
using UiScenario.Concrete.Data;
using UnityEngine;
using static UiScenario.Contractor;

namespace Assets.Scripts.UI.Controllers
{
    public class EventPopup : Contractor
    {
        public class Controller : Controller<EventPopupView>, SelectAction.ISubscribed
        {
            public override WindowType Type => WindowType.Event;

            Controller()
            {

            }
            public override void Open(Dictionary<string, object> callData)
            {

            }

            public void OnEvent(bool isAccept)
            {
               if(isAccept)
                {
                    //doAction
                }
                OnClose();
                
            }
        }


        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Event;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }


        }

        public class SelectAction : EventAggHub<SelectAction, bool>
        {
        }
    }
}
