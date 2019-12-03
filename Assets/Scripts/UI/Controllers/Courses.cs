using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using EventAggregator;
using Pool;
using Prefabs;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class Courses : Contractor
    {
        public class Controller : Controller<CoursesView>, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Courses;
            private UnityPool _pool;

            Controller(UnityPool pool)
            {
                _pool = pool;
            }

            public override void Open(Dictionary<string, object> callData)
            {

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
            }

            private void ChangeCourse(CourseDto reason)
            {
                //todo get sprite here and call view function
            }


            private Course GetCourse(CourseDto dto)
            {
                var course = _pool.Pop<Course>();
                course.Id = dto.number;
                course.Name.text = dto.number.ToString(CultureInfo.InvariantCulture);
                //todo add sprite
                return course;

            }


            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
               OnClose();
            }
        }
        
        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Reasons;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

            
        }


        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }
            
        
    }
}