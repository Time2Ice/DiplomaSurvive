using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IExam: ICloneable<IExam>
    {
        int Level { get; set; }
        double ExclusionProbability { set; }
        ExamType Type { get; set; }
        ExamPage Start();
    }
}
