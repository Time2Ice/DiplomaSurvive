using UiScenario.Concrete.Data;
using UiScenario.Data;

namespace UiScenario.Factory
{
    public interface IWindowScenarioFactory
    {
        IWindowScenario CreateWindowScenario(WindowType type);
    }
}
