using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseEventContext : Context, IEventContext
    {
        public event Action<DeductionType> OnDeduct;
        public event Action<PlayEvent> OnOpenEventPage;
        public event Action OnCloseEventPage;
        public event Action OnFinishCourse;
        public event Action<IExam> OnStartExam;
        public event Action OnFailExam;
        public event Action OnPassExam;
        public event Action<IExam> OnStartEIT;
        public event Action OnFailEIT;
        public event Action OnPassEIT;

        public virtual void Deduct(DeductionType deduction)
        {
            OnDeduct?.Invoke(deduction);
            ContextChanged();
        }
        public virtual void OpenEventPage(PlayEvent eventPage)
        {
            OnOpenEventPage?.Invoke(eventPage);
            ContextChanged();
        }
        public virtual void CloseEventPage()
        {
            OnCloseEventPage?.Invoke();
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
        public virtual void StartGame(IExam exam)
        {
            OnStartExam?.Invoke(exam);
            ContextChanged();
        }
        public virtual void FailExam()
        {
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
