using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class MainContext : Context
    {
        protected IStateService _stateService;
        protected PlayState _state;
        protected int _level;
        public virtual int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnLevelChanged?.Invoke();
            }
        }
        public virtual PlayState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnStateChanged?.Invoke();
            }
        }

        public event ValueChanged OnLevelChanged;
        public event ValueChanged OnStateChanged;

        public MainContext(IStateService service)
        {
            _stateService = service;
            OnLevelChanged += ContextChanged;
            OnStateChanged += ContextChanged;
        }

        public virtual void ChangeState(EventType eventType)
        {
            State = _stateService.ChangeState(_state, eventType);
        }
    }
}
