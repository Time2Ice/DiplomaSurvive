using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class EventsContext : Context
    {
        protected IDeductionStore _deductionStore;
        public event Action<Deduction> OnDeduction;
        public event Action<PlayEvent> OnOpenEventPage;
        public event Action<PlayEvent> OnCloseEventPage;
        public event Action OnFinishCourse;
        public event Action<IExam> OnStartExam;
        public event Action OnFailExam;
        public event Action OnPassExam;

        public EventsContext(IDeductionStore deductionStore)
        {
            _deductionStore = deductionStore;
        }
        public virtual void Deduct(DeductionType Type)
        {
            var deduction = _deductionStore.GetDeduction(Type);
            OnDeduction?.Invoke(deduction);
            ContextChanged();
        }
        public virtual void OpenEventPage(PlayEvent eventPage)
        {
            OnOpenEventPage?.Invoke(eventPage);
            ContextChanged();
        }
        public virtual void CloseEventPage(PlayEvent eventPage)
        {
            OnCloseEventPage?.Invoke(eventPage);
            ContextChanged();
        }
        public virtual void FinishCourse()
        {
            OnFinishCourse?.Invoke();
            ContextChanged();
        }
        public virtual void StartExam(IExam exam)
        {
            OnStartExam?.Invoke(exam);
            ContextChanged();
        }
        public virtual void FailExam()
        {
            //Deduct(DeductionType.FailExam);
            OnFailExam?.Invoke();
            ContextChanged();
        }
        public virtual void PassExam()
        {
            OnPassExam?.Invoke();
            ContextChanged();
        }
    }
}
