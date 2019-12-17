using System.Collections.Generic;
using System.Globalization;
using UiScenario;
using UiScenario.Concrete.Data;
using UI.Views;
using Assets.Scripts.Handlers;
using Assets.Scripts.Data;
using System;

namespace UI.Controllers
{
    public class TopLobbyMenu : Contractor
    {
        public class Controller : Controller<TopLobbyMenuView>, IDisposable
        {
            private IPersonalLifeHandler _personalLifeHandler;
            private IExperienceHandler _experienceHandler;
            public override WindowType Type => WindowType.TopLobbyMenu;

            Controller(IPersonalLifeHandler personalLifeHandler, IExperienceHandler experienceHandler)
            {
                _personalLifeHandler = personalLifeHandler;
                _experienceHandler = experienceHandler;
                _personalLifeHandler.PersonalLifeChanged += SetPersonalLife;
                _experienceHandler.ExperienceChanged += SetMarks;
            }

            public override void Open(Dictionary<string, object> callData)
            {
                SetPersonalLife(_personalLifeHandler.PrivateLife, _personalLifeHandler.MaxPrivateLife);
                SetMarks(_experienceHandler.Experience, _experienceHandler.MaxExperience);

            }


            private void ShowBalance(int value)
            {
                ConcreteView.SetBalance(value);
            }

            private void ShowUniversityCount(int value)
            {
                ConcreteView.SetUniversityCount(value);
            }

            private void SetSemester(int value)
            {
                ConcreteView.SetSemester(value);
            }

            private void SetContinuePossibility(int value)
            {
                ConcreteView.SetContinuePossibility(value);
            }

            private void SetPersonalLife(int currentValue, int maxValue)
            {
                ConcreteView.SetPersonalLife(currentValue, maxValue);
            }

            private void SetMarks(int currentValue, int maxValue)
            {
                ConcreteView.SetMarks(currentValue, maxValue);
            }

            public void Dispose()
            {
                _personalLifeHandler.PersonalLifeChanged -= SetPersonalLife;
                _experienceHandler.ExperienceChanged -= SetMarks;
            }
        }

        public new class Scenario : Contractor.Scenario
        {
            public override WindowType Type => WindowType.TopLobbyMenu;

            public Scenario(IWindowHandler windowHandler) : base(windowHandler)
            {
            }



        }
    }
}