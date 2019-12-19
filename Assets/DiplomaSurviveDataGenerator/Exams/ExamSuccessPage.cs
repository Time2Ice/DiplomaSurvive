using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ExamSuccessPage : ExamPage, ICloneable<ExamSuccessPage>
    {
        public override ExamPageType Type
        {
            get
            {
                return ExamPageType.Success;
            }
        }

        ExamSuccessPage ICloneable<ExamSuccessPage>.Clone()
        {
            ICloneable<ExamButton> cloneable;
            var page = new ExamSuccessPage
            {
                Title = Title,
                Description = Description,
                Type = ExamPageType.Success
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
