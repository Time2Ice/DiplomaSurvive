using System.Collections.Generic;
using UiScenario.Concrete.Data;

namespace UiScenario
{
    public interface IWindowHandler
    {
        bool IsOpen(WindowType type);
        void OpenWindow(WindowType type, Dictionary<string, object> callData = null);
        void CloseWindow(WindowType type);
        IWindowController GetTopWindow();
    }
}