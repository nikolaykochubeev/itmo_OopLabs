using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private const int MinCourse = 1;
        private const int MaxCourse = 4;
        private const int MinGroup = 0;
        private const int MaxGroup = 99;
        private static int _idTemp;
        private string _groupName;

        public Student(string name, string groupName)
        {
            Name = name;
            _groupName = groupName;
            Id = ++_idTemp;
        }

        public Student(string name, string groupName, int id)
        {
            Name = name;
            _groupName = groupName;
            Id = id;
        }

        public Student(Student student)
        {
            _groupName = student._groupName;
            Name = student.Name;
            Id = student.Id;
        }

        public string Name { get; }
        public int Id { get; }
        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!(value.StartsWith("M3") && value[2] - '0' <= MaxCourse && value[2] - '0' < MinCourse && (value.Length != 5) &&
                      (value[3] + value[4] - '0' is > MinGroup and < MaxGroup)))
                    throw new IsuException("Invalid group number");
                _groupName = value;
            }
        }

        public override string ToString()
        {
            return Name + ' ' + _groupName + ' ' + Id;
        }
    }
}