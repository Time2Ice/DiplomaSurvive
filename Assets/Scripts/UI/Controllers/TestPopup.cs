using Assets.Scripts.Enums;
using Assets.Scripts.Handlers;
using Assets.Scripts.UI.Views;
using DefaultNamespace;
using DiplomaSurviveDataGenerator;
using EventAggregator;
using System.Collections.Generic;
using UiScenario;
using UiScenario.Concrete.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Controllers
{
    public class TestPopup : Contractor
    {
        public class Controller : Controller<TestPopupView>, SecondButtonClicked.ISubscribed, FirstButtonClicked.ISubscribed
        {
            private readonly IExamService _examService;
            private IExam _currentExam;
            private ExamPage _currentExamPage;
            public override WindowType Type => WindowType.Test;
            private TestType _type;
            private readonly IPlayerInfoHolder _playerInfoHolder;
            private readonly IReasonHandler _reasonHandler;
            private readonly IExclusionService _exclusionService;

            public Controller(IExamService examService, IPlayerInfoHolder playerInfoHolder, 
                IReasonHandler reasonHadler, IExclusionService exclusionService)
            {
                _playerInfoHolder = playerInfoHolder;
                _examService = examService;
                _reasonHandler = reasonHadler;
                _exclusionService = exclusionService;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                _type = (TestType)callData["Test"];
                switch (_type)
                {
                    case TestType.EIT:
                        _currentExam = _examService.GetEIT();
                        break;
                    case TestType.Session:
                        _currentExam = _examService.GetSession();
                        break;
                }
                _currentExamPage = _currentExam.Start();
                _playerInfoHolder.IsClassroom = false;
                ChangeData();
            }

            void EventAggHub<SecondButtonClicked>.ISubscribed.OnEvent()
            {
                _currentExamPage = _currentExamPage.RightButton.OnClickFunc();
                if (!CheckIfFinal())
                {
                    ChangeData();
                }
            }

            void EventAggHub<FirstButtonClicked>.ISubscribed.OnEvent()
            {
                _currentExamPage = _currentExamPage.LeftButton.OnClickFunc();
                if (!CheckIfFinal())
                {
                    ChangeData();
                }
            }

            private bool CheckIfFinal()
            {
                if (_currentExamPage.Type == ExamPageType.Fail)
                {
                    GoDown();
                    OnClose();
                    return true;
                }
                if (_currentExamPage.Type == ExamPageType.Success)
                {
                    GoUp();
                    OnClose();
                    return true;
                }
                return false;
            }

            private void GoDown()
            {
                _playerInfoHolder.CurrentCourse = 0;
                _playerInfoHolder.Points = 0;
                _playerInfoHolder.UniversityPoints = 0;
                _playerInfoHolder.TasksTaken = 0;
                _playerInfoHolder.PrivateLife = _playerInfoHolder.MaxPrivateLife;
                _playerInfoHolder.IsClassroom = false;
                if (_type == TestType.EIT)
                {
                    _reasonHandler.ShowReason(_exclusionService.GetExclusion(ExclusionType.FailEIT)?.Id);
                }
                if (_type == TestType.Session)
                {
                    _reasonHandler.ShowReason(_exclusionService.GetExclusion(ExclusionType.FailSession)?.Id);
                }
            }

            private void GoUp()
            {
                switch (_type)
                {
                    case TestType.EIT:
                        _playerInfoHolder.University = _playerInfoHolder.University + 1;
                        break;
                    case TestType.Session:
                        _playerInfoHolder.Points = 0;
                        _playerInfoHolder.CurrentCourse = _playerInfoHolder.CurrentCourse+1;
                        break;
                }
                _playerInfoHolder.IsClassroom = true;
            }


            private void ChangeData()
            {
                ConcreteView.SetTestText(_currentExamPage.Title);
                ConcreteView.SetFirstButtonText(_currentExamPage.LeftButton.Title);
                ConcreteView.SetSecondButtonText(_currentExamPage.RightButton.Title);
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Test;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }
        }

        public class SecondButtonClicked : EventAggHub<SecondButtonClicked>
        {
        }

        public class FirstButtonClicked : EventAggHub<FirstButtonClicked>
        {
        }
    }
}
