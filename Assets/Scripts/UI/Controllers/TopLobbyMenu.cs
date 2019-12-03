using System.Collections.Generic;
using System.Globalization;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class TopLobbyMenu : Contractor
    {
        public class Controller : Controller<TopLobbyMenuView>
        {
            public override WindowType Type => WindowType.TopLobbyMenu;

            Controller()
            {

            }

            public override void Open(Dictionary<string, object> callData)
            {

            }


            private void ShowBalance(int value)
            {
                ConcreteView.SetBalance(value);
            }

            private void ShowUniversityCount(int value)
            {
                ConcreteView.SetUniversityCount(value);
            }

            private void SetSemester(int value)
            {
                ConcreteView.SetSemester(value);
            }

            private void SetContinuePossibility(int value)
            {
                ConcreteView.SetContinuePossibility(value);
            }

            private void SetPersonalLife(int currentValue, int maxValue)
            {
                ConcreteView.SetPersonalLife(currentValue, maxValue);
            }

            private void SetMarks(int currentValue, int maxValue)
            {
                ConcreteView.SetMarks(currentValue, maxValue);
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.TopLobbyMenu;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }



        }
    }
}