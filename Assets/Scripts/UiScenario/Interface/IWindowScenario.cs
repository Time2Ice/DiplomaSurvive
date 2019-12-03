using System;
using System.Collections.Generic;
using UiScenario.Concrete.Data;
using UiScenario.Data;
using UiScenario.Fade;

namespace UiScenario
{
    public interface IWindowScenario
    {
        WindowState State { get; }
        WindowType Type { get; }
        event Action<WindowType> Completed;
        event Action<WindowFadeSettings> FadeIn;
        event Action<WindowFadeSettings> FadeOut;
        void Execute(Dictionary<string, object> callData = null);
        void Resume();
        
        void OnWindowAction(WindowActionType actionType, IWindowController controller);
    }
}
