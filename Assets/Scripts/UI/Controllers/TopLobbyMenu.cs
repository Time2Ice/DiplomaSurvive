using System.Collections.Generic;
using System.Globalization;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using Assets.Scripts.Handlers;
using Assets.Scripts.Data;
using System;
using DefaultNamespace;

namespace UI.Controllers
{
    public class TopLobbyMenu : Contractor
    {
        public class Controller : Controller<TopLobbyMenuView>, IDisposable
        {
            private IPlayerInfoHolder _playerInfoHolder;
            private IGameInfoHolder _gameInfoHolder;

            private IExperienceHandler _experienceHandler;
            public override WindowType Type => WindowType.TopLobbyMenu;

            Controller(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder, IExperienceHandler experienceHandler)
            {
                _playerInfoHolder = playerInfoHolder;
                _experienceHandler = experienceHandler;
                _gameInfoHolder = gameInfoHolder;
                _playerInfoHolder.PersonalLifeChanged += SetPersonalLife;
                _experienceHandler.ExperienceChanged += SetMarks;
                _playerInfoHolder.CoinsChanged += ShowBalance;
                _playerInfoHolder.CourseChanged += SetSemester;
                _playerInfoHolder.UniversityChanged += ShowUniversityCount;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                SetPersonalLife(_playerInfoHolder.PrivateLife, _playerInfoHolder.MaxPrivateLife);
                SetMarks(_experienceHandler.Experience, _experienceHandler.MaxExperience);
                ShowBalance(_playerInfoHolder.Coins);
                ShowUniversityCount(_playerInfoHolder.University);
                SetSemester(_playerInfoHolder.CurrentCourse);
            }


            private void ShowBalance(int value)
            {
                ConcreteView.SetBalance(value);
            }

            private void ShowUniversityCount(int value)
            {
                ConcreteView.SetUniversityCount(value);
            }

            private void SetSemester(int value)
            {
                ConcreteView.SetSemester(_gameInfoHolder.Courses[value].number);
            }

            private void SetContinuePossibility(int value)
            {
                ConcreteView.SetContinuePossibility(value);
            }

            private void SetPersonalLife(int currentValue, int maxValue)
            {
                ConcreteView.SetPersonalLife(currentValue, maxValue);
            }

            private void SetMarks(int currentValue, int maxValue)
            {
                ConcreteView.SetMarks(currentValue, maxValue);
            }

            public void Dispose()
            {
                _playerInfoHolder.PersonalLifeChanged -= SetPersonalLife;
                _experienceHandler.ExperienceChanged -= SetMarks;
                _playerInfoHolder.CoinsChanged -= ShowBalance;
                _playerInfoHolder.CourseChanged -= SetSemester;
                _playerInfoHolder.UniversityChanged -= ShowUniversityCount;
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.TopLobbyMenu;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }



        }
    }
}