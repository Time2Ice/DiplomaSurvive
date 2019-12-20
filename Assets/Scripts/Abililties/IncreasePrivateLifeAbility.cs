using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abililties
{
    public class IncreasePrivateLifeAbility:IAbility
    {
        public int Id { get; set; }
        IPlayerInfoHolder _playerInfoHolder;
        public IncreasePrivateLifeAbility(IPlayerInfoHolder playerInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
        }

        public void UseAbility()
        {
            _playerInfoHolder.MaxPrivateLife = _playerInfoHolder.MaxPrivateLife + 20;
        }
    }
}
