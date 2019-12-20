using DefaultNamespace;
using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class ScoreContext : BaseScoreContext
    {
        private readonly IGameInfoHolder _gameData;
        private readonly IPlayerInfoHolder _playerData;

        public override event ValueChanged OnStudyScoreChanged;
        public override event ValueChanged OnPersonalLifeScoreChanged;
        public override event ValueChanged OnMaxPersonalLifeScoreChanged;
        public override event ValueChanged OnMaxStudyScoreChanged;

        public ScoreContext(IPlayerInfoHolder playerData, IGameInfoHolder gameData)
        {
            _gameData = gameData;
            _playerData = playerData;
            _playerData.PointsChanged += (x) => { OnStudyScoreChanged?.Invoke(); };
            _playerData.CourseChanged += (x) => { OnMaxStudyScoreChanged?.Invoke(); };
            _playerData.MaxPrivateLifeChanged += (x) => { OnMaxPersonalLifeScoreChanged?.Invoke(); };
            _playerData.PersonalLifeChanged += (x, y) => { OnPersonalLifeScoreChanged?.Invoke(); };
        }

        public override int StudyScore
        {
            get { return _playerData.Points; }
            set
            {
                _playerData.Points = value;
            }
        }
        public override int PersonalLifeScore
        {
            get { return _playerData.PrivateLife; }
            set
            {
                _playerData.PrivateLife = value;
            }
        }
        public override int MaxPersonalLifeScore
        {
            get { return _playerData.MaxPrivateLife; }
            set
            {
                _playerData.MaxPrivateLife = value;
            }
        }
        public override int MaxStudyScore
        {
            get 
            {
                return _gameData.Courses[_playerData.CurrentCourse].points_to_next;
            }
        }
    }
}
