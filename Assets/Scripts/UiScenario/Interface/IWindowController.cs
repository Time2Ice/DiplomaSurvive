using System;
using System.Collections.Generic;
using UiScenario.Concrete.Data;
using UiScenario.Data;

namespace UiScenario
{
    public interface IWindowController
    {
        event Action<IWindowController> Closed;

        WindowType Type { get; }
        IWindowView View { get; }

        void Init(IWindowView view);
        void Open(Dictionary<string, object> callData);
        void Block();
        void Unblock();
        void OnClose();
    }
}
