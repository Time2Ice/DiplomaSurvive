using System;
using System.Collections.Generic;
using DefaultNamespace;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class GoUp:Contractor
    {
    public class Controller : Controller<GoUpView>, IDisposable, CloseClickEvent.ISubscribed
    {
        public override WindowType Type => WindowType.GoUp;           
           
        Controller()
        {
              
        }
        public override void Open(Dictionary<string, object> callData)
        {
            
        }
            
            

        private void ShowText(string sucessText)
        {
            ConcreteView.SetSuccessText(sucessText);
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
        public override WindowType Type => WindowType.GoUp;

        public Scenario(IWindowHandler windowHandler) : base(windowHandler)
        {
        }

      
    }
    
    public class CloseClickEvent : EventAggHub<CloseClickEvent>
    {
    }

    }
}