using UnityEngine.UI;
using VBCM.Interfaces;

namespace VBCM.EventSources
{
    public class ButtonEventSource<TEventHub> : EventSource<TEventHub>
        where TEventHub : EventHubBase<TEventHub>
    {
        public ButtonEventSource(Button button)
        {
            button.onClick.AddListener(OnEvent);
        }
    }
}