using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSurviveDataGenerator;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class ExamStore : BaseStore<IExam>
    {
        public ExamStore(INumberGenerator generator)
            : base(GetExams(), generator)
        { }

        private static List<IExam> GetExams()
        {
            return new List<IExam>
            {
                new ExamTree()
                {
                    RootPage = new SimilarPageExam(new List<SimilarPageExam>()
                    {
                        new SimilarPageExam(Questions.CHOOSE_SUBJECT, Answers.Subjects.MATH, Answers.Subjects.PHYSICS, 1.15, 0.9),
                        new SimilarPageExam(ExamPages.FROM_WHAT_BEGIN()),
                        new SimilarPageExam(Questions.TRY_TO_CHEAT, Answers.YES, Answers.NO, 0.5, 1.5),
                    }),
                    Type = ExamType.EIT,
                    DeductionProbability = 0.5
                },
                new ExamTree()
                {
                    RootPage = new SimilarPageExam(Questions.CHOOSE_SUBJECT, Answers.Subjects.LITERATURE,
                        Answers.Subjects.SPANISH, 0.75, 0.9, FullExamPages.DoNotKnowAnswersContinueBomb()),
                    Type = ExamType.Universal,
                    DeductionProbability = 0.5
                },
                new ExamTree()
                {
                    RootPage = FullExamPages.MathChemistryAskHelp(),
                    Type = ExamType.EIT,
                    DeductionProbability = 0.5
                },
            };
        }
    }
}
