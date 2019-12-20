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

        event ValueChanged OnStudyScoreChanged;
        event ValueChanged OnPersonalLifeScoreChanged;
        event ValueChanged OnMaxPersonalLifeScoreChanged;
        event ValueChanged OnMaxStudyScoreChanged;
    }
}
