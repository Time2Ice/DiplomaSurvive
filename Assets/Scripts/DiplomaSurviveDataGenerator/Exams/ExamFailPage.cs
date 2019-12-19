using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ExamFailPage : ExamPage, ICloneable<ExamFailPage>
    {
        public override ExamPageType Type
        {
            get
            {
                return ExamPageType.Fail;
            }
        }
        ExamFailPage ICloneable<ExamFailPage>.Clone()
        {
            ICloneable<ExamButton> cloneable;
            var page = new ExamFailPage
            {
                Title = Title,
                Description = Description,
                Type = ExamPageType.Fail
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
