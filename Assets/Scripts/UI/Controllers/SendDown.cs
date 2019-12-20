using System.Collections.Generic;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using UnityEngine;
using DefaultNamespace;
using UnityEngine.SceneManagement;

namespace UI.Controllers
{
    public class SendDown : Contractor
    {
        public class Controller : Controller<SendDownView>, RestudyEvent.ISubscribed
        {
            IPlayerInfoHolder _playerInfoHolder;

            public override WindowType Type => WindowType.SendDown;

            Controller(IPlayerInfoHolder playerInfoHolder)
            {
                _playerInfoHolder = playerInfoHolder;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowData("Reason", "Description");
            }

            private void ShowData(string name, string description)
            {
                ConcreteView.SetReasonData(name, description);
            }

            private void ShowImage(Sprite sprite)
            {
                ConcreteView.SetReasonImage(sprite);
            }

            private void ShowStipendium(int value)
            {
                ConcreteView.SetStipendium(value);
            }

            private void ShowCourse(int value)
            {
                ConcreteView.SetCourse(value);
            }

            private void ShowContinuePossibility(int value)
            {
                ConcreteView.SetContinuePossibility(value);
            }

            void EventAggHub<RestudyEvent>.ISubscribed.OnEvent()
            {
                GoDown();
            }

            private void GoDown()
            {
                _playerInfoHolder.CurrentCourse = 0;
                _playerInfoHolder.Points = 0;
                _playerInfoHolder.UniversityPoints = 0;
                _playerInfoHolder.TasksTaken = 0;
                _playerInfoHolder.PrivateLife = _playerInfoHolder.MaxPrivateLife;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.SendDown;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }



        }

        public class RestudyEvent : EventAggHub<RestudyEvent>
        {
        }

    }
}
  