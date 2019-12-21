using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class ExclusionStore : BaseStore<Exclusion>
    {
        private static readonly List<Exclusion> _exclusions;
        static ExclusionStore()
        {
            _exclusions = InitExclusions();
        }

        public ExclusionStore(INumberGenerator generator)
            : base(_exclusions, generator)
        {
        }

        public static List<Exclusion> InitExclusions()
        {
            Dictionary<ExclusionType, string> types = new Dictionary<ExclusionType, string>();
            foreach(var type in Enum.GetValues(typeof(ExclusionType)).Cast<ExclusionType>())
            {
                types.Add(type, "Exclusion due to ");
            }
            types[ExclusionType.FailEIT] += "EIT failure";

            return new List<Exclusion>()
            {
                new Exclusion()
                {
                    Id = "0001",
                    Title = types[ExclusionType.FailEIT],
                    Type = ExclusionType.FailEIT,
                    Description = "Unfortunately, you could not even enroll in a university. You failed external independent testing.",
                }
            };
        }
    }
}
