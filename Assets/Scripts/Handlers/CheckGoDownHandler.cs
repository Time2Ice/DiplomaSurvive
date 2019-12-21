using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Handlers
{
    class CheckGoDownHandler : ITickable
    {
        private IExclusionCheck _exclusionCheck;
        CheckGoDownHandler(IExclusionCheck exclusionCheck)
        {
            _exclusionCheck = exclusionCheck;
        }

        public void Tick()
        {
            if (_exclusionCheck.IsNecessary) Check();
        }

        private void Check()
        {
           var exclusion= _exclusionCheck.CheckForExclusion();
           
        }
    }
}
