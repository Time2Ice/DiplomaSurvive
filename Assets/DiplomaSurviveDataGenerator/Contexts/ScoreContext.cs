using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class ScoreContext : Context
    {
        protected int _studyScore;
        protected int _personalLifeScore;
        protected int _maxPersonalLifeScore;
        protected int _minPersonalLifeScore;
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
        public virtual int MinPersonalLifeScore
        {
            get { return _minPersonalLifeScore; }
            set
            {
                _minPersonalLifeScore = value;
                OnMinPersonalLifeScoreChanged?.Invoke();
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

        public event ValueChanged OnStudyScoreChanged;
        public event ValueChanged OnPersonalLifeScoreChanged;
        public event ValueChanged OnMaxPersonalLifeScoreChanged;
        public event ValueChanged OnMinPersonalLifeScoreChanged;
        public event ValueChanged OnMaxStudyScoreChanged;

        public ScoreContext()
        {
            OnStudyScoreChanged += ContextChanged;
            OnPersonalLifeScoreChanged += ContextChanged;
            OnMaxPersonalLifeScoreChanged += ContextChanged;
            OnMinPersonalLifeScoreChanged += ContextChanged;
            OnMaxStudyScoreChanged += ContextChanged;
        }
    }
}
