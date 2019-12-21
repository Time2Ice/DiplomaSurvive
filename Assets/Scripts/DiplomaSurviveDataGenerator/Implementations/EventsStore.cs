using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSurviveDataGenerator;

namespace Assets.Scripts.DiplomaSurviveDataGenerator.Implementations
{
    public class EventsStore : PlayEventStore
    {
        public EventsStore(INumberGenerator generator)
            : base(GetEvents(), generator)
        { }

        private static List<PlayEvent> GetEvents()
        {
            return new List<PlayEvent>
            {
                new PlayEvent()
                {
                    Title = "The teacher offered to buy his book for 20 coins. Buy one?",
                    IsAvailableFunc = (context) => context.Main.Coins > 20 && !context.Study.HasBook,
                    Buttons = new List<Button<Page>>()
                    {
                        new Button<Page>()
                        {
                            ClickFuncReturn = (context) =>
                            {
                                if (new DefaultNumberGenerator().NextDouble01() > 0.7)
                                {
                                    context.Main.Coins = context.Main.Coins - 20;
                                    context.Study.HasBook = true;
                                    return new Page() { Title = "Alas, the teacher ran out of books, but he will not return the prepayment of 5 coins."};
                                }
                                else
                                {
                                    context.Main.Coins = context.Main.Coins - 5;
                                    context.Study.HasBook = false;
                                    return new Page() { Title = "Congratulations! You bought a useful book" };
                                }
                            }
                        },
                        new Button<Page>()
                        {
                            ClickFuncReturn = (context) =>
                            {
                                if (new DefaultNumberGenerator().NextDouble01() > 0.9)
                                {
                                    context.Study.NeedExclusion = ExclusionType.Professor;
                                }
                                return new Page() { Title = "Ok, this is your decision ..." };
                            }
                        }
                    }
                },
                new PlayEvent()
                {
                    Title = "Your girlfriend want to visit your lesson. Allow?",
                    IsAvailableFunc = (x) => true,
                    Buttons = new List<Button<Page>>()
                    {
                        new Button<Page>()
                        {
                            ClickFuncReturn = (context) =>
                            {
                                context.Score.MaxPersonalLifeScore = context.Score.MaxPersonalLifeScore + 10;
                                context.Score.PersonalLifeScore = context.Score.PersonalLifeScore + 3;
                                if (!context.Study.HasBook)
                                {
                                    context.Score.StudyScore = context.Score.StudyScore - 15;
                                    return new Page() { Title = "Your relationship with the girl has improved, but because of this, you got distracted and lost points."};
                                }
                                return new Page() { Title = "You gave girlfriend your study book and the professor took her for a student. You improved your relationship and didn't lose points" };
                            }
                        },
                        new Button<Page>()
                        {
                            ClickFuncReturn = (context) =>
                            {
                                if (new DefaultNumberGenerator().NextDouble01() > 0.75)
                                {
                                    context.Study.NeedExclusion = ExclusionType.PersonalLife;
                                    return new Page() { Title = "You lost your girlfriend" };
                                }
                                else {
                                    context.Score.PersonalLifeScore = context.Score.PersonalLifeScore - 6;
                                    context.Score.StudyScore = context.Score.StudyScore + 5;
                                    return new Page() { Title = "You quarreled with your girlfriend, but you were able to concentrate on studying and get 5 points" };
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}
