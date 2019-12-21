using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ExamButton: Button<ExamPage>, ICloneable<ExamButton>
    {
        protected INumberGenerator _numberGenerator;
        public double CurrProbability { get; protected set; } = 1;
        public double ExclusionCoefficient { get; set; } = 1;
        public ExamPage NextPage { get; set; }
        public ExamFailPage FailPage { get; set; }
        public ExamSuccessPage SuccessPage { get; set; }
        protected ExamPage NextPageClone
        {
            get
            {
                ICloneable<ExamPage> cloneable = NextPage;
                return cloneable?.Clone() ?? null;
            }
        }
        protected ExamFailPage FailPageClone
        {
            get
            {
                ICloneable<ExamFailPage> cloneable = FailPage;
                return cloneable?.Clone() ?? null;
            }
        }
        protected ExamSuccessPage SuccessPageClone
        {
            get
            {
                ICloneable<ExamSuccessPage> cloneable = SuccessPage;
                return cloneable?.Clone() ?? null;
            }
        }

        public ExamButton(string title = "", double exclusionCoef = 1, INumberGenerator generator = null)
        {
            Title = title;
            ExclusionCoefficient = exclusionCoef;
            _numberGenerator = generator ?? new DefaultNumberGenerator();
        }

        public void SetExclusionProbability(double probability)
        {
            CurrProbability = probability * ExclusionCoefficient;
        }
        public override ExamPage OnClickFunc (IPlayContext context = null)
        {
            base.OnClickFunc(context);
            if (NextPage != null)
            {
                NextPage.SetProbability(CurrProbability);
                return NextPage;
            }

            var probability = _numberGenerator.NextDouble01();
            if (probability > CurrProbability || probability >= 0.999)
            {
                SuccessPage = SuccessPage ?? new ExamSuccessPage();
                return SuccessPage;
            }

            FailPage = FailPage ?? new ExamFailPage();
            return FailPage;
        }
        ExamButton ICloneable<ExamButton>.Clone()
        {
            return new ExamButton(generator: _numberGenerator)
            {
                Title = Title,
                CurrProbability = CurrProbability,
                ExclusionCoefficient = ExclusionCoefficient,
                NextPage = NextPageClone,
                SuccessPage = SuccessPageClone,
                FailPage = FailPageClone,
                ClickFunc = ClickFunc,
                ClickFuncReturn = ClickFuncReturn
            };
        }
    }
}
