using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class ChecksStore : BaseStore<BaseCheck>
    {
        public ChecksStore(INumberGenerator generator, IPlayContext context)
            : base(GetChecks(context), generator)
        {
        }

        public static List<BaseCheck> GetChecks(IPlayContext context)
        {
            return new List<BaseCheck>()
            {
                new BaseCheck(ExclusionType.PersonalLife, new List<ICheckStep>
                {
                    new StudyScoreExcessPercentCheckStep(context) { Percent = 0.5},
                    new PersonalLifeScoreShortagePercentCheckStep(context) { Percent = 0.5, ExclusionProbability = 0.001 },
                }),
                new BaseCheck(ExclusionType.PersonalLife, new List<ICheckStep>
                {
                    new PersonalLifeScoreShortageCheckStep(context) { MinScore = 0, ExclusionProbability = 1 },
                }) { Priority = 1 },
                new NeedDeductionCheck(context)
                { },
            };
        }
    }
}
