namespace DefaultNamespace
{
    public interface IGameInfoHolder
    {
        AbilityDto[] Abilities { get; }
        CourseDto[] Courses { get; }
        ReasonCategory[] ReasonCategories { get; }
        int TaskPrice { get; }
        int ReasonPossibilityPlus { get; }
        int[] TaskTimes { get; }

        int PrivateLifeClickReduce { get; }
        float PersonalLifeIncreaseTime { get; }
    }
}