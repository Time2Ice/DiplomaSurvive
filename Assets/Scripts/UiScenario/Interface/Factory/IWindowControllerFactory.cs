using UiScenario.Concrete.Data;
using UiScenario.Data;

namespace UiScenario.Factory
{
    public interface IWindowControllerFactory
    {
        IWindowController CreateWindowController(WindowType type);
    }
}