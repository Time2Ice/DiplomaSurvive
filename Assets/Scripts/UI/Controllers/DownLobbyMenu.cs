using System.Collections.Generic;
using System.Diagnostics;
using DefaultNamespace;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class DownLobbyMenu : Contractor
    {
        public class Controller : Controller<DownLobbyMenuView>
        {
            public override WindowType Type => WindowType.DownLobbyMenu;

            Controller()
            {

            }

            public override void Open(Dictionary<string, object> callData)
            {

            }

        }

        public new class Scenario : Contractor.Scenario, OpenWindowEvent.ISubscribed
        {
            public override WindowType Type => WindowType.DownLobbyMenu;

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

    }
}
