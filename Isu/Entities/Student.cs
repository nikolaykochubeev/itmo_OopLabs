using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private static int _idTemp;
        private string _groupName;

        public Student(string name, string groupName)
        {
            Name = name;
            _groupName = groupName;
            Id = ++_idTemp;
        }

        public Student(Student student)
        {
            _groupName = student._groupName;
            Name = student.Name;
            Id = student.Id;
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                int matches = new Regex(@"^M3[1-4][0-9][0-9]$").Matches(value).Count;
                if (matches != 1)
                    throw new IsuException("Invalid group number ");
                _groupName = value;
            }
        }

        public string Name { get; }
        public int Id { get; }
        public string Info()
        {
            return Name + ' ' + _groupName + ' ' + Id;
        }
    }
}