using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ExamTree : IExam, ICloneable<ExamTree>
    {
        public ExamPage RootPage { get; set; }
        public int Level { get; set; }
        public ExamType Type { get; set; }
        public double DeductionProbability { set; protected get; } = 0.5;

        protected ExamPage RootPageClone
        {
            get
            {
                ICloneable<ExamPage> cloneable = RootPage;
                return cloneable.Clone();
            }
        }

        public virtual object ShallowCopy()
        {
            return MemberwiseClone();
        }
        public virtual ExamPage Start()
        {
            RootPage.SetProbability(DeductionProbability);
            return RootPage;
        }
        IExam ICloneable<IExam>.Clone()
        {
            return (this as ICloneable<ExamTree>).Clone();
        }
        ExamTree ICloneable<ExamTree>.Clone()
        {
            return new ExamTree
            {
                RootPage = RootPageClone,
                Level = Level,
                Type = Type,
                DeductionProbability = DeductionProbability
            };
        }
    }
}
