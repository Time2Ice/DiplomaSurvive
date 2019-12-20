using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseMainContext : Context, IMainContext
    {
        protected int? _level;
        protected int _coins;
        public virtual int? Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnLevelChanged?.Invoke();
            }
        }
        public virtual int Coins
        {
            get
            {
                return _coins;
            }
            set
            {
                _coins = value;
                OnCoinsChanged?.Invoke();
            }
        }

        public virtual event ValueChanged OnLevelChanged;
        public virtual event ValueChanged OnCoinsChanged;

        public BaseMainContext()
        {
            OnLevelChanged += ContextChanged;
        }
    }
}
