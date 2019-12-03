using Zenject;

namespace EventAggregator
{
    public class EventAggregatorInstaller : Installer<EventAggregatorInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventAggregator>().To<EventAggregator>().AsSingle();
            Container.Bind<ISubscriber>().To<Subscriber>().AsSingle();
            Container.Bind<IBinderAgg>().To<BinderAgg>().AsSingle().NonLazy();
            Container.Bind<IValidator>().To<Validator>().AsSingle();
        }
    }
}