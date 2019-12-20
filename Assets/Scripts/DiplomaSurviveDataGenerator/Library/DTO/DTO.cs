using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public abstract class DTO
    {
        public string Id { get; set; }

        public DTO()
        {
            Id = GetHashCode().ToString();
        }
    }
}
