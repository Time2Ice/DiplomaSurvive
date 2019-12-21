using DiplomaSurviveDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class Play
    {
        protected IPlayContext _context;
        protected INumberGenerator _numberGenerator;
        public IStore<IExam> ExamStore { get; protected set; }
        public IStore<ICheck> CheckStore { get; protected set; }
        public IPlayEventStore EventStore { get; protected set; }

        public Play(INumberGenerator numberGenerator, IPlayContext context)
        {
            _numberGenerator = numberGenerator;
            _context = context;
            InitExamStore();
            //InitCheckStore();
        }
        public void InitCheckStore()
        {
            CheckStore = new BaseStore<ICheck>()
            {
                new BaseCheck()
                {
                    //CheckChain = new CheckLastClickTime(_context)
                    //{
                    //    MinTimeAfterClick = 2,
                    //    ExclusionProbabilityMin = 0.004,
                    //    MaxTimeAfterClick = 60,
                    //    ExclusionProbabilityMax = 0.002
                    //}
                }
            };
        }
        public void InitExamStore()
        {
            ExamStore = new BaseStore<IExam>
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
                    ExclusionProbability = 0.5
                },
                new ExamTree()
                {
                    RootPage = new SimilarPageExam(Questions.CHOOSE_SUBJECT, Answers.Subjects.LITERATURE,
                        Answers.Subjects.SPANISH, 0.75, 0.9, FullExamPages.DoNotKnowAnswersContinueBomb()),
                    Type = ExamType.Universal,
                    ExclusionProbability = 0.5
                },
                new ExamTree()
                {
                    RootPage = FullExamPages.MathChemistryAskHelp(),
                    Type = ExamType.EIT,
                    ExclusionProbability = 0.5
                },
            };
        }
    }

}
