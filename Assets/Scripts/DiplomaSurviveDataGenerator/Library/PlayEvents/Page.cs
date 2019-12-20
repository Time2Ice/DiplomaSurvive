using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class Page : ICloneable<Page>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [field: NonSerialized]
        public virtual event ValueChanged OnCloseEvent;
        public Action OnCloseFunc;

        public virtual void OnClose()
        {
            OnCloseFunc?.Invoke();
            OnCloseEvent?.Invoke();
        }
        Page ICloneable<Page>.Clone()
        {
            return new Page
            {
                Title = Title,
                Description = Description,
                OnCloseFunc = OnCloseFunc
            };
        }
        public virtual object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
