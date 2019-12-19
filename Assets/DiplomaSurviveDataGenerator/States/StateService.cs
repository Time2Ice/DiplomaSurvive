using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class StateService : IStateService
    {
        protected IDictionary<StateKey, PlayState> _stateMachine;

        public StateService(IDictionary<StateKey, PlayState> statesConverter)
        {
            _stateMachine = statesConverter;
        }

        public PlayState ChangeState(PlayState currState, EventType eventType)
        {
            var key = new StateKey
            {
                State = currState,
                Event = eventType
            };
            return _stateMachine[key];
        }
    }

}
