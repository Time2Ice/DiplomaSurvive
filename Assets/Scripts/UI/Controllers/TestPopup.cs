

using Assets.Scripts.UI.Views;
using DiplomaSurviveDataGenerator;
using EventAggregator;
using System.Collections.Generic;
using UiScenario;
using UiScenario.Concrete.Data;
using UnityEngine;

namespace UI.Controllers
{
   public class TestPopup: Contractor
    {
        public class Controller : Controller<TestPopupView>, SecondButtonClicked.ISubscribed, FirstButtonClicked.ISubscribed
        {
            private IExamService _examServise;
            private IExam _currentExam;
            private ExamPage _currentExamPage;
            public override WindowType Type => WindowType.Test;
            Controller(IExamService examService)
            {
                _examServise = examService;
             
            }
            public override void Open(Dictionary<string, object> callData)
            {
                _currentExam = _examServise.GetEIT();
                _currentExamPage = _currentExam.Start();
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
                    Debug.Log("Failed");                   
                    OnClose();
                    return true;
                }
                if (_currentExamPage.Type == ExamPageType.Success)
                {
                    Debug.Log("Success");
                    OnClose();
                    return true;
                }
                return false;
            }

            private void ChangeData()
            {
                ConcreteView.SetTestText(_currentExamPage.Title);
                ConcreteView.SetFirstButtonText(_currentExamPage.LeftButton.Title);
                ConcreteView.SetSecondButtonText(_currentExamPage.RightButton.Title);

            }

            void EventAggHub<SecondButtonClicked>.ISubscribed.OnEvent()
            {
                
            }

            void EventAggHub<FirstButtonClicked>.ISubscribed.OnEvent()
            {
               
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
