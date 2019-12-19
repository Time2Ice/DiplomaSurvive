using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class Play
    {
        protected BaseContext _context;
        protected INumberGenerator _numberGenerator;
        public IStore<IExam> ExamStore { get; protected set; }
        public IStore<ICheck> CheckStore { get; protected set; }
        public IPlayEventStore EventStore { get; protected set; }

        public Play(INumberGenerator numberGenerator, BaseContext context)
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
                    CheckChain = new CheckLastClickTime(_context)
                    {
                        MinTimeAfterClick = 2,
                        DeductionProbabilityMin = 0.004,
                        MaxTimeAfterClick = 60,
                        DeductionProbabilityMax = 0.002
                    }
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

    public static class Questions
    {
        public const string FROW_WHAT_BEGIN = "What tasks do you start with? ";
        public const string TRY_TO_CHEAT = "Try to cheat? ";
        public const string CHOOSE_SUBJECT = "Choose the subject ";
        public const string MINUS_1_MINUS_1 = "- 1 - 1 = ? ";
        public const string _2_PLUS_2 = "2 + 2 = ? ";
        public const string HELP_FRIEND = "Your friend asked you for the help. Help him with the task? ";
        public const string H2O_IS = "What does the H2O formula mean? ";
        public const string DONT_KNOW_REMAINING_ANSWERS = "You do not know the remaining answers. ";
        public const string MARK_BY_LETTER = "Select a letter to fill in all the answers";
        public const string FOCUS_OR_RELAX = "Focus or relax? ";
        public const string CHECK_AGAIN = "Check all answers again? ";
        public const string ASK_FOR_HELP = "Ask for help? ";
        public const string SEARCH_IN_PHONE = "Try to find the answers on your phone? ";
        public const string SEARCH_IN_INTERNET = "Try to find the answers in the Internet?";
        public const string GET_CAUGHT_CHEATING = "You were caught cheating. ";
        public const string PHONE_RANG = "Your phone rang in room. ";
        public const string SAY_ABOUT_CHEATING = "You noticed someone cheating. Say about it? ";
        public const string ONE_MINUTE_LEFT = "One minute left before the end of the exam.";
        public const string REPORT_ABOUT_BOMB = "Report that there is a bomb in the building?";
        public const string DONT_KNOW_ANSWERS = "You do not know the correct answers. ";
        public const string POLICE_FOUND_NO_BOMB = "The police found that there was no bomb.";
        public const string BLAME_WHO = "Who do you want to blame?";
        public const string WHOM_ASK_HELP = "Whom do you ask for help?";
        public const string HELP_WHO = "Who do you want to help?";
        //public const string  = "";
        //public const string  = "";
        //public const string  = "";
        //public const string  = "";
    }
    public partial class Answers
    {
        public const string THEORETICAL_TASKS = "Theoretical tasks";
        public const string PRACTICAL_TASKS = "Practical tasks";
        public const string THEORY = "Theory";
        public const string PRACTICE = "Practice";
        public const string NO = "No";
        public const string YES = "Yes";
        public static partial class Subjects
        {
            public const string ENGLISH = "English";
            public const string MATH = "Math";
            public const string CHEMISTRY = "Chemistry";
            public const string HISTORY = "History";
            public const string LITERATURE = "Literature";
            public const string GEOMETRY = "Geometry";
            public const string ALGEBRA = "Algebra";
            public const string SPANISH = "Spanish";
            public const string DISCRETE_MATH = "Discrete math";
            public const string LINEAR_ALGEBRA = "Linear algebra";
            public const string COMPUTER_SCIENCE = "Computer science";
            public const string PHILOSOPHY = "Philosophy";
            public const string PSYCHOLOGY = "Psychology";
            public const string COMPUTER_ENGINEERING = "Computer engineering";
            public const string CULTUROLOGY = "Culturology";
            public const string PHYSICS = "Physics";
            public const string GAME_DEVELOPMENT = "Game development";
        }
        public const string FIRE = "Fire";
        public const string WATER = "Water";
        public const string ACID = "Acid";
        public const string IRON = "Iron";
        public const string FOCUS = "Focus";
        public const string RELAX = "Relax";
        public const string CONFESS = "Confess";
        public const string DENY = "Deny";
        public const string IGNORE = "Ignore";
        public const string TAKE_CALL = "Take a call";
        public const string TURN_ON = "Turn on";
        public const string TURN_OFF = "Turn off";
        public const string FINISH = "Finish";
        public const string RECHECK = "Recheck";
        public const string GUESS = "Guess";
        public const string GIVE_UP = "Give up";
        public const string CHEAT = "Cheat";
        public const string CONTINUE = "Continue";
        public const string LIE = "Lie";
        public static partial class Names
        {
            public const string ANTON = "Anton";
            public const string DENIS = "Denis";
        }
        //public const string  = "";
        //public const string  = "";
    }
    public static partial class ExamPages
    {
        public static ExamPage MINUS_1_MINUS_1(double coefL = 1.5, double coefR = 0.2)
        {
            return new ExamPage(Questions.MINUS_1_MINUS_1, "2", "-2", coefL, coefR);
        }
        public static ExamPage TWO_PLUS_TWO(double coefL = 0.9, double coefR = 0.8)
        {
            return new ExamPage(Questions._2_PLUS_2, "5", "4", coefL, coefR);
        }
        public static ExamPage H2O_IS(double coefL = 0.3, double coefR = 1.35)
        {
            return new ExamPage(Questions.H2O_IS, Answers.WATER, Answers.FIRE, coefL, coefR);
        }
        public static ExamPage TRY_TO_CHEAT(double coefL = 1.12, double coefR = 0.76)
        {
            return YesNoQuestion(Questions.TRY_TO_CHEAT, coefL, coefR);
        }
        public static ExamPage HELP_FRIEND(double coefL = 1.4, double coefR = 1)
        {
            return YesNoQuestion(Questions.HELP_FRIEND, coefL, coefR);
        }
        public static ExamPage MARK_REMAINING_ANSWERS_WITH_A(double coefL = 0.8, double coefR = 1.05)
        {
            return YesNoQuestion(Questions.DONT_KNOW_REMAINING_ANSWERS + "Mark them with the letter A?", coefL, coefR);
        }
        public static ExamPage MARK_ANSWERS_BY_LETTER(double coefL = 1.4, double coefR = 0.99)
        {
            return new ExamPage(Questions.MARK_BY_LETTER, "A", "B", coefL, coefR);
        }
        public static ExamPage FOCUS_OR_RELAX(double coefL = 1.6, double coefR = 1.1)
        {
            return new ExamPage(Questions.FOCUS_OR_RELAX, Answers.FOCUS, Answers.RELAX, coefL, coefR);
        }
        public static ExamPage SEARCH_IN_INTERNET(double coefL = 0.72, double coefR = 0.8)
        {
            return YesNoQuestion(Questions.SEARCH_IN_INTERNET, coefL, coefR);
        }
        public static ExamPage SEARCH_IN_PHONE(double coefL = 1.11, double coefR = 1)
        {
            return YesNoQuestion(Questions.SEARCH_IN_PHONE, coefL, coefR);
        }
        public static ExamPage CAUGHT_CHEATING(double coefL = 2.25, double coefR = 1.4)
        {
            return new ExamPage(Questions.GET_CAUGHT_CHEATING, Answers.CONFESS, Answers.DENY, coefL, coefR);
        }
        public static ExamPage PHONE_RANG(double coefL = 0.15, double coefR = 0.8)
        {
            return new ExamPage(Questions.PHONE_RANG, Answers.TAKE_CALL, Answers.IGNORE, coefL, coefR);
        }
        public static ExamPage ONE_MINUTE_LEFT(double coefL = 0.8, double coefR = 3)
        {
            return new ExamPage(Questions.ONE_MINUTE_LEFT, Answers.FINISH, Answers.RECHECK, coefL, coefR);
        }
        public static ExamPage SAY_ABOUT_CHEATING(double coefL = 0.4, double coefR = 1)
        {
            return YesNoQuestion(Questions.SAY_ABOUT_CHEATING, coefL, coefR);
        }
        public static ExamPage CHECK_ANSWERS(double coefL = 0.66, double coefR = 1.33)
        {
            return YesNoQuestion(Questions.CHECK_AGAIN, coefL, coefR);
        }
        public static ExamPage REPORT_ABOUT_BOMB(double coefL = 0.2, double coefR = 0.98)
        {
            return YesNoQuestion(Questions.REPORT_ABOUT_BOMB, coefL, coefR);
        }
        public static ExamPage DONT_KNOW_ANSWERS_GUESS(double coefL = 2.3, double coefR = 0.32)
        {
            return new ExamPage(Questions.DONT_KNOW_ANSWERS, Answers.GIVE_UP, Answers.GUESS, coefL, coefR);
        }
        public static ExamPage DONT_KNOW_ANSWERS_CONTINUE(double coefL = 2.18, double coefR = 0.8)
        {
            return new ExamPage(Questions.DONT_KNOW_ANSWERS, Answers.GIVE_UP, Answers.CONTINUE, coefL, coefR);
        }
        public static ExamPage DONT_KNOW_ANSWERS_CHEAT(double coefL = 1.9, double coefR = 1.15)
        {
            return new ExamPage(Questions.DONT_KNOW_ANSWERS, Answers.GIVE_UP, Answers.CHEAT, coefL, coefR);
        }
        public static ExamPage POLICE_FOUND_NO_BOMB(double coefL = 20, double coefR = 1)
        {
            return new ExamPage(Questions.POLICE_FOUND_NO_BOMB, Answers.GIVE_UP, Answers.LIE, coefL, coefR);
        }
        public static ExamPage BLAME_ANTON_DENIS(double coefL = 20, double coefR = 0.75)
        {
            return new ExamPage(Questions.BLAME_WHO, Answers.Names.ANTON, Answers.Names.DENIS, coefL, coefR);
        }
        public static ExamPage ASK_HELP_ANTON_DENIS(double coefL = 2.2, double coefR = 0.175)
        {
            return new ExamPage(Questions.WHOM_ASK_HELP, Answers.Names.ANTON, Answers.Names.DENIS, coefL, coefR);
        }
        public static ExamPage CHOOSE_CHEMISTRY_MATH(double coefL = 1, double coefR = 1.2)
        {
            return new ExamPage(Questions.CHOOSE_SUBJECT, Answers.Subjects.CHEMISTRY, Answers.Subjects.MATH, coefL, coefR);
        }
        public static ExamPage FROM_WHAT_BEGIN(double coefL = 1, double coefR = 0.98)
        {
            return new ExamPage(Questions.FROW_WHAT_BEGIN, Answers.THEORETICAL_TASKS, Answers.PRACTICAL_TASKS, coefL, coefR);
        }
        public static ExamPage ASK_FOR_HELP(double coefL = 0.75, double coefR = 0.91)
        {
            return YesNoQuestion(Questions.ASK_FOR_HELP, coefL, coefR);
        }
        //public static ExamPage (double coefL = 1, double coefR = 1)
        //{
        //    return YesNoQuestion(Questions., coefL, coefR);
        //}
        //public static ExamPage (double coefL = 2.5, double coefR = 0.32)
        //{
        //    return new ExamPage(Questions., Answers., Answers., coefL, coefR);
        //}
        //public static ExamPage (double coefL = 1, double coefR = 1)
        //{
        //    return YesNoQuestion(Questions., coefL, coefR);
        //}
        //public static ExamPage (double coefL = 1, double coefR = 1)
        //{
        //    return YesNoQuestion(Questions., coefL, coefR);
        //}

        public static ExamPage YesNoQuestion(string question, double coefL = 1, double coefR = 1)
        {
            return new ExamPage(question, Answers.YES, Answers.NO, coefL, coefR);
        }
    }
    public static partial class FullExamPages
    {
        public static ExamPage DoNotKnowAnswersContinueBomb()
        {
            ExamPage currPage;
            ExamPage page = ExamPages.DONT_KNOW_ANSWERS_CONTINUE();
            currPage = page.RightPage = ExamPages.REPORT_ABOUT_BOMB();
            currPage = currPage.LeftPage = ExamPages.POLICE_FOUND_NO_BOMB();
            currPage.RightPage = ExamPages.BLAME_ANTON_DENIS();
            return page;
        }
        public static ExamPage MathChemistryAskHelp()
        {
            ExamPage leftPage, rightPage;
            ExamPage page = ExamPages.CHOOSE_CHEMISTRY_MATH();
            leftPage = page.LeftPage = ExamPages.H2O_IS();
            rightPage = page.RightPage = ExamPages.MINUS_1_MINUS_1();
            leftPage.LeftPage = AskForHelpDenisAnton(0.45, 1.1);
            leftPage.RightPage = AskForHelpDenisAnton(1.5, 0.8);
            rightPage.RightPage = AskForHelpDenisAnton(0.5, 1.2);
            rightPage.LeftPage = AskForHelpDenisAnton(1.85, 0.82);
            return page;
        }
        public static ExamPage AskForHelpDenisAnton(double coefHelp = 1.4, double coefNoHelp = 1)
        {
            ExamPage page = ExamPages.ASK_FOR_HELP();
            page.LeftPage = ExamPages.ASK_HELP_ANTON_DENIS();
            page.RightPage = ExamPages.HELP_FRIEND(coefHelp, coefNoHelp);
            page.RightPage.LeftPage = ExamPages.ONE_MINUTE_LEFT();
            page.RightPage.RightPage = ExamPages.ONE_MINUTE_LEFT(1, 0.66);
            return page;
        }
    }
}
