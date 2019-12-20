using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BasePlayContext : Context, IPlayContext
    {
        public IScoreContext Score { get; set; }
        public IMainContext Main { get; set; }
        public IEventContext Events { get; set; }
        public IStudyContext Study { get ; set; }
    }

    public interface IPlayContext : IContext
    {
        IScoreContext Score { get; set; }
        IMainContext Main { get; set; }
        IEventContext Events { get; set; }
        IStudyContext Study { get; set; }
    }
}
