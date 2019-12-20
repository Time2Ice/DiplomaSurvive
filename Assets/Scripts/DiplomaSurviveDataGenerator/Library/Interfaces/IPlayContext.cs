using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IPlayContext : IContext
    {
        IScoreContext Score { get; }
        IMainContext Main { get; }
        IEventContext Events { get; }
        IStudyContext Study { get; }
    }
}
