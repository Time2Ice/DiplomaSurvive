using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface INumberDistribution
    {
        double Min { get; set; }
        double Max { get; set; }
        double Next();
    }
}
