using UiScenario.Concrete.Data;
using UiScenario.Data;

namespace UiScenario.Factory
{
    public interface IWindowViewFactory
    {
        IWindowView CreateWindowView(WindowType type);
    }
}
