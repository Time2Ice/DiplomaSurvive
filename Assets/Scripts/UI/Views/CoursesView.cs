using System;
using System.Collections.Generic;
using System.Globalization;
using EventAggregator;
using Prefabs;
using TMPro;
using UiScenario;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class CoursesView:Contractor.View, IPublisherAgg<Courses.CloseClickEvent>
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private GridLayoutGroup _coursesArea;
        [SerializeField] private Button _closeButton;

        private Dictionary<int, Course> _courses=new Dictionary<int, Course>();
        
        public Func<Courses.CloseClickEvent> Event1 { get; set; }
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

             Infrastructure.BinderAgg.Bind(this);
             _closeButton.onClick.AddListener(()=>Event1().Publish());
        }

        public void SetProgress(int currentValue, int maxValue)
        {
            _progressSlider.value = (float) currentValue / maxValue;
            _progressText.text = currentValue.ToString(CultureInfo.InvariantCulture) +
                                 "/" + maxValue.ToString(CultureInfo.InvariantCulture);
        }

        public void AddCourse(Course course)
        {
            if (_courses.ContainsKey(course.Id)) return;
            course.transform.SetParent(_coursesArea.transform);
            _courses.Add(course.Id, course);
        }
        
        public void ChangeCourse(int id, Sprite image, string name)
        {
            var courseToChange = _courses[id];
            courseToChange.Image.sprite = image;
            courseToChange.Name.text = name;
        }

      
    }
}