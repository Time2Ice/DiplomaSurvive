

using System;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using EventAggregator;
using Pool;
using Prefabs;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using Assets.Scripts.Handlers;
using System.Linq;

namespace UI.Controllers
{
    public class Reasons : Contractor
    {
        public class Controller : Controller<ReasonsView>, IPublisherAgg<OpenReasonInfo>, CloseClickEvent.ISubscribed, ChooseReasonEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Reasons;
            private UnityPool _pool;
            private IPlayerInfoHolder _playerInfoHolder;
            private IGameInfoHolder _gameInfoHolder;
            private IReasonHandler _reasonHandler;

            public Controller(UnityPool pool,
                IReasonHandler reasonHandler, IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
            {
                _pool = pool;
                _playerInfoHolder = playerInfoHolder;
                _gameInfoHolder = gameInfoHolder;
                _reasonHandler = reasonHandler;
                _reasonHandler.ReasonOpened += OpenReason;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                ShowReasons(_gameInfoHolder.Reasons);
                ShowProgress(_playerInfoHolder.Reasons.Length, _gameInfoHolder.Reasons.Length);
            }

            private void ShowProgress(int currentValue, int maxValue)
            {
                ConcreteView.SetProgress(currentValue, maxValue);
            }

            private void ShowContinuePossibility(float possibility)
            {
                ConcreteView.SetContinuePossibility("Possibility to stay: " +
                                                    possibility.ToString(CultureInfo.InvariantCulture));
            }

            private void ShowReasons(ReasonDto[] reasons)
            {
                foreach (var reason in reasons)
                {
                    ConcreteView.AddReason(GetReason(reason));
                }
                foreach (var id in _playerInfoHolder.Reasons)
                {
                    ConcreteView.OpenReason(id);
                }
            }

            
            private void OpenReason(string id)
            {
                ConcreteView.OpenReason(id);
                ShowProgress(_playerInfoHolder.Reasons.Length, _gameInfoHolder.Reasons.Length);
            }


            private Reason GetReason(ReasonDto dto)
            {
                var reason = _pool.Pop<Reason>();
                reason.Id = dto.Id;
                reason.Name.text = dto.Name;
                return reason;

            }

            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
                OnClose();
            }

            void EventAggHub<ChooseReasonEvent, string>.ISubscribed.OnEvent(string id)
            {
                var reason = _gameInfoHolder.Reasons.FirstOrDefault(r => r.Id == id);
                Event1().Publish(reason);
                
            }

            public Func<OpenReasonInfo> Event1 { get; set; }
        }

        public new class Scenario : Contractor.Scenario, OpenReasonInfo.ISubscribed
        {
            public override WindowType Type => WindowType.Reasons;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

            void EventAggHub<OpenReasonInfo, ReasonDto>.ISubscribed.OnEvent(ReasonDto value)
            {
                var data = new Dictionary<string, object>
                {
                    {"Reason", value}
                };
                WindowHandler.OpenWindow(WindowType.ReasonInfo, data);
            }
        }


        public class CloseClickEvent : EventAggHub<CloseClickEvent>
            {
            }
            
            public class ChooseReasonEvent : EventAggHub<ChooseReasonEvent, string>
            {
            }
            
            public class OpenReasonInfo:EventAggHub<OpenReasonInfo, ReasonDto>
            {
               
            }
            

        
    }
}