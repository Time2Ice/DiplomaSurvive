using System;

namespace DefaultNamespace
{
    [Serializable]
    public struct GameInfoDto
    {
        public AbilityDto[] abilities;
        public CourseDto[] courses;
        public ReasonCategory[] reason_categories;

        public int task_price;
        public int reason_possibility_plus;
        public int[] task_times;
        public int personal_life_click_reduce;
        public float personal_life_increase_time;
    }
}