using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class PersonalLifeScoreShortageCheckStep : BaseCheckStep
    {
        public double ExclusionProbability { get; set; } = 1;
        public int MinScore { get; set; } = 0;

        public PersonalLifeScoreShortageCheckStep(IPlayContext context) : base(context)
        {
            _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
        }
        protected override bool TryHandle(ref double probability)
        {
            if (_context.Score.PersonalLifeScore <= MinScore)
            {
                probability = ExclusionProbability;
                return false;
            }
            return true;
        }
        public override BaseCheckStep Clone()
        {
            return new PersonalLifeScoreShortageCheckStep(_context)
            {
                NextStep = _nextStep.Clone(),
                ExclusionProbability = ExclusionProbability,
                MinScore = MinScore
            };
        }
        public override void AskForCheck()
        {
            base.AskForCheck();
        }
    }

    public class PersonalLifeScoreShortagePercentCheckStep : BaseCheckStep
    {
        public double ExclusionProbability { get; set; } = 1;
        public double Percent { get; set; } = int.MaxValue;
        public PersonalLifeScoreShortagePercentCheckStep(IPlayContext context) : base(context)
        {
            _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
            _context.Score.OnMaxPersonalLifeScoreChanged += AskForCheck;
        }

        protected override bool TryHandle(ref double probability)
        {
            if (_context.Score.PersonalLifeScore <= _context.Score.MaxPersonalLifeScore * Percent)
            {
                probability = ExclusionProbability;
                return false;
            }

            return true;
        }

        public override void AskForCheck()
        {
            base.AskForCheck();
        }
    }
}
