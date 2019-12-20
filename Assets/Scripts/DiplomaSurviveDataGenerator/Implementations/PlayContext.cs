using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class PlayContext : IPlayContext
    {
        public IScoreContext Score { get ; set ; }
        public IMainContext Main { get ; set; }
        public IEventContext Events { get ; set; }
        public IStudyContext Study { get ; set; }

        public event ValueChanged OnContextChanged;
    }
}
