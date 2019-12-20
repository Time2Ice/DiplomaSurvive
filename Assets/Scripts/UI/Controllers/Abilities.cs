using System;
using System.Collections.Generic;
using DefaultNamespace;
using EventAggregator;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using Assets.Scripts.Abililties;
using System.Linq;
using UnityEngine;

namespace UI.Controllers
{
    public class Abilities:Contractor
    {
          public class Controller : Controller<AbilitiesView>, IDisposable, BuyClickEvent.ISubscribed, CloseClickEvent.ISubscribed
        {
            public override WindowType Type => WindowType.Abilities;
            private IAbility[] _abilities;
            private IPlayerInfoHolder _playerInfoHolder;
            private IGameInfoHolder _gameInfoHolder;

            Controller(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
            {
                _playerInfoHolder = playerInfoHolder;
                _gameInfoHolder = gameInfoHolder;
                _abilities = new IAbility[]
                {
                   new IncreasePrivateLifeAbility(_playerInfoHolder),
                   new IncreaseTasksCapacityAbility(_playerInfoHolder)
               };
                _abilities[0].Id = _gameInfoHolder.Abilities[0].id;
                _abilities[1].Id = _gameInfoHolder.Abilities[1].id;

                _playerInfoHolder.CoinsChanged += ShowCoins;
            }
            public override void Open(Dictionary<string, object> callData)
            {
                
                ShowAbilities(_gameInfoHolder.Abilities);
                ShowCoins(_playerInfoHolder.Coins);
                foreach(var ability in _abilities)
                {
                    if(_playerInfoHolder.Abilities.Contains(ability.Id))
                    {
                        SetBoughtAbility(ability.Id);
                    }
                }
            }
            
            

            private void ShowAbilities(AbilityDto[] abilities)
            {
               ConcreteView.SetAbilitiesData(abilities);
            }

            private void ShowCoins(int coins)
            {
                ConcreteView.SetBalance(coins);
            }
            private void SetBoughtAbility(int id)
            {
                ConcreteView.SetBoughtAbility(id);
            }


            void EventAggHub<BuyClickEvent, int>.ISubscribed.OnEvent(int value)
            {
                Debug.Log(value);
                var ability = _gameInfoHolder.Abilities.FirstOrDefault(a => a.id == value);
                if (_playerInfoHolder.Coins>=ability.price)
                {
                    _abilities.FirstOrDefault(a => a.Id == value).UseAbility();
                    _playerInfoHolder.Coins = _playerInfoHolder.Coins - ability.price;
                    AddAbilityToOwned(value);
                   SetBoughtAbility(value);
                }
            }

            private void AddAbilityToOwned(int id)
            {
                var abilities = _playerInfoHolder.Abilities.ToList();
                abilities.Add(id);
                _playerInfoHolder.Abilities = abilities.ToArray();

            }

            void EventAggHub<CloseClickEvent>.ISubscribed.OnEvent()
            {
               OnClose();
            }

            public void Dispose()
            {
                _playerInfoHolder.CoinsChanged -= ShowCoins;
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
        