using System.Collections.Generic;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class RecordBook : Contractor
    {
        public class Controller : Controller<RecordBookView>, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.RecordBook;

            Controller()
            {

            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowUniversityCount(1);
                ShowCurrentCourse(1);
                ShowMaxCourse(1);
            }

            private void ShowUniversityCount(int value)
            {
                ConcreteView.SetUniversityCount(value);

            }

            private void ShowCurrentCourse(int value)
            {
                ConcreteView.SetCurrentCourse(value);

            }

            private void ShowMaxCourse(int value)
            {
                ConcreteView.SetMaxCourse(value);
            }


            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
                OnClose();
            }
        }

        public new class Scenario : Contractor.Scenario, OpenWindowEvent.ISubscribed
        {
            public override WindowType Type => WindowType.RecordBook;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

            void EventAggHub<OpenWindowEvent, WindowType>.ISubscribed.OnEvent(WindowType value)
            {
                WindowHandler.OpenWindow(value);
            }
        }

        public class OpenWindowEvent : EventAggHub<OpenWindowEvent, WindowType>
        {
        }

        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }

    }
}
