namespace UiScenario.Fade
{
    public interface IFadeEffect
    {
        void Run(FadeEffectData data);
        void Destroy();
    }
}