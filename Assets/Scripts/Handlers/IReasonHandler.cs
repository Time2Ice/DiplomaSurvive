using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;

namespace Assets.Scripts.Handlers
{
    public interface IReasonHandler
    {
        event Action<string> ReasonOpened;
        void ShowReason(string id);
        IWindowHandler WindowHandler { get; set; }
    }
}
