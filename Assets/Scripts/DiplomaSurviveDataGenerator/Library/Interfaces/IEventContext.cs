using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IEventContext
    {
        event Action<ExclusionType> OnDeduct;
        event Action<PlayEvent> OnOpenEventPage;
        event Action OnCloseEventPage;
        event Action OnFinishCourse;
        event Action<IExam> OnStartExam;
        event Action OnFailExam;
        event Action OnPassExam;
        event Action<IExam> OnStartEIT;
        event Action OnFailEIT;
        event Action OnPassEIT;
        void OpenEventPage(PlayEvent eventPage);
        void CloseEventPage();
        void FinishCourse();
        void Deduct(ExclusionType exclusion);
        void StartExam(IExam exam);
        void FailExam();
        void PassExam();
    }
}
