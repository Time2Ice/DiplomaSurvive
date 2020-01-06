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
            foreach (var type in Enum.GetValues(typeof(ExclusionType)).Cast<ExclusionType>())
            {
                types.Add(type, "Exclusion due to ");
            }
            types[ExclusionType.FailEIT] += "EIT failure";
            types[ExclusionType.FailSession] += "Session failure";
            types[ExclusionType.PersonalLife] += "Personal reasons";
            types[ExclusionType.Professor] += "Study reasons";

            var list = new List<Exclusion>()
            {
                new Exclusion()
                {
                    Title = types[ExclusionType.FailEIT],
                    Types = new List<ExclusionType>(){ ExclusionType.FailEIT },
                    Description = "Unfortunately, you could not even enroll in a university. You failed external independent testing.",
                },
                new Exclusion()
                {
                    Title = types[ExclusionType.FailSession],
                    Types = new List<ExclusionType>(){ ExclusionType.FailSession },
                    Description = "The teacher liked you too much. And he decided to flunk you so that no one would think that he has favorites.",
                },
                new Exclusion()
                {
                    Coefficient = 3,
                    Title = types[ExclusionType.PersonalLife],
                    Types = new List<ExclusionType>(){ ExclusionType.PersonalLife },
                    Description = "You studied too much, because of the girlfriend left you. You were upset and could not finish the tasks on time.",
                },
                new Exclusion()
                {
                    Title = types[ExclusionType.FailSession],
                    Types = new List<ExclusionType>(){ ExclusionType.FailSession },
                    Description = "You had different tastes at football clubs with the teacher who took the exam."
                },
                new Exclusion()
                {
                    Title = types[ExclusionType.Professor],
                    Types = new List<ExclusionType>(){ ExclusionType.Professor },
                    Description = "You had different tastes at basketb clubs with the teacher who took the exam."
                },
                new Exclusion()
                {
                    Title = types[ExclusionType.PersonalLife],
                    Types = new List<ExclusionType>(){ ExclusionType.PersonalLife },
                    Description = "You missing two deadlines because you die. But that does not matter anyone..."
                },
                new Exclusion()
                {
                    Coefficient = 2,
                    Title = types[ExclusionType.PersonalLife],
                    Types = new List<ExclusionType>(){ ExclusionType.PersonalLife, ExclusionType.FailSession },
                    Description = "You tried to take everything so hard that you started to disgust everyone. "
                },
            };
            //You are expelled due to missing deadlines that you didn’t know about in 2 important subjects
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Id = (1000 + i).ToString();
            }

            return list;
        }
    }
}
