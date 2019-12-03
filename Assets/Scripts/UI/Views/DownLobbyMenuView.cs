using System;
using EventAggregator;
using TMPro;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class DownLobbyMenuView : Contractor.View, IPublisherAgg<DownLobbyMenu.OpenWindowEvent>
    {

        [SerializeField] private Button _workButton;
        [SerializeField] private Button _abilitiesButton;
        [SerializeField] private Button _luckButton;
        [SerializeField] private Button _recordBookButton;

        [SerializeField] private GameObject _workAlert;
        [SerializeField] private GameObject _abilitiesAlert;
        [SerializeField] private GameObject _luckAlert;
        [SerializeField] private GameObject _recordAlert;
 
        public Func<DownLobbyMenu.OpenWindowEvent> Event1 { get; set; }
         public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

             Infrastructure.BinderAgg.Bind(this);
             _abilitiesButton.onClick.AddListener(()=>Event1().Publish(WindowType.Abilities));
             _recordBookButton.onClick.AddListener(()=>Event1().Publish(WindowType.RecordBook));
        }

        public void SetWorkState(bool isAlerted)
        {
            _workAlert.SetActive(isAlerted);
        }
        
        public void SetAbilitiesState(bool isAlerted)
        {
            _abilitiesAlert.SetActive(isAlerted);
        }
        
        public void SetLuckState(bool isAlerted)
        {
            _luckAlert.SetActive(isAlerted);
        }
        
        public void SetRecordState(bool isAlerted)
        {
            _recordAlert.SetActive(isAlerted);
        }

      
    }

}