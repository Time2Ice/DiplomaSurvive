using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using DefaultNamespace;
using UiScenario;

namespace Assets.Scripts.Handlers
{
    public class ExperienceHandler:IExperienceHandler
    {
        public event Action<int, int> ExperienceChanged;
        private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;
        private readonly IWindowHandler _windowHandler;
        private readonly IReasonHandler _reasonHandler;

        public int MaxExperience => _gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next;
        public int Experience => _playerInfoHolder.Points;


        public ExperienceHandler(IReasonHandler reasonHandler, IWindowHandler windowHandler,IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
        {
            _windowHandler = windowHandler;
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
            _reasonHandler = reasonHandler;
            _reasonHandler.WindowHandler = _windowHandler;
            if(_playerInfoHolder.UniversityPoints==0)
            {
                OpenTestWindow(TestType.EIT);
            }
        }

        public void ChangeExperience()
        {
            _playerInfoHolder.UniversityPoints= _playerInfoHolder.UniversityPoints+_gameInfoHolder.TaskCompletePoints;
            _playerInfoHolder.Points = _playerInfoHolder.Points+_gameInfoHolder.TaskCompletePoints;
            if(_playerInfoHolder.Points>_gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next&&
                _gameInfoHolder.Courses.Length>_playerInfoHolder.CurrentCourse+1)
            {
                OpenTestWindow(TestType.Session);
            }
            ExperienceChanged?.Invoke(_playerInfoHolder.Points,_gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].points_to_next);

        }
      

        private void OpenTestWindow(TestType type)
        {
            var data = new Dictionary<string, object>
                {
                    {"Test", type}
                };
            _windowHandler.OpenWindow(UiScenario.Concrete.Data.WindowType.Test, data);

        }
    }
}
