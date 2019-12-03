using UiScenario.Concrete.Data;
using UiScenario.Data;

namespace UiScenario
{
    public interface IWindowBuilder
    {
        IWindowController Create(WindowType type, int topWindowOrder);
    }
}