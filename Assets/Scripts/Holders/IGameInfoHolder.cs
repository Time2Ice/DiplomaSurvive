namespace DefaultNamespace
{
    public interface IGameInfoHolder
    {
        PlayState CurrentState { get; }
        AbilityDto[] Abilities { get; }
        CourseDto[] Courses { get; }
        ReasonDto[] Reasons { get; }
        int TaskCompletePoints { get; }
        int TaskCompleteCoins { get; }
        int ReasonPossibilityPlus { get; }
        int[] TaskTimes { get; }

        int PrivateLifeClickReduce { get; }
        float PersonalLifeIncreaseTime { get; }
    }
}