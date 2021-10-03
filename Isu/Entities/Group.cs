using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MinCourse = 1;
        private const int MaxCourse = 4;
        private const int MinGroup = 0;
        private const int MaxGroup = 99;
        private string _groupName;
        private string _megaFacultyCode;
        public Group(string groupName, int maximumNumberOfStudents, string megaFacultyCode = "M3")
        {
            _megaFacultyCode = megaFacultyCode;
            GroupName = groupName;
            Students = new List<Student>();
            MaximumNumberOfStudents = maximumNumberOfStudents;
        }

        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!(value.StartsWith(_megaFacultyCode) && value[2] - '0' <= MaxCourse && value[2] - '0' >= MinCourse && (value.Length == 5) &&
                    (value[3] + value[4] - '0' is > MinGroup and < MaxGroup)))
                    throw new IsuException("Invalid group number");
                _groupName = value;
            }
        }

        public List<Student> Students { get; }
        public int MaximumNumberOfStudents { get; }

        public Student AddStudent(Student student)
        {
            if (MaximumNumberOfStudents == Students.Count)
                throw new IsuException("Group is full, student cannot be added");
            Students.Add(student);
            return Students.Last();
        }

        public void TransferStudent(Student student, Group oldGroup)
        {
            oldGroup.Students.Remove(student);
            AddStudent(new Student(student.Id, student.Name, GroupName));
        }

        public Student GetStudent(Guid id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Student GetStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
        }
    }
}