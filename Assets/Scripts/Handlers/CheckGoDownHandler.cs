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
        private IDeductionCheck _deductionCheck;
        CheckGoDownHandler(IDeductionCheck deductionCheck)
        {
            _deductionCheck = deductionCheck;
        }

        public void Tick()
        {
            if (_deductionCheck.IsNecessary) Check();
        }

        private void Check()
        {
           var deduction= _deductionCheck.CheckForDeduction();
           
        }
    }
}
