using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BasePlayContext : Context, IPlayContext
    {
        public IScoreContext Score { get; }
        public IMainContext Main { get; }
        public IEventContext Events { get; }
        public IStudyContext Study { get; }

        public BasePlayContext(IScoreContext scoreContext, IMainContext mainContext, 
            IEventContext eventContext, IStudyContext studyContext)
        {
            Score = scoreContext;
            Main = mainContext;
            Events = eventContext;
            Study = studyContext;
        }
    }
}
