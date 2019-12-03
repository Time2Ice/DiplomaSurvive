namespace UiScenario.Data
{
    public static class Extensions
    {
        public static bool Contains(this WindowAttribute atr1, WindowAttribute atr2)
        {
            return (atr1 & atr2) == atr2;
        }
    }
}