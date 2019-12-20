using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiScenario;

namespace Assets.Scripts.Handlers
{
    public class ReasonHandler:IReasonHandler
    {
        public event Action<string> ReasonOpened;
        private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;
        private readonly IWindowHandler _windowHandler;
        public ReasonHandler(IWindowHandler windowHandler, IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
            _windowHandler = windowHandler;
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
            _windowHandler.OpenWindow(UiScenario.Concrete.Data.WindowType.SendDown, data);

        }
    }
}
