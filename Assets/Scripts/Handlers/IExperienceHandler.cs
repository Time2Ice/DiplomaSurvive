using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
    public interface IExperienceHandler
    {
        event Action<int, int> ExperienceChanged;

        void ChangeExperience();

        int MaxExperience { get; }
        int Experience { get; }
    }
}
