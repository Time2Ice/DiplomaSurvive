using DefaultNamespace;
using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Handlers
{
    public class CheckGoDownHandler : ITickable
    {
        private readonly IExclusionCheck _exclusionCheck;
        private readonly IReasonHandler _reasonHandler;
        private readonly IWindowHandler _windowHandler;
        private readonly IPlayerInfoHolder _playerHolder;
        private readonly IPlayEventsService _eventService;

        public CheckGoDownHandler(IExclusionCheck exclusionCheck, IReasonHandler reasonHadler, IPlayEventsService eventService,
            IWindowHandler windowHandler, IPlayerInfoHolder playerHolder)
        {
            _exclusionCheck = exclusionCheck;
            _reasonHandler = reasonHadler;
            _windowHandler = windowHandler;
            _playerHolder = playerHolder;
            _eventService = eventService;
        }

        public void Tick()
        {
            if (_playerHolder.IsClassroom &&  _exclusionCheck.IsNecessary)
            {
                Debug.Log("Check");
                Check();
            }
            if (_playerHolder.IsClassroom && _eventService.IsAvailable)
            {
                Debug.Log("Play event");
                OpenEventWindow();
            }
        }

        private void Check()
        {
            var exclusionId = _exclusionCheck.CheckForExclusion();
            if (exclusionId != null)
            {
                _playerHolder.IsClassroom = false;
                _reasonHandler.ShowReason(exclusionId);
            }
        }

        private void OpenEventWindow()
        {
            _playerHolder.IsClassroom = false;
            _windowHandler.OpenWindow(UiScenario.Concrete.Data.WindowType.Event);
        }
    }
}
