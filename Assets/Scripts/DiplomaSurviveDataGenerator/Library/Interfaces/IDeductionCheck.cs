using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IDeductionCheck
    {
        string CheckForDeduction();
        bool IsNecessary { get; }
    }
}
