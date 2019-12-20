using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseCheck : ICheck, ICloneable<BaseCheck>
    {
        private ICheckStep _checkChain;
        public int Priority { get; set; } = int.MaxValue;
        public bool IsDirty { get; private set; } = true;
        [field:NonSerialized]
        public virtual event ValueChanged OnDirty;
        public DeductionType DeductionType { get; set; } = DeductionType.Undefined;
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

        public virtual double Check()
        {
            var result = CheckChain?.Check() ?? 0;
            IsDirty = result != 0;
            return result;
        }
        public virtual void NeedCheck()
        {
            if (IsDirty == false)
            {
                OnDirty?.Invoke();
                IsDirty = true;
            }
        }
        BaseCheck ICloneable<BaseCheck>.Clone()
        {
            return new BaseCheck
            {
                CheckChain = CheckChain.Clone(),
                Priority = Priority,
                DeductionType = DeductionType
            };
        }
        public virtual object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
