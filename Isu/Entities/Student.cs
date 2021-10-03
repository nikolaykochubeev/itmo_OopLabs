using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private const int MinCourse = 1;
        private const int MaxCourse = 4;
        private const int MinGroup = 0;
        private const int MaxGroup = 99;
        private string _groupName;
        private string _megaFacultyCode;
        public Student(Guid id, string name, string groupName, string megaFacultyCode = "M3")
        {
            Id = id;
            Name = name;
            _megaFacultyCode = megaFacultyCode;
            _groupName = groupName;
        }

        public Student(Student student)
        {
            _groupName = student._groupName;
            Name = student.Name;
            Id = student.Id;
        }

        public string Name { get; }
        public Guid Id { get; }
        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!(value.StartsWith(_megaFacultyCode) && value[2] - '0' <= MaxCourse && value[2] - '0' < MinCourse && (value.Length != 5) &&
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