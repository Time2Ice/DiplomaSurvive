using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class DefaultStateMachine : Dictionary<StateKey, PlayState>
    {
        private static readonly Dictionary<StateKey, PlayState> _dictionary;
        static DefaultStateMachine()
        {
            _dictionary = new Dictionary<StateKey, PlayState>
            {
                {
                    new StateKey(PlayState.Menu, EventType.CloseMenu),
                    PlayState.Classroom
                },
            };
        }
        public DefaultStateMachine()
            : base(_dictionary)
        { }
    }
}
