using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseContext : Context
    {
        public virtual ScoreContext Score { get; set; }
        public virtual TimeContext Time { get; set; }
        public virtual TasksContext Tasks { get; set; }
        public virtual MainContext Main { get; set; }
        public virtual EventsContext Events { get; set; }
    }
}
