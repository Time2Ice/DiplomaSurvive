using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class ExclusionService : IExclusionService
    {
        private readonly IStore<Exclusion> _exclusionStore;
        private readonly INumberGenerator _generator;

        public ExclusionService(IStore<Exclusion> exclusionStore, INumberGenerator generator = null)
        {
            _exclusionStore = exclusionStore ?? throw new ArgumentNullException();
            _generator = generator ?? new DefaultNumberGenerator();
        }

        public Exclusion GetExclusion(ExclusionType type, int? level = null)
        {
            var exclusions = _exclusionStore.GetAll().Where(d => d.Type == type);
            if (level.HasValue)
            {
                exclusions = exclusions.Where(d => d.Level == level || d.Level <= 0);
            }
            int? index = _generator.IndexByCoefficients(exclusions.Select(x => x.Coefficient).ToList());
            return index == null 
                ? null 
                : exclusions.ToList()[index.Value];
        }
    }
}
