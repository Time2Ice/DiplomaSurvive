using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class TimeContext : Context
    {
        protected double _currentChanged;
        protected double _lastTaskChanged;
        protected double _lastClickChanged;
        public virtual double LastTask
        {
            get { return _lastTaskChanged; }
            set
            {
                _lastTaskChanged = value;
                OnLastTaskChanged?.Invoke();
            }
        }
        public virtual double LastClick
        {
            get { return _lastClickChanged; }
            set
            {
                _lastClickChanged = value;
                OnLastClickChanged?.Invoke();
            }
        }
        public virtual double Current
        {
            get { return _currentChanged; }
            set
            {
                _currentChanged = value;
                OnCurrentChanged?.Invoke();
            }
        }
        public event ValueChanged OnLastTaskChanged;
        public event ValueChanged OnLastClickChanged;
        public event ValueChanged OnCurrentChanged;

        public TimeContext()
        {
            OnLastClickChanged += ContextChanged;
            OnLastTaskChanged += ContextChanged;
            OnCurrentChanged += ContextChanged;
        }
    }
}
