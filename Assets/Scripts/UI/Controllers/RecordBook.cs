using System.Collections.Generic;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using DefaultNamespace;

namespace UI.Controllers
{
    public class RecordBook : Contractor
    {
        public class Controller : Controller<RecordBookView>, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.RecordBook;

            private IPlayerInfoHolder _playerInfoHolder;
            private IGameInfoHolder _gameInfoHolder;
            Controller(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
            {
                _playerInfoHolder = playerInfoHolder;
                _gameInfoHolder = gameInfoHolder;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowUniversityCount(_playerInfoHolder.University);
                ShowCurrentCourse(_gameInfoHolder.Courses[_playerInfoHolder.CurrentCourse].number);
                ShowMaxCourse(_gameInfoHolder.Courses[_playerInfoHolder.MaxCourse].number);
            }

            private void ShowUniversityCount(int value)
            {
                ConcreteView.SetUniversityCount(value);

            }

            private void ShowCurrentCourse(int value)
            {
                ConcreteView.SetCurrentCourse(value);

            }

            private void ShowMaxCourse(int value)
            {
                ConcreteView.SetMaxCourse(value);
            }


            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
                OnClose();
            }
        }

        public new class Scenario : Contractor.Scenario, OpenWindowEvent.ISubscribed
        {
            public override WindowType Type => WindowType.RecordBook;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

            void EventAggHub<OpenWindowEvent, WindowType>.ISubscribed.OnEvent(WindowType value)
            {
                WindowHandler.OpenWindow(value);
            }
        }

        public class OpenWindowEvent : EventAggHub<OpenWindowEvent, WindowType>
        {
        }

        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }

    }
}
