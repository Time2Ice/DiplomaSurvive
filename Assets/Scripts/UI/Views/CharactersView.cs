using System;
using Assets.Scripts.Prefabs;
using Assets.Scripts.UI.Controllers;
using EventAggregator;
using UiScenario;
using UnityEngine;


namespace Assets.Scripts.UI.Views
{
    public class CharactersView: Contractor.View,  IPublisherAgg<Characters.HeroClickedEvent, Characters.TeacherClickedEvent>
    {
        [SerializeField] private Character[] _teachers;
        [SerializeField] private Character _hero;
        [SerializeField] private Transform _tasksArea;

        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            Infrastructure.BinderAgg.Bind(this);
            for (var i = 0; i < _teachers.Length; i++)
            {
                BindTeacherButton(i);
            }
            _hero.Button.onClick.AddListener(()=>Event1().Publish());
          
        }

        private void BindTeacherButton(int i)
        {
           _teachers[i].Button.onClick.AddListener(()=>Event2().Publish(i));
        }

        public Func<Characters.HeroClickedEvent> Event1 { get; set; }
        public Func<Characters.TeacherClickedEvent> Event2 { get; set; }

        public void ShowTaskMessage(int teacherNum, TaskMessage message)
        {
           message.Transform.SetParent(_teachers[teacherNum].MessageArea);
           message.Transform.localPosition = Vector3.zero;
        }

        public void ShowTask(Task task)
        {
           task.Transform.SetParent(_tasksArea); 
        }

      
    }
}
