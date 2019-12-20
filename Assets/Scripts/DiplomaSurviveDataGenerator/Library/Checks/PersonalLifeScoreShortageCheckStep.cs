using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class PersonalLifeScoreDefaultShortageCheckStep : BaseCheckStep
    {
        public double DeductionProbability { get; set; } = 0;

        public PersonalLifeScoreDefaultShortageCheckStep(IPlayContext context) : base(context)
        {
          //  _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
            _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
        }
        protected override bool TryHandle(ref double probability)
        {
           // if (_context.Score.PersonalLifeScore <= _context.Score.MinPersonalLifeScore)
           // {
            //    return false;
          //  }

            probability = DeductionProbability;
            return true;
        }

        public override BaseCheckStep Clone()
        {
            return new PersonalLifeScoreDefaultShortageCheckStep(_context)
            {
                NextStep = _nextStep.Clone(),
                DeductionProbability = DeductionProbability
            };
        }
    }

    public class PersonalLifeScoreShortageCheckStep : BaseCheckStep
    {
        public double DeductionProbability { get; set; } = 1;
        public int MinScore { get; set; } = 0;

        public PersonalLifeScoreShortageCheckStep(IPlayContext context) : base(context)
        {
            _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
        }
        protected override bool TryHandle(ref double probability)
        {
            if (_context.Score.PersonalLifeScore <= MinScore)
            {
                return false;
            }

            probability = DeductionProbability;
            return true;
        }
        public override BaseCheckStep Clone()
        {
            return new PersonalLifeScoreShortageCheckStep(_context)
            {
                NextStep = _nextStep.Clone(),
                DeductionProbability = DeductionProbability,
                MinScore = MinScore
            };
        }
    }
}
