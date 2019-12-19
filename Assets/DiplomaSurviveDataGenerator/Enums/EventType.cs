using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public enum EventType
    {
        OnStart,
        OpenEvent,
        CloseEvent,
        StartExam,
        FailExam,
        PassExam,
        NewCourse,
        OpenSettings,
        CloseSettings,
        OpenMenu,
        CloseMenu,
        StartEIT,
        FailEIT,
        PassEIT,
        StartGame,
        FinishGame
    }
}
