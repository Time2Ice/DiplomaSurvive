using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface ICloneable<T> : IShallowCopy
    {
        T Clone();
    }
}
