using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using EventAggregator;
using Pool;
using Prefabs;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using System.Linq;

namespace UI.Controllers
{
    public class Courses : Contractor
    {
        public class Controller : Controller<CoursesView>, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Courses;
            private UnityPool _pool;
            private IPlayerInfoHolder _playerInfoHolder;
            private IGameInfoHolder _gameInfoHolder;

            Controller(UnityPool pool, IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
            {
                _pool = pool;
                _playerInfoHolder = playerInfoHolder;
                _gameInfoHolder = gameInfoHolder;
                _playerInfoHolder.MaxCourseChanged += OpenCourseByNumber;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowCourses(_gameInfoHolder.Courses);
                ShowProgress(_playerInfoHolder.Courses.Length, _gameInfoHolder.Courses.Length);
            }

            private void ShowProgress(int currentValue, int maxValue)
            {
                ConcreteView.SetProgress(currentValue, maxValue);
            }

         
            private void ShowCourses(CourseDto[] courses)
            {
                foreach (var course in courses)
                {
                    ConcreteView.AddCourse(GetCourse(course));
                }
                foreach (var id in _playerInfoHolder.Courses)
                {
                    OpenCourse(id);
                }
            }
            private void OpenCourseByNumber(int num)
            {
                var id = _gameInfoHolder.Courses[num].number;
                var courses = _playerInfoHolder.Courses.ToList();
                courses.Add(id);
                _playerInfoHolder.Courses = courses.ToArray();
                ConcreteView.OpenCourse(id);
                ShowProgress(_playerInfoHolder.Courses.Length, _gameInfoHolder.Courses.Length);

            }


            private void OpenCourse(int id)
            {
                ConcreteView.OpenCourse(id);
            }


            private Course GetCourse(CourseDto dto)
            {
                var course = _pool.Pop<Course>();
                course.Id = dto.number;
                course.Name.text = dto.number.ToString(CultureInfo.InvariantCulture);
                return course;
            }


            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
               OnClose();
            }
        }
        
        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Courses;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

            
        }


        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }
            
        
    }
}