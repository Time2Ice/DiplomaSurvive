using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IMainContext : IContext
    {
        int? Level { get; set; }
        event ValueChanged OnLevelChanged;
    }
}
