using System;
using System.Collections.Generic;
using DefaultNamespace;


namespace Game
{
    public class AppStateHandler : IAppStateHandler
    {
        private readonly Dictionary<StageAppType, Dictionary<Type, object>> _stateData =
            new Dictionary<StageAppType, Dictionary<Type, object>>();

        public StageAppType State { get; private set; } = StageAppType.None;

        public void ChangeState(StageAppType state)
        {
            if (State != state)
                State = state;
        }

        public void SetData<TData>(StageAppType state, TData data)
        {
            if (data == null)
                return;
            var type = typeof(TData);

            if (_stateData.ContainsKey(state))
                if (_stateData[state].ContainsKey(type))
                    _stateData[state][type] = data;
                else
                    _stateData[state].Add(type, data);
            else
            {
                _stateData.Add(state, new Dictionary<Type, object>
                {
                    {type, data}
                });
            }
        }

        public bool GetData<TData>(out TData data)
        {
            var type = typeof(TData);
            if (_stateData.ContainsKey(State) && _stateData[State].ContainsKey(type))
            {
                data = (TData) _stateData[State][type];
                return true;
            }

            data = default;
            return false;
        }

        public void ClearData(StageAppType state)
        {
            if (_stateData.ContainsKey(state))
                _stateData.Remove(state);
        }
    }
}