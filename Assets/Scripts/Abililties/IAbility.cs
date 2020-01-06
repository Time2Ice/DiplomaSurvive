using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abililties
{
    interface IAbility
    {
        void UseAbility();
       int Id { get; set; }
    }
}
