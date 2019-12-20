using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseScoreContext : Context, IScoreContext
    {
        protected int _studyScore;
        protected int _personalLifeScore;
        protected int _maxPersonalLifeScore;
        protected int _maxStudyScore;
        public virtual int StudyScore
        {
            get { return _studyScore; }
            set
            {
                _studyScore = value;
                OnStudyScoreChanged?.Invoke();
            }
        }
        public virtual int PersonalLifeScore
        {
            get { return _personalLifeScore; }
            set
            {
                _personalLifeScore  = value;
                OnPersonalLifeScoreChanged?.Invoke();
            }
        }
        public virtual int MaxPersonalLifeScore
        {
            get { return _maxPersonalLifeScore; }
            set
            {
                _maxPersonalLifeScore = value;
                OnMaxPersonalLifeScoreChanged?.Invoke();

            }
        }
        public virtual int MaxStudyScore
        {
            get { return _maxStudyScore; }
            set
            {
                _maxStudyScore = value;
                OnMaxStudyScoreChanged?.Invoke();
            }
        }

        public virtual event ValueChanged OnStudyScoreChanged;
        public virtual event ValueChanged OnPersonalLifeScoreChanged;
        public virtual event ValueChanged OnMaxPersonalLifeScoreChanged;
        public virtual event ValueChanged OnMaxStudyScoreChanged;

        public BaseScoreContext()
        {
            OnStudyScoreChanged += ContextChanged;
            OnPersonalLifeScoreChanged += ContextChanged;
            OnMaxPersonalLifeScoreChanged += ContextChanged;
            OnMaxStudyScoreChanged += ContextChanged;
        }
    }
}
