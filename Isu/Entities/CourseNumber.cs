using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
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
                if (value is > 4 or < 1)
                    throw new IsuException("Invalid course number");
                _number = value;
            }
        }
    }
}