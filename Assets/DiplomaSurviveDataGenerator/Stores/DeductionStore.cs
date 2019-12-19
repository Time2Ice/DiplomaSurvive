using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class DeductionStore : IDeductionStore
    {
        private readonly IStore<Deduction> _deductionStore;
        private readonly INumberGenerator _generator;

        public DeductionStore(IStore<Deduction> deductionStore, INumberGenerator generator = null)
        {
            _deductionStore = deductionStore ?? throw new ArgumentNullException();
            _generator = generator ?? new DefaultNumberGenerator();
        }

        public Deduction GetDeduction(DeductionType type, int? level = null)
        {
            var deductions = _deductionStore.GetAll().Where(d => d.Type == type);
            if (level.HasValue)
            {
                deductions = deductions.Where(d => d.Level == level || d.Level <= 0);
            }

            int? index = _generator.IndexByCoefficients(deductions.Select(x => x.Coefficient).ToList());

            return index == null 
                ? null 
                : deductions.ToList()[index.Value];
        }
    }
}
