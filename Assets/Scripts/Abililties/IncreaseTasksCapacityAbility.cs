using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abililties
{
    public class IncreaseTasksCapacityAbility : IAbility
    {
        public int Id { get; set; }

        IPlayerInfoHolder _playerInfoHolder;
        public IncreaseTasksCapacityAbility(IPlayerInfoHolder playerInfoHolder)
        {
            _playerInfoHolder = playerInfoHolder;
        }

        public void UseAbility()
        {
            _playerInfoHolder.TaskQueueCapasity = _playerInfoHolder.TaskQueueCapasity + 10;
        }
    }
}
