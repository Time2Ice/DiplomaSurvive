

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

namespace UI.Controllers
{
    public class Reasons : Contractor
    {
        public class Controller : Controller<ReasonsView>, IPublisherAgg<OpenReasonInfo>, CloseClickEvent.ISubscribed, ChooseReasonEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Reasons;
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
            }

            private void ChangeReason(ReasonDto reason)
            {
                //todo get sprite here and call view function
            }


            private Reason GetReason(ReasonDto dto)
            {
                var reason = _pool.Pop<Reason>();
                reason.Id = dto.Id;
                reason.Name.text = dto.Name;
                //todo add sprite
                return reason;

            }

            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
                OnClose();
            }

            void EventAggHub<ChooseReasonEvent, string>.ISubscribed.OnEvent(string id)
            {
                ReasonDto reason=new ReasonDto();
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
                WindowHandler.OpenWindow(WindowType.ReasonInfo);
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