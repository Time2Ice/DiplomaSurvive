using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handlers
{
    public class ReasonHandler:IReasonHandler
    {
        public event Action<string> ReasonOpened;
        private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;
        public ReasonHandler(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
        }
        
        public void OpenReason(string id)
        {
            var playerReasons = _playerInfoHolder.Reasons.ToList();
            playerReasons.Add(id);
            _playerInfoHolder.Reasons=playerReasons.ToArray();
            ReasonOpened?.Invoke(id);
        }
    }
}
