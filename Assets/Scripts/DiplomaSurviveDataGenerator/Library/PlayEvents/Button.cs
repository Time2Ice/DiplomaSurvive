using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class Button<T> : Button, ICloneable<Button<T>>
    {
        public Func<IPlayContext, T> ClickFuncReturn;
        [field: NonSerialized]
        public override event ValueChanged OnClickEvent;
        public virtual T OnClickFunc(IPlayContext context = null)
        {
            OnClickEvent?.Invoke();
            if (ClickFuncReturn != null)
            {
                return ClickFuncReturn.Invoke(context);
            }
            return default(T);
        }

        Button<T> ICloneable<Button<T>>.Clone()
        {
            return new Button<T>
            {
                ClickFuncReturn = ClickFuncReturn,
                ClickFunc = ClickFunc,
                Title = Title,
            };
        }
    }

    [Serializable]
    public class Button : ICloneable<Button>
    {
        public Action<IPlayContext> ClickFunc;
        public string Title { get; set; }
        [field:NonSerialized]
        public virtual event ValueChanged OnClickEvent;

        public virtual void OnClick(IPlayContext context = null)
        {
            ClickFunc?.Invoke(context);
            OnClickEvent?.Invoke();
        }
        Button ICloneable<Button>.Clone()
        {
            return new Button
            {
                Title = Title,
                ClickFunc = ClickFunc
            };
        }
        public virtual object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
