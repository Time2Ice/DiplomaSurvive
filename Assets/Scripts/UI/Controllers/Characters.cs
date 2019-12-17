using System;
using System.Collections.Generic;
using Assets.Scripts.Handlers;
using Assets.Scripts.Prefabs;
using Assets.Scripts.UI.Views;
using EventAggregator;
using Pool;
using UiScenario;
using UiScenario.Concrete.Data;

namespace Assets.Scripts.UI.Controllers
{
    public class Characters : Contractor
    {
        public class Controller : Controller<CharactersView>, HeroClickedEvent.ISubscribed,
            TeacherClickedEvent.ISubscribed, IDisposable
        {
            public override WindowType Type => WindowType.Characters;
            private readonly UnityPool _pool;
            private TaskMessage[] _taskMessages;
            private ITaskMessagesHandler _taskMessagesHandler;
            private IPersonalLifeHandler _personalLifeHandler;

            private Queue<Task> _tasks=new Queue<Task>();

            Controller(UnityPool pool, ITaskMessagesHandler taskMessagesHandler, IPersonalLifeHandler personalLifeHandler)
            {
                _pool = pool;
                _taskMessages=new TaskMessage[3];
                _taskMessagesHandler = taskMessagesHandler;
                _personalLifeHandler = personalLifeHandler;
                _taskMessagesHandler.AddMessage += ShowTaskMessage;
                _taskMessagesHandler.CompleteTask += HideTask;

            }

            public override void Open(Dictionary<string, object> callData)
            {

            }

            private void ShowTaskMessage(int teacherNum)
            {
                var message = _pool.Pop<TaskMessage>();
                _taskMessages[teacherNum] = message;
                ConcreteView.ShowTaskMessage(teacherNum, message);
            }

            private void HideTaskMessage(int teacherNum)
            {
                if (_taskMessages[teacherNum] != null)
                {
                    _taskMessages[teacherNum].Push();
                    _taskMessages[teacherNum] = null;
                }
            }

            private void ShowTask()
            {
                var task = _pool.Pop<Task>();
                _tasks.Enqueue(task);
                ConcreteView.ShowTask(task);
            }

            private void HideTask()
            {
                if (_tasks.Count>0)
                {
                    _tasks.Dequeue().Push();
                }
            }

            void EventAggHub<HeroClickedEvent>.ISubscribed.OnEvent()
            {
                _personalLifeHandler.ReducePrivateLife();
            }

            void EventAggHub<TeacherClickedEvent, int>.ISubscribed.OnEvent(int value)
            {
                if (_taskMessages[value]!=null&&_taskMessagesHandler.CheckTaskTakePossibility())
                {
                    _taskMessagesHandler.TakeTask(value);
                    HideTaskMessage(value);
                    ShowTask();
                }
            }

            public void Dispose()
            {
                _taskMessagesHandler.AddMessage -= ShowTaskMessage;
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Characters;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }
        }

        public class HeroClickedEvent : EventAggHub<HeroClickedEvent>
        {

        }

        public class TeacherClickedEvent : EventAggHub<TeacherClickedEvent, int>
        {

        }
    }
}
