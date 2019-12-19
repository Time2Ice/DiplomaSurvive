﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class BaseDeductionCheck : IDeductionCheck
    {
        private INumberGenerator _generator;
        protected BaseContext _context;
        protected List<BaseCheck> _checks;
        protected IDeductionStore _deductionStore;
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

        public BaseDeductionCheck 
        (
            BaseContext context, 
            IStore<BaseCheck> checks, 
            IDeductionStore deductionStore
        )
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null");
            _deductionStore = deductionStore ?? throw new ArgumentNullException("Deduction store must be not null");
            _generator = new DefaultNumberGenerator();
            InitChecks(checks.GetAll());
        }

        public Deduction CheckForDeduction()
        {
            IsNecessary = false;
            Deduction deduction = null;
            var currChecks = _checks.Where(check => check.IsDirty);

            foreach(var check in currChecks)
            {
                if (check.Check() <= _generator.NextDouble01())
                {
                    continue;
                }
                deduction = _deductionStore.GetDeduction(check.DeductionType);
                if (deduction != null)
                {
                    break;
                }
            }

            return deduction;
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