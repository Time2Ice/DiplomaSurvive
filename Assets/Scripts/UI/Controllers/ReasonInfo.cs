using System;
using System.Collections.Generic;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class ReasonInfo:Contractor
    {
        public class Controller : Controller<ReasonInfoView>, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.GoUp;           
           
            Controller()
            {
              
            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowData("Reason", "Description");
            }

            private void ShowData(string name, string description)
            {
                ConcreteView.SetReasonData(name, description);
            }
            
            private void ShowImage(Sprite sprite)
            {
                ConcreteView.SetReasonImage(sprite);
            }

            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
                OnClose();
            }

            public void Dispose()
            {
                
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.ReasonInfo;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

      
        }
    
        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }

    }
}
   