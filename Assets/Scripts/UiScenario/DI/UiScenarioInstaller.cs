using Modules;
using UiScenario.CanvasPart;
using UiScenario.Concrete;
using UiScenario.Concrete.Factory;
using UiScenario.Factory;
using UiScenario.Fade;
using Zenject;

namespace UiScenario
{
    public class UiScenarioInstaller : Installer<UiScenarioInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISetCanvasBounds>().To<SetCanvasBounds>().AsSingle();

            Container.Bind<IWindowHandler>().To<WindowHandler>().AsSingle();
            Container.Bind<IWindowBuilder>().To<WindowBuilder>().AsSingle();
            //Canvas===========================================
            Container.Bind<IWindowCanvasConfigurator>().To<WindowCanvasConfigurator>().AsSingle();
            Container.Bind<IWindowCanvasBuilder>().To<WindowCanvasBuilder>().AsSingle();
            Container.Bind<IFadeDataBuilder>().To<FadeDataBuilder>().AsSingle();
//            Container.Bind<IFadeEffectBuilder>().To<WindowFadeCanvasMaskEffectBuilder>().AsSingle();
            Container.Bind<IFadeEffectBuilder>().To<WindowFadeImageEffectBuilder>().AsSingle();
            Container.Bind<IWindowFadeManager>().To<WindowFadeManager>().AsSingle();
        }
    }
}