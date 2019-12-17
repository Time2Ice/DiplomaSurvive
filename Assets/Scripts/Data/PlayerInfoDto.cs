using System;
using Prefabs;

namespace DefaultNamespace
{
    [Serializable]
    public class PlayerInfoDto
    {
        public int[] abilities;
        public int[] courses;
        public string[] reasons;

        public int max_course;
        public int university;
        public int coins;

        public int points;
        public int private_life;
         public int max_private_life;
        public int current_course;

        public int possibility_to_stay;
        public bool AbilitiesEqual(int[] other)
        {
            if (abilities == null || other == null) return false;
            if (abilities.Length != other.Length) return false;
            for (int i = 0; i < abilities.Length; i++)
            {
                if (abilities[i] != other[i]) return false;
            }
            return true;
        }

        public bool CoursesEqual(int[] other)
        {
            if (courses == null || other == null) return false;
            if (courses.Length != other.Length) return false;
            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i] != other[i]) return false;
            }
            return true;
        }

        public bool ReasonsEqual(string[] other)
        {
            if (reasons == null || other == null) return false;
            if (reasons.Length != other.Length) return false;
            for (int i = 0; i < reasons.Length; i++)
            {
                if (reasons[i] != other[i]) return false;
            }
            return true;
        }
    }
}