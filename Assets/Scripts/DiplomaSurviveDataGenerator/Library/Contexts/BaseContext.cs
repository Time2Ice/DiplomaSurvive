using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BasePlayContext : Context, IPlayContext
    {
        public IScoreContext Score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMainContext Main { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEventContext Events { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public interface IPlayContext : IContext
    {
        IScoreContext Score { get; set }
        IMainContext Main { get; set; }
        IEventContext Events { get; set; }
        IStudyContext Study { get; set; }
    }
}
