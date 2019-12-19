using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface INumberGenerator
    {
        int Next(int min, int max);
        int Next(int max);
        int Next();
        double NextDouble();
        double NextDouble01();
        double NextDouble(double max);
        int? IndexByCoefficients(List<int> coefficients);
    }
}
