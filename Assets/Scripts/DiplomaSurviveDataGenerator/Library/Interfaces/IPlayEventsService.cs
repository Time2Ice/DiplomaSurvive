using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IPlayEventsService
    {
        bool IsAvailable { get; }
        double EventProbability { get; set; }
        PlayEvent GetEvent();
    }
}
