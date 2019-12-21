using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ActionPage<TButton> : Page, ICloneable<ActionPage<TButton>>
        where TButton : Button, ICloneable<TButton>
    {
        public virtual List<TButton> Buttons { get; set; } = new List<TButton>();
        public virtual void AddButton(TButton button)
        {
            Buttons.Add(button);
        }

        ActionPage<TButton> ICloneable<ActionPage<TButton>>.Clone()
        {
            var page = new ActionPage<TButton>
            {
                Title = Title,
                Description = Description,
                OnCloseFunc = OnCloseFunc
            };
            foreach (var button in Buttons)
            {
                page.AddButton(button.Clone());
            }
            return page;
        }
    }
}
