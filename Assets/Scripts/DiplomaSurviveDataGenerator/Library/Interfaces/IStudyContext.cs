using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IStudyContext
    {
        bool HasBook { get; set; }
        ExclusionType NeedExclusion { get; set; }
        event ValueChanged OnHasBookChanged;
        event ValueChanged OnNeedExclusionChanged;
    }
}
