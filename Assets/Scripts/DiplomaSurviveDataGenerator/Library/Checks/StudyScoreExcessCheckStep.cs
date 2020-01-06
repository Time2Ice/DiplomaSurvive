using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class StudyScoreExcessCheckStep : BaseCheckStep
    {
        public double ExclusionProbability { get; set; } = 1;
        public int MaxScore { get; set; } = int.MaxValue;
        public StudyScoreExcessCheckStep(IPlayContext context) : base(context)
        {
            _context.Score.OnPersonalLifeScoreChanged += AskForCheck;
            _context.Score.OnMaxPersonalLifeScoreChanged += AskForCheck;
        }

        protected override bool TryHandle(ref double probability)
        {
            if (_context.Score.PersonalLifeScore >= MaxScore)
            {
                probability = ExclusionProbability;
                return false;
            }

            return true;
        }
    }

    public class StudyScoreExcessPercentCheckStep : BaseCheckStep
    {
        public double ExclusionProbability { get; set; } = 1;
        public double Percent { get; set; } = int.MaxValue;
        public StudyScoreExcessPercentCheckStep(IPlayContext context) : base(context)
        {
            var s = _context.Score;
            _context.Score.OnStudyScoreChanged += AskForCheck;
            _context.Score.OnMaxStudyScoreChanged += AskForCheck;
        }

        protected override bool TryHandle(ref double probability)
        {
            if (_context.Score.StudyScore >= _context.Score.MaxStudyScore * Percent)
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
