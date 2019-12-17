namespace DefaultNamespace
{
    public interface IPlayerInfoHolder
    {
        int[] Abilities { get; set; }
        int[] Courses { get; set; }
        string[] Reasons { get; set; }
        int MaxCourse { get; set; }
        int University { get; set; }
        int Coins { get; set; }
        int Points { get; set; }
        int PrivateLife { get; set; }
        int CurrentCourse { get; set; }
        int PossibilityToStay { get; set; }
        int MaxPrivateLife { get; set; }
    }
}