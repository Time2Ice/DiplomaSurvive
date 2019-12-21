﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return false;
            }

            probability = ExclusionProbability;
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
    }
}
