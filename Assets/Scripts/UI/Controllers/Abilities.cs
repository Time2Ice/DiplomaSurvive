using System;
using System.Collections.Generic;
using DefaultNamespace;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;

namespace UI.Controllers
{
    public class Abilities:Contractor
    {
          public class Controller : Controller<AbilitiesView>, IDisposable, BuyClickEvent.ISubscribed, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Abilities;           
           
            Controller()
            {
              
            }
            public override void Open(Dictionary<string, object> callData)
            {
                var abilities = new AbilityDto[]
                {
                    new AbilityDto() {id = 1, name = "first", price = 100},
                    new AbilityDto() {id = 1, name = "first", price = 100},
                    new AbilityDto() {id = 1, name = "first", price = 100}
                };
                ShowAbilities(abilities);
                ShowCoins(900);
            }
            
            

            private void ShowAbilities(AbilityDto[] abilities)
            {
               ConcreteView.SetAbilitiesData(abilities);
            }

            private void ShowCoins(int coins)
            {
                ConcreteView.SetBalance(coins);
            }


            void EventAggHub<BuyClickEvent, int>.ISubscribed.OnEvent(int value)
            {
                //todo if player balance is enough, buy ability, else show popup or do nothing
            }

            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
               OnClose();
            }

            public void Dispose()
            {
                
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.Abilities;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }

      
        }

        public class BuyClickEvent : EventAggHub<BuyClickEvent, int>
        {
        }
        
        public class CloseClickEvent : EventAggHub<CloseClickEvent>
        {
        }
        
        
    }
}
        