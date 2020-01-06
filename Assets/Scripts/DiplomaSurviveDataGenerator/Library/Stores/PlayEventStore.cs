using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiplomaSurviveDataGenerator
{
    public class PlayEventStore : BaseStore<PlayEvent, IPlayContext>, IPlayEventStore
    {
        public PlayEventStore(ICollection<PlayEvent> events = null, INumberGenerator numberGenerator = null)
            : base(events, numberGenerator)
        { }

        public override PlayEvent Get(IPlayContext context)
        {
            var availableEl = GetAll().Where(el => el.IsAvailable(context)).ToList();
            if (availableEl.Count == 0)
            {
                return null;
            }
            int num = _numberGen.Next(availableEl.Count);
            Debug.Log(num + "|");
            return availableEl[num];
        }
       
    }
}
