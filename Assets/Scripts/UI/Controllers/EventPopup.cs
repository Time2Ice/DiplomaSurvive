using Assets.Scripts.UI.Views;
using DefaultNamespace;
using DiplomaSurviveDataGenerator;
using EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;
using UiScenario.Concrete.Data;
using UnityEngine;
using static UiScenario.Contractor;

namespace Assets.Scripts.UI.Controllers
{
    public class EventPopup : Contractor
    {
        public class Controller : Controller<EventPopupView>, SelectAction.ISubscribed
        {
            public override WindowType Type => WindowType.Event;
            private readonly IPlayEventsService _eventsStore;
            private PlayEvent _currEvent;
            private readonly IPlayerInfoHolder _playerHolder;
            private IPlayContext _context;

            public Controller(IPlayEventsService eventsStore, IPlayContext playContext, IPlayerInfoHolder playerHolder)
            {
                _eventsStore = eventsStore;
                _context = playContext;
                _playerHolder = playerHolder;
            }
            public override void Open(Dictionary<string, object> callData)
            {
                _playerHolder.IsClassroom = false;
                _currEvent = _eventsStore.GetEvent();
                if (_currEvent != null)
                {
                    ConcreteView.SetEventText(_currEvent.Title);
                }
            }

            public void OnEvent(bool isAccept)
            {
                //Debug.Log("Close event");
                //_playerHolder.IsClassroom = true;
                //OnClose();
                if (_currEvent == null || _currEvent.Buttons.Count < 2)
                {
                    Debug.Log("Close event");
                    _playerHolder.IsClassroom = true;
                    OnClose();
                }
                Page page;
                if (isAccept)
                {
                    page = _currEvent?.Buttons[0]?.OnClickFunc(_context);
                }
                else
                {
                    page = _currEvent?.Buttons[1]?.OnClickFunc(_context);
                }
                ConcreteView.SetEventText(page?.Title);
                Debug.Log(page?.Title);
                _currEvent = null;
            }
        }


        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Event;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }


        }

        public class SelectAction : EventAggHub<SelectAction, bool>
        {
        }
    }
}
