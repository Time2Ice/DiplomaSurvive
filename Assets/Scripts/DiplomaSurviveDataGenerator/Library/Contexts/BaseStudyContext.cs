using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseStudyContext : Context, IStudyContext
    {
        protected bool _hasBook = false;
        protected ExclusionType _needExclusion = ExclusionType.Undefined;
        public virtual bool HasBook
        {
            get { return _hasBook; }
            set
            {
                _hasBook = value;
                OnHasBookChanged?.Invoke();
            }
        }
        public virtual ExclusionType NeedExclusion
        {
            get
            {
                return _needExclusion;
            }
            set
            {
                _needExclusion = value;
                OnNeedExclusionChanged?.Invoke();
            }
        }

        public virtual event ValueChanged OnHasBookChanged;
        public virtual event ValueChanged OnNeedExclusionChanged;

        public BaseStudyContext()
        {
            OnHasBookChanged += ContextChanged;
            OnNeedExclusionChanged += ContextChanged;
        }
    }
}
