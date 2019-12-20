using System;
using Core.DataProvider;
using DataProvider;
using DefaultNamespace;
using EventAggregator;
using Game;
using Pool;
using Prefabs;
using UiScenario;
using UiScenario.Concrete.Data;
using UiScenario.Concrete.Factory;
using UiScenario.Factory;
using UI.Controllers;
using UI.Views;
using UnityEngine;
using VBCM;
using Assets.Scripts.Data;
using Assets.Scripts.Handlers;
using Manager;
using Unity;
using Assets.Scripts.UI.Controllers;
using DiplomaSurviveDataGenerator;

namespace Installers
{
    public class MainGameUIInstaller : UiScenarioConcreteInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            var asyncProcessor = new GameObject("AsyncProcessor").AddComponent<AsyncProcessor>();
            Container.BindInstance(asyncProcessor);
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

            Container.Bind(typeof(IAppStateHandler))
                .To<AppStateHandler>().AsSingle();
            Container.Bind(typeof(ILocalDataProvider))
                .To<LocalDataProvider>().AsSingle();
            Container.Bind(typeof(ILocalDataWriter))
                .To<LocalDataWriter>().AsSingle();

            BindHandlers();
            BindDataGenerator();
        }

        private void BindDataGenerator()
        {

            Container.Bind(typeof(INumberGenerator))
                  .To<DefaultNumberGenerator>().AsSingle();
            Container.Bind(typeof(IStudyContext))
                  .To<BaseStudyContext>().AsSingle();
            Container.Bind(typeof(IMainContext))
                  .To<BaseMainContext>().AsSingle();
            Container.Bind(typeof(IEventContext))
                  .To<BaseEventContext>().AsSingle();
            Container.Bind(typeof(IScoreContext))
                  .To<BaseScoreContext>().AsSingle();
            Container.Bind(typeof(IPlayContext))
                  .To<BasePlayContext>().AsSingle();
            Container.Bind(typeof(INumberDistribution))
                  .To<UniformDistribution>().AsSingle();
            Container.Bind(typeof(Play)).ToSelf().AsSingle();
            var play = Container.Resolve<Play>();
            var examStore = play.ExamStore;

            Container.Bind(typeof(IExamService))
               .To<ExamService>().AsSingle().WithArguments(examStore);
        }

        private void BindHandlers()
        {
            Container.Bind(typeof(IPlayStateHandler))
                .To<PlayStateHandler>().AsSingle();
            Container.Bind(typeof(IPlayerInfoHolder))
                .To<PlayerInfoHolder>().AsSingle();
            Container.Bind(typeof(IGameInfoHolder))
                .To<GameInfoHolder>().AsSingle();
            Container.Bind(typeof(IExperienceHandler))
                .To<ExperienceHandler>().AsSingle();
            Container.Bind(typeof(ITasksHandler))
                .To<TasksHandler>().AsSingle();
            Container.Bind(typeof(ITaskMessagesHandler))
               .To<TaskMessagesHandler>().AsSingle();
            Container.Bind(typeof(IPersonalLifeHandler))
                .To<PersonalLifeHandler>().AsSingle();
            Container.Bind(typeof(IReasonHandler))
               .To<ReasonHandler>().AsSingle();
        }
        protected override void BindViews()
        {
            BindView(WindowType.Courses);
            BindView(WindowType.Reasons);
            BindView(WindowType.RecordBook);
            BindView(WindowType.Abilities);
            BindView(WindowType.TopLobbyMenu);
            BindView(WindowType.DownLobbyMenu);
            BindView(WindowType.GoUp);
            BindView(WindowType.ReasonInfo);
            BindView(WindowType.SendDown);
            BindView(WindowType.Characters);
            BindView(WindowType.Test);
        }

        protected override void BindControllers()
        {
            Container.Bind(typeof(Courses.Controller), typeof(Courses.CloseClickEvent.ISubscribed))
                .To<Courses.Controller>().AsSingle();

            Container.Bind(typeof(Abilities.Controller), typeof(Abilities.CloseClickEvent.ISubscribed),
            typeof(Abilities.BuyClickEvent.ISubscribed))
                .To<Abilities.Controller>().AsSingle();

            Container.Bind(typeof(DownLobbyMenu.Controller))
                .To<DownLobbyMenu.Controller>().AsSingle();

            Container.Bind(typeof(GoUp.Controller), typeof(GoUp.CloseClickEvent.ISubscribed))
                .To<GoUp.Controller>().AsSingle();

            Container.Bind(typeof(ReasonInfo.Controller), typeof(ReasonInfo.CloseClickEvent.ISubscribed))
                .To<ReasonInfo.Controller>().AsSingle();

            Container.Bind(typeof(Reasons.Controller), typeof(IPublisherAgg),
                    typeof(Reasons.CloseClickEvent.ISubscribed), typeof(Reasons.ChooseReasonEvent.ISubscribed))
                .To<Reasons.Controller>().AsSingle();

            Container.Bind(typeof(RecordBook.Controller), typeof(RecordBook.CloseClickEvent.ISubscribed))
                .To<RecordBook.Controller>().AsSingle();

            Container.Bind(typeof(SendDown.Controller), typeof(SendDown.RestudyEvent.ISubscribed))
                .To<SendDown.Controller>().AsSingle();

            Container.Bind(typeof(TopLobbyMenu.Controller), typeof(IDisposable))
                 .To<TopLobbyMenu.Controller>().AsSingle();

            Container.Bind(typeof(Characters.Controller), typeof(IDisposable), typeof(Characters.TeacherClickedEvent.ISubscribed), typeof(Characters.HeroClickedEvent.ISubscribed))
               .To<Characters.Controller>().AsSingle();

            Container.Bind(typeof(TestPopup.Controller), typeof(TestPopup.FirstButtonClicked.ISubscribed), typeof(TestPopup.SecondButtonClicked.ISubscribed))
              .To<TestPopup.Controller>().AsSingle();

        }

        protected override void BindScenarios()
        {
            Container.Bind(typeof(Abilities.Scenario)).To<Abilities.Scenario>().AsSingle();

            Container.Bind(typeof(Reasons.Scenario), typeof(Reasons.OpenReasonInfo.ISubscribed))
                .To<Reasons.Scenario>().AsSingle();

            Container.Bind(typeof(RecordBook.Scenario), typeof(RecordBook.OpenWindowEvent.ISubscribed))
                .To<RecordBook.Scenario>().AsSingle();

            Container.Bind(typeof(SendDown.Scenario)).To<SendDown.Scenario>().AsSingle();

            Container.Bind(typeof(TopLobbyMenu.Scenario)).To<TopLobbyMenu.Scenario>().AsSingle();

            Container.Bind(typeof(Courses.Scenario)).To<Courses.Scenario>().AsSingle();

            Container.Bind(typeof(DownLobbyMenu.Scenario), typeof(DownLobbyMenu.OpenWindowEvent.ISubscribed)).To<DownLobbyMenu.Scenario>().AsSingle();

            Container.Bind(typeof(ReasonInfo.Scenario)).To<ReasonInfo.Scenario>().AsSingle();

            Container.Bind(typeof(GoUp.Scenario)).To<GoUp.Scenario>().AsSingle();
            Container.Bind(typeof(Characters.Scenario)).To<Characters.Scenario>().AsSingle();
            Container.Bind(typeof(TestPopup.Scenario)).To<TestPopup.Scenario>().AsSingle();

        }

        protected override void BindEvents()
        {
            Container.Bind<Abilities.BuyClickEvent>().AsSingle();
            Container.Bind<Abilities.CloseClickEvent>().AsSingle();

            Container.Bind<Reasons.OpenReasonInfo>().AsSingle();
            Container.Bind<Reasons.ChooseReasonEvent>().AsSingle();
            Container.Bind<Reasons.CloseClickEvent>().AsSingle();

            Container.Bind<RecordBook.OpenWindowEvent>().AsSingle();
            Container.Bind<RecordBook.CloseClickEvent>().AsSingle();

            Container.Bind<SendDown.RestudyEvent>().AsSingle();

            Container.Bind<Courses.CloseClickEvent>().AsSingle();

            Container.Bind<DownLobbyMenu.OpenWindowEvent>().AsSingle();

            Container.Bind<ReasonInfo.CloseClickEvent>().AsSingle();

            Container.Bind<GoUp.CloseClickEvent>().AsSingle();
            Container.Bind<Characters.TeacherClickedEvent>().AsSingle();
            Container.Bind<Characters.HeroClickedEvent>().AsSingle();

            Container.Bind<TestPopup.FirstButtonClicked>().AsSingle();
            Container.Bind<TestPopup.SecondButtonClicked>().AsSingle();
        }

        public override IWindowController CreateWindowController(WindowType type)
        {
            switch (type)
            {
                case WindowType.Abilities:
                    return Container.Resolve<Abilities.Controller>();
                case WindowType.Courses:
                    return Container.Resolve<Courses.Controller>();
                case WindowType.DownLobbyMenu:
                    return Container.Resolve<DownLobbyMenu.Controller>();
                case WindowType.GoUp:
                    return Container.Resolve<GoUp.Controller>();
                case WindowType.ReasonInfo:
                    return Container.Resolve<ReasonInfo.Controller>();
                case WindowType.Reasons:
                    return Container.Resolve<Reasons.Controller>();
                case WindowType.RecordBook:
                    return Container.Resolve<RecordBook.Controller>();
                case WindowType.SendDown:
                    return Container.Resolve<SendDown.Controller>();
                case WindowType.TopLobbyMenu:
                    return Container.Resolve<TopLobbyMenu.Controller>();
                case WindowType.Characters:
                    return Container.Resolve<Characters.Controller>();
                case WindowType.Test:
                    return Container.Resolve<TestPopup.Controller>();
                default:
                    throw new Exception($"Invalid window type : {type}");
            }
        }

        public override IWindowScenario CreateWindowScenario(WindowType type)
        {
            switch (type)
            {
                case WindowType.Abilities:
                    return Container.Resolve<Abilities.Scenario>();
                case WindowType.Courses:
                    return Container.Resolve<Courses.Scenario>();
                case WindowType.DownLobbyMenu:
                    return Container.Resolve<DownLobbyMenu.Scenario>();
                case WindowType.GoUp:
                    return Container.Resolve<GoUp.Scenario>();
                case WindowType.ReasonInfo:
                    return Container.Resolve<ReasonInfo.Scenario>();
                case WindowType.Reasons:
                    return Container.Resolve<Reasons.Scenario>();
                case WindowType.RecordBook:
                    return Container.Resolve<RecordBook.Scenario>();
                case WindowType.SendDown:
                    return Container.Resolve<SendDown.Scenario>();
                case WindowType.TopLobbyMenu:
                    return Container.Resolve<TopLobbyMenu.Scenario>();
                case WindowType.Characters:
                    return Container.Resolve<Characters.Scenario>();
                case WindowType.Test:
                    return Container.Resolve<TestPopup.Scenario>();

                default:
                    throw new Exception($"Invalid scenario type : {type}");
            }
        }
    }

}