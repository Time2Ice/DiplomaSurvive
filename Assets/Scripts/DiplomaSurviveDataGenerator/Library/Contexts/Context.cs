using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public abstract class Context : IContext
    {
        public virtual event ValueChanged OnContextChanged;

        protected void ContextChanged()
        {
            OnContextChanged?.Invoke();
        }
    }
}
