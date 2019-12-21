using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Handlers
{
    public class CheckGoDownHandler : ITickable
    {
        private readonly IExclusionCheck _exclusionCheck;
        private readonly IReasonHandler _reasonHandler;
        public CheckGoDownHandler(IExclusionCheck exclusionCheck, IReasonHandler reasonHadler)
        {
            _exclusionCheck = exclusionCheck;
            _reasonHandler = reasonHadler;
        }

        public void Tick()
        {
            if (_exclusionCheck.IsNecessary)
            {
                Check();
            }
        }

        private void Check()
        {
            var exclusionId = _exclusionCheck.CheckForExclusion();
            if (exclusionId != null)
            {
                _reasonHandler.ShowReason(exclusionId);
            }
        }
    }
}
