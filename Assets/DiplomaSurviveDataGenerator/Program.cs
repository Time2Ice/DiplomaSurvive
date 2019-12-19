using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Check3();
            Console.ReadKey();
        }

        public static void Check1()
        {
            BaseContext context = new BaseContext()
            {
                Score = new ScoreContext()
                {
                    PersonalLifeScore = 10,
                },
            };

            BaseCheck checkService = new BaseCheck()
            {
                CheckChain = new PersonalLifeScoreDefaultShortageCheckStep(context)
                {
                    DeductionProbability = 0.8,
                },
                DeductionType = DeductionType.Undefined
            };
            Console.WriteLine(checkService.IsDirty);
            Console.WriteLine(checkService.Check());
            Console.WriteLine(checkService.IsDirty);
            context.Score.MinPersonalLifeScore = 8;
            Console.WriteLine(checkService.IsDirty);
            Console.WriteLine(checkService.Check());
            Console.WriteLine(checkService.IsDirty);
            context.Score.PersonalLifeScore = 7;
            Console.WriteLine(checkService.IsDirty);
            Console.WriteLine(checkService.Check());
        }

        public static void Check2()
        {
            BaseContext context = new BaseContext()
            {
                Score = new ScoreContext()
                {
                    PersonalLifeScore = 50,
                }
            };
            PersonalLifeScoreShortageCheckStep step = new PersonalLifeScoreShortageCheckStep(context)
            {
                MinScore = 100,
            };
            ICheckStep stepI = step;
            ICheck check = new BaseCheck
            {
                CheckChain = stepI.ShallowCopy() as ICheckStep
            };
            Console.WriteLine("initial  " + check.Check());
            step.MinScore = 20;
            Console.WriteLine("step.MinScore = 20;  " + check.Check());
            context.Score.PersonalLifeScore = 120;
            Console.WriteLine("PersonalLifeScore = 120;  " + check.Check());
            context.Score.PersonalLifeScore = 10;
            Console.WriteLine("PersonalLifeScore = 10;  " + check.Check());
        }

        public static void Check3()
        {
            Play play = new Play(null, new BaseContext());
            var exams = play.ExamStore;
            foreach(var exam in exams.GetAll())
            {
                Console.WriteLine();
                Console.WriteLine("------ NEW EXAM ------");
                var examPages = exam.Start();
                ActPage(examPages);
            }
        }

        public static ActionPageShow show = new ActionPageShow();
        public static void ActPage(ExamPage page)
        {
            show.Clean();
            if (page.Type == ExamPageType.InProgress)
            {
                show.Description = page.Title;
                if (page.LeftButton != null)
                {
                    show.ButtonL = page.LeftButton.Title;
                    show.EnableL = true;
                    show.OnL += () =>
                    {
                        show.Clean();
                        ActPage(page.LeftButton.OnClickFunc());
                    };
                }
                if (page.RightButton != null)
                {
                    show.EnableR = true;
                    show.ButtonR = page.RightButton.Title;
                    show.OnR += () =>
                    {
                        show.Clean();
                        ActPage(page.RightButton.OnClickFunc());
                    };
                }
            }
            if (page.Type == ExamPageType.Fail)
            {
                show.Description = page.Title ?? "You fail the exam!";
                show.EnableM = true;
                show.ButtonM = "finish";
                show.OnM += () =>
                {
                    show.Clean();
                    Console.WriteLine("------ FINISH ------");
                };
            }
            if (page.Type == ExamPageType.Success)
            {
                show.Description = page.Title ?? "You pass the exam!";
                show.EnableM = true;
                show.ButtonM = "continue";
                show.OnM += () =>
                {
                    show.Clean();
                    Console.WriteLine("------ FINISH ------");
                };
            }
            show.Show();
        }
    }

    public class ActionPageShow
    {
        public string Description;
        public string ButtonM;
        public string ButtonL;
        public string ButtonR;
        public bool EnableL;
        public bool EnableR;
        public bool EnableM;
        public event Action OnL;
        public event Action OnR;
        public event Action OnM;
        public bool IsFinish = true;
        public void Show()
        {
            Console.WriteLine("------- PAGE --------");
            Console.WriteLine("Description: " + Description);
            if (EnableL)
            {
                Console.WriteLine("Button1: " + ButtonL);
            }
            if (EnableM)
            {
                Console.WriteLine("Button2: " + ButtonM);
            }
            if (EnableR)
            {
                Console.WriteLine("Button3: " + ButtonR);
            }
            string check = "";
            while ((check != "1" || !EnableL) && (check != "2" || !EnableM) && (check != "3" || !EnableR) && check != "100")
            {
                Console.Write("Put number of Button: ");
                check = Console.ReadLine();
            }
            Console.WriteLine();
            switch(check)
            {
                case "1":
                    OnL?.Invoke();
                    break;
                case "2":
                    OnM?.Invoke();
                    //Console.WriteLine("FINISH");
                    //IsFinish = true;
                    break;
                case "3":
                    OnR?.Invoke();
                    break;
            }
        }
        public void Clean()
        {
            Description = "";
            ButtonL = "";
            ButtonM = "";
            ButtonR = "";
            EnableL = false;
            EnableM = false;
            EnableR = false;
            OnL = null;
            OnR = null;
            OnM = null;
            IsFinish = false;
        }
    }
}
