using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        private const int MinCourse = 1;
        private const int MaxCourse = 4;
        private int _number;

        public CourseNumber(int number)
        {
            Number = number;
        }

        public int Number
        {
            get => _number;
            private set
            {
                if (value is > MaxCourse + 1 or < MinCourse)
                    throw new IsuException("Invalid course number");
                _number = value;
            }
        }
    }
}