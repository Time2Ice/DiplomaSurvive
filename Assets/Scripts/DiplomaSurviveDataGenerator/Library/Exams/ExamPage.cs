using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class ExamPage : ActionPage<ExamButton>, ICloneable<ExamPage>
    {
        public virtual ExamPageType Type { get; set; } = ExamPageType.InProgress;
        public virtual ExamPage LeftPage
        {
            get
            {
                return LeftButton?.NextPage;
            }
            set
            {
                if (LeftButton != null)
                {
                    LeftButton.NextPage = value;
                }
            }
        }
        public virtual ExamPage RightPage
        {
            get
            {
                return RightButton?.NextPage;
            }
            set
            {
                if (RightButton != null)
                {
                    RightButton.NextPage = value;
                }
            }
        }
        public virtual ExamButton LeftButton
        {
            get
            {
                return Buttons.Count > 0 ? Buttons[0] : null;
            }
            set
            {
                if (Buttons.Count > 0)
                {
                    Buttons[0] = value;
                }
                else
                {
                    Buttons.Add(value);
                }
            }
        }
        public virtual ExamButton RightButton
        {
            get
            {
                return Buttons.Count > 1 ? Buttons[1] : null;
            }
            set
            {
                ICloneable<ExamButton> cloneable = value;
                if (Buttons.Count > 1)
                {
                    Buttons[1] = value;
                }
                else
                {
                    if (Buttons.Count == 0)
                    {
                        Buttons.Add(cloneable.Clone());
                    }
                    Buttons.Add(value);
                }
            }
        }

        public ExamPage()
        { }
        public ExamPage(string title = "", string leftTitle = "", string rightTitle = "", 
            double leftCoef = 1, double rightCoef = 1, INumberGenerator generator = null)
        {
            Title = title;
            Buttons.Add(new ExamButton(generator: generator)
            {
                Title = leftTitle,
                DeductionCoefficient = leftCoef
            });
            Buttons.Add(new ExamButton(generator: generator)
            {
                Title = rightTitle,
                DeductionCoefficient = rightCoef
            });
        }
        public void SetProbability(double probability)
        {
            foreach(var button in Buttons)
            {
                button?.SetDeductionProbability(probability);
            }
        }
        ExamPage ICloneable<ExamPage>.Clone()
        {
            ICloneable<ExamButton> cloneable;
            var page = new ExamPage
            {
                Title = Title,
                Description = Description,
                Type = Type
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
