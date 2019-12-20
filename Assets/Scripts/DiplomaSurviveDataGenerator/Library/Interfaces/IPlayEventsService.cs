using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IPlayEventsService
    {
        double NextTime { get; }
        PlayEvent GetEvent();
        void GenerateNextTime();
    }
}
