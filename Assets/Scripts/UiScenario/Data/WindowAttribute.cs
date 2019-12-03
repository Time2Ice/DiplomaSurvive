namespace UiScenario.Data
{
    public enum WindowAttribute
    {
        None = 1,
        //elements of the graphical interface - requiring a specific user action, which does not allow switching to other similar objects
        Modal = 1 << 1,
        IgnoreSort = 1 << 2,
        //It is the goal of adaptation for devices like IPhoneX
        IphoneXAdapt = 1 << 3
    }
}