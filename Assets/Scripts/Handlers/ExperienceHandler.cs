using System;
using Assets.Scripts.Data;
using DefaultNamespace;

namespace Assets.Scripts.Handlers
{
    public class ExperienceHandler:IExperienceHandler
    {
        public event Action<int, int> ExperienceChanged;
        private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;


        public int MaxExperience => _gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next;
        public int Experience => _playerInfoHolder.Points;


        public ExperienceHandler(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
        }

        public void ChangeExperience()
        {
            _playerInfoHolder.Points = _playerInfoHolder.Points+_gameInfoHolder.TaskCompletePoints;
            if(_playerInfoHolder.Points>_gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next&& _gameInfoHolder.Courses.Length>_playerInfoHolder.CurrentCourse+1)
            {
                _playerInfoHolder.Points = _playerInfoHolder.Points -
                                           _gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next;
                _playerInfoHolder.CurrentCourse = _playerInfoHolder.CurrentCourse + 1;
                
            }
            ExperienceChanged?.Invoke(_playerInfoHolder.Points,_gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next);

        }
    }
}
