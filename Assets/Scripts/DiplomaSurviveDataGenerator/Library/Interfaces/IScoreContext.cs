using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{

    public interface IScoreContext : IContext
    {
        int StudyScore { get; set; }
        int PersonalLifeScore { get; set; }
        int MaxPersonalLifeScore { get; set; }
        int MaxStudyScore { get; set; }

        public event ValueChanged OnStudyScoreChanged;
        public event ValueChanged OnPersonalLifeScoreChanged;
        public event ValueChanged OnMaxPersonalLifeScoreChanged;
        public event ValueChanged OnMaxStudyScoreChanged;
    }
}
