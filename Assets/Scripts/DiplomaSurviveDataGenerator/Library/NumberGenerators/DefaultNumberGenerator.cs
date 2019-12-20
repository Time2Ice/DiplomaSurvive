using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class DefaultNumberGenerator : INumberGenerator
    {
        private readonly Random _randomGenerator = new Random();
        public int Next(int min, int max)
        {
            return _randomGenerator.Next(min, max);
        }
        public int Next(int max)
        {
            return Next(0, max);
        }
        public int Next()
        {
            return Next(0, int.MaxValue);
        }
        public double NextDouble01()
        {
            return _randomGenerator.NextDouble();
        }
        public double NextDouble()
        {
            return (_randomGenerator.NextDouble() % 1.0f) * int.MaxValue;
        }
        public double NextDouble(double max)
        {
            return (_randomGenerator.NextDouble() % 1.0f) * max;
        }
        public int? IndexByCoefficients(List<int> coefficients)
        {
            int maxNumber = 0;
            foreach (var coef in coefficients)
            {
                maxNumber += coef <= 0 ? 1 : coef;
            }

            if (maxNumber == 0)
            {
                return null;
            }

            int resNumber = Next(0, maxNumber);
            int currNumber = 0;
            for (int i = 0; i < coefficients.Count; i++)
            {
                currNumber += coefficients[i];
                if (resNumber < currNumber)
                {
                    return i;
                }
            }

            return null;
        }
    }
}
