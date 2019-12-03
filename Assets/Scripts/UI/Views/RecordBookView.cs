using System;
using System.Globalization;
using EventAggregator;
using TMPro;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class RecordBookView:Contractor.View, IPublisherAgg<RecordBook.CloseClickEvent, RecordBook.OpenWindowEvent>
    {
    [SerializeField] private TMP_Text _universityCountText;
    [SerializeField] private TMP_Text _currentCourseText;
    [SerializeField] private TMP_Text _maximalCourseText;
    
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _coursesButton;
    [SerializeField] private Button _reasonsButton;
    public Func<RecordBook.CloseClickEvent> Event1 { get; set; }
        public Func<RecordBook.OpenWindowEvent> Event2 { get; set; }

        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            Infrastructure.BinderAgg.Bind(this);
            
            _closeButton.onClick.AddListener(()=>Event1().Publish());
            _coursesButton.onClick.AddListener(()=>Event2().Publish(WindowType.Courses));
            _reasonsButton.onClick.AddListener(()=>Event2().Publish(WindowType.Reasons));

        }

        public void SetUniversityCount(int value)
    {
        _universityCountText.text = value.ToString(CultureInfo.InvariantCulture);
    }
    public void SetCurrentCourse(int value)
    {
        _currentCourseText.text = value.ToString(CultureInfo.InvariantCulture);
    }
    public void SetMaxCourse(int value)
    {
        _maximalCourseText.text = value.ToString(CultureInfo.InvariantCulture);
    }

    
    }
}