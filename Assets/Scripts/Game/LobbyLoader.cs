using UiScenario;
using UiScenario.Concrete.Data;

namespace Game
{
    public class LobbyLoader
    {
        public LobbyLoader(IWindowHandler windowHandler)
        {
            windowHandler.OpenWindow(WindowType.Characters);
            windowHandler.OpenWindow(WindowType.TopLobbyMenu);
        }
    }
}