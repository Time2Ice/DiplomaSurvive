using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class NeedDeductionCheck : BaseCheck
    {
        private IPlayContext _context;
        private ExclusionType _type;
        public override ExclusionType ExclusionType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public NeedDeductionCheck(IPlayContext context)
        {
            _context = context;
            _context.Study.OnNeedExclusionChanged += NeedCheck;
        }

        public override double Check()
        {
            if(_context.Study.NeedExclusion == ExclusionType.Undefined)
            {
                IsDirty = false;
                return 0;
            }
            _type = _context.Study.NeedExclusion;
            return 1;
        }
    }
}
