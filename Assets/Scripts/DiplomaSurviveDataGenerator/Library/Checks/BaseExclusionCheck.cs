using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class BaseExclusionCheck : IExclusionCheck
    {
        private INumberGenerator _generator;
        protected IPlayContext _context;
        protected List<BaseCheck> _checks;
        protected IExclusionService _exclusionStore;
        public INumberGenerator NumberGenerator
        {
            get
            {
                return _generator;
            }
            set
            {
                _generator = value ?? new DefaultNumberGenerator();
            }
        }
        public bool IsNecessary { get; protected set; } = true;

        public BaseExclusionCheck 
        (
            IPlayContext context, 
            IStore<BaseCheck> checks, 
            IExclusionService exclusionStore
        )
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null");
            _exclusionStore = exclusionStore ?? throw new ArgumentNullException("Exclusion store must be not null");
            _generator = new DefaultNumberGenerator();
            InitChecks(checks.GetAll());
        }

        public string CheckForExclusion()
        {
            Exclusion exclusion = null;
            var currChecks = _checks.Where(check => check.IsDirty);
            foreach(var check in currChecks)
            {
                if (check.Check() <= _generator.NextDouble01())
                {
                    continue;
                }
                exclusion = _exclusionStore.GetExclusion(check.ExclusionType);
                if (exclusion != null)
                {
                    break;
                }
            }

            if (exclusion == null)
            {
                IsNecessary = false;
                return null;
            }
            return exclusion.Id;
        }

        private void NeedCheck()
        {
            IsNecessary = true;
        }

        private void InitChecks(ICollection<BaseCheck> checks)
        {
            _checks = checks?.OrderBy(x => x.Priority).ToList() ?? new List<BaseCheck>();
            foreach(var check in _checks)
            {
                check.OnDirty += NeedCheck;
            }
        }
    }

}
