using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class BaseCheck : ICheck, ICloneable<BaseCheck>
    {
        private ICheckStep _checkChain;
        public int Priority { get; set; } = int.MaxValue;
        public bool IsDirty { get; private set; } = true;
        [field:NonSerialized]
        public virtual event ValueChanged OnDirty;
        public ExclusionType ExclusionType { get; set; } = ExclusionType.Undefined;
        public ICheckStep CheckChain 
        { 
            protected get
            {
                return _checkChain;
            }
            set
            {
                if (_checkChain != null)
                {
                    _checkChain.OnNeedCheck -= NeedCheck;
                }
                _checkChain = value;
                _checkChain.OnNeedCheck += NeedCheck;
            }
        }

        public BaseCheck(ExclusionType exclusionType = ExclusionType.Undefined, List<ICheckStep> checkSteps = null)
        {
            if (checkSteps != null)
            {
                ICheckStep lastStep = null;
                foreach(var step in checkSteps)
                {
                    if (lastStep != null)
                    {
                        lastStep.SetNextStep(step);
                    }
                    else
                    {
                        CheckChain = step;
                        lastStep = CheckChain;
                        continue;
                    }
                    lastStep = step;
                }
            }
            ExclusionType = exclusionType;
        }

        public virtual double Check()
        {
            var result = CheckChain?.Check() ?? 0;
            IsDirty = result != 0;
            return result;
        }
        public virtual void NeedCheck()
        {
            OnDirty?.Invoke();
            IsDirty = true;
        }
        BaseCheck ICloneable<BaseCheck>.Clone()
        {
            return new BaseCheck
            {
                CheckChain = CheckChain.Clone(),
                Priority = Priority,
                ExclusionType = ExclusionType
            };
        }
        public virtual object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
