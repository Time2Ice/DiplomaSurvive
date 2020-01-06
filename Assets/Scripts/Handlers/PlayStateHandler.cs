using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handlers
{
    public class PlayStateHandler : IPlayStateHandler
    {
        public event Action<PlayState> OnStateChanged;

        protected PlayState _state;
        protected IDictionary<StateKey, PlayState> _stateMachine;

        public PlayStateHandler(IDictionary<StateKey, PlayState> statesConverter = null)
        {
            _stateMachine = statesConverter ?? DefaultStateMachine();
        }

        protected IDictionary<StateKey, PlayState> DefaultStateMachine()
        {
            var dictionary = new Dictionary<StateKey, PlayState>();
            foreach (var state in Enum.GetValues(typeof(PlayState)).Cast<PlayState>())
            {
                foreach (var eventType in Enum.GetValues(typeof(EventType)).Cast<EventType>())
                {
                    dictionary.Add(new StateKey(state, eventType), PlayState.Classroom);
                }
            }
            return dictionary;
        }
        public virtual void SetState(PlayState state)
        {
            _state = state;
            OnStateChanged?.Invoke(state);
        }
        public virtual void ChangeState(EventType eventType)
        {
            var key = new StateKey(_state, eventType);
            if (_stateMachine.ContainsKey(key))
            {
                _state = _stateMachine[key];
                OnStateChanged?.Invoke(_state);
            }
        }
        public virtual PlayState GetState()
        {
            return _state;
        }
    }
}
