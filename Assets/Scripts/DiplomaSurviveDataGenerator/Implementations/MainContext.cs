using DefaultNamespace;
using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class MainContext : BaseMainContext
    {
        private readonly IPlayerInfoHolder _playerData;

        public MainContext(IPlayerInfoHolder playerData)
        {
            _playerData = playerData;
            _playerData.CoinsChanged += (x) => { OnCoinsChanged?.Invoke(); };
            _playerData.CourseChanged += (x) => { OnLevelChanged?.Invoke(); };
        }

        public override int? Level
        {
            get
            {
                return _playerData.CurrentCourse;
            }
        }
        public override int Coins
        {
            get
            {
                return _playerData.Coins;
            }
            set
            {
                _playerData.Coins = value;
            }
        }

        public override event ValueChanged OnLevelChanged;
        public override event ValueChanged OnCoinsChanged;
    }
}
