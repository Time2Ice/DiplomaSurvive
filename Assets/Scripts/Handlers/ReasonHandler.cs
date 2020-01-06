using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;

namespace Assets.Scripts.Handlers
{
    public class ReasonHandler : IReasonHandler
    {
        public event Action<string> ReasonOpened;
        private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;
        public IWindowHandler WindowHandler {get;set;}
        public ReasonHandler(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
           
        }
        
        public void ShowReason(string id)
        {
            var playerReasons = _playerInfoHolder.Reasons.ToList();
            if (!playerReasons.Contains(id))
            {
                playerReasons.Add(id);
                _playerInfoHolder.Reasons = playerReasons.ToArray();
                ReasonOpened?.Invoke(id);
            }
            var reason = _gameInfoHolder.Reasons.FirstOrDefault(r => r.Id == id);
            OpenReasonWindow(reason);
        }

        private void OpenReasonWindow(ReasonDto reason)
        {
            var data = new Dictionary<string, object>
                {
                    {"GoDownReason", reason}
                };
            WindowHandler.OpenWindow(UiScenario.Concrete.Data.WindowType.SendDown, data);

        }
    }
}
