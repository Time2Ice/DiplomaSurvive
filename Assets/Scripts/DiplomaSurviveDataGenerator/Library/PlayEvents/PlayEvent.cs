﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class PlayEvent : ActionPage<Button<Page>>, ICloneable<PlayEvent>
    {
        public Func<IPlayContext, bool> IsAvailableFunc;
        public virtual bool IsAvailable(IPlayContext context = null)
        {
            return IsAvailableFunc?.Invoke(context) ?? true;
        }
        public virtual string Result { get; set; }
        PlayEvent ICloneable<PlayEvent>.Clone()
        {
            ICloneable<Button<Page>> cloneable;
            var page = new PlayEvent
            {
                Title = Title,
                Description = Description,
                IsAvailableFunc = IsAvailableFunc
            };
            foreach (var button in Buttons)
            {
                cloneable = button;
                page.AddButton(cloneable.Clone());
            }
            return page;
        }
    }
}
