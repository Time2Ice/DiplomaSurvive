using System.Collections.Generic;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SendDown : Contractor
    {
        public class Controller : Controller<SendDownView>, RestudyEvent.ISubscribed
        {
            public override WindowType Type => WindowType.SendDown;

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

            private void ShowStipendium(int value)
            {
                ConcreteView.SetStipendium(value);
            }

            private void ShowCourse(int value)
            {
                ConcreteView.SetCourse(value);
            }

            private void ShowContinuePossibility(int value)
            {
                ConcreteView.SetContinuePossibility(value);
            }

            void EventAggHub<RestudyEvent>.ISubscribed.OnEvent()
            {
                throw new System.NotImplementedException();
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.SendDown;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }



        }

        public class RestudyEvent : EventAggHub<RestudyEvent>
        {
        }

    }
}
  