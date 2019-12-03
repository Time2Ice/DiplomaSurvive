
using DefaultNamespace;

namespace Game
{
    public interface IAppStateHandler
    {
        StageAppType State { get; }
        void ChangeState(StageAppType state);
        void SetData<TData>(StageAppType state, TData data);
        bool GetData<TData>(out TData data);
        void ClearData(StageAppType state);
    }
}