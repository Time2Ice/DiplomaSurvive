using System;

namespace DefaultNamespace
{
    public interface IPlayerInfoHolder
    {
        event Action<int, int> PersonalLifeChanged;
        event Action<int> CoinsChanged;
        event Action<int> CourseChanged;
        event Action<int> MaxCourseChanged;
        event Action<int> UniversityChanged;
        event Action<int> MaxPrivateLifeChanged;
        event Action<int> PointsChanged;
        event Action<int> ChangedPosition;

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
        int TaskQueueCapasity { get; set; }
        bool IsClassroom { get; set; }

        int MaxPoints { get; set; }
        int UniversityPoints { get; set; }
        int TasksTaken { get; set; }
        string Token { get; set; }
        string Name{ get; set; }
        int Position { get; set; }
    }
}