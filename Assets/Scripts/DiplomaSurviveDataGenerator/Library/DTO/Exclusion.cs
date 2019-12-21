using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class Exclusion : DTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Coefficient { get; set; } = 1;
        public int? Level { get; set; }
        public ExclusionType Type { get; set; }
    }
}
