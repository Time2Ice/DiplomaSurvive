using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct GameInfoDto
    {
        public AbilityDto[] abilities;
        public CourseDto[] courses;
        public ReasonDto[] reasons;

        public int task_coins;
        public int task_points;

        public int reason_possibility_plus;
        public int[] task_times;
        public int personal_life_click_reduce;
        public float personal_life_increase_time;
    }
}