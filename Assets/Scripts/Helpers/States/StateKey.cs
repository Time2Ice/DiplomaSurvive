using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    public class StateKey
    {
        public PlayState State { get; set; }
        public EventType Event { get; set; }

        public StateKey(PlayState state, EventType eventType)
        {
            State = state;
            Event = eventType;
        }
        public StateKey()
        { }
    }
}
