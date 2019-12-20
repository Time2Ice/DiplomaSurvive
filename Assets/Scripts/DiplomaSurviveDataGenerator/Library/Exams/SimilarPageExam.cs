using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    [Serializable]
    public class SimilarPageExam: ExamPage
    {
        protected ExamPage _nextPage;
        public ExamPage NextPage
        {
            get { return _nextPage; }
            set
            {
                _nextPage = value;
                if (LeftButton != null)
                {
                    LeftButton.NextPage = value;
                }
                if (RightButton != null)
                {
                    RightButton.NextPage = value;
                }
            }
        }
        public double Probability
        {
            set
            {
                SetProbability(value);
            }
        }

        public SimilarPageExam(ExamPage currPage, ExamPage nextPage = null)
        {
            LeftButton = currPage.LeftButton;
            RightButton = currPage.RightButton;
            Type = currPage.Type;
            NextPage = nextPage;
            Title = currPage.Title;
        }
        public SimilarPageExam(string title = "", string leftTitle = "", string rightTitle = "", double leftCoef = 1, 
            double rightCoef = 1, ExamPage nextPage = null, INumberGenerator generator = null)
        {
            Title = title;
            Buttons.Add(new ExamButton(generator: generator)
            {
                Title = leftTitle,
                NextPage = nextPage,
                DeductionCoefficient = leftCoef
            });
            Buttons.Add(new ExamButton(generator: generator)
            {
                Title = rightTitle,
                NextPage = nextPage,
                DeductionCoefficient = rightCoef
            });
        }
        public SimilarPageExam(ExamButton leftButton, ExamButton rightButton, ExamPage nextPage = null, string title = "")
        {
            LeftButton = leftButton;
            RightButton = rightButton;
            leftButton.NextPage = nextPage;
            rightButton.NextPage = nextPage;
            Title = title;
        }
        public SimilarPageExam(List<SimilarPageExam> pages)
        {
            SimilarPageExam lastPage = null;
            foreach (SimilarPageExam page in pages)
            {
                if (lastPage == null)
                {
                    LeftButton = page.LeftButton;
                    RightButton = page.RightButton;
                    Type = page.Type;
                    NextPage = page.NextPage;
                    Title = page.Title;
                    lastPage = this;
                }
                else
                {
                    lastPage.NextPage = page;
                    lastPage = page;
                }
            }
        }
    }
}
