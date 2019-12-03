﻿using System;

namespace VBCM.Interfaces
{
    public class EventSource<TEventHub> 
        where TEventHub : EventHubBase<TEventHub>
    {
        public event Action Event;

        public void OnEvent()
        {
            Event?.Invoke();
        }
    }
}