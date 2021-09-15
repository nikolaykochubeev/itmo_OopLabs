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
        public Group(string groupName, int maximumNumberOfStudents)
        {
            GroupName = groupName;
            Students = new List<Student>();
            MaximumNumberOfStudents = maximumNumberOfStudents;
        }

        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!(value.StartsWith("M3") && value[2] - '0' <= MaxCourse && value[2] - '0' >= MinCourse && (value.Length == 5) &&
                    (value[3] + value[4] - '0' is > MinGroup and < MaxGroup)))
                    throw new IsuException("Invalid group number");
                _groupName = value;
            }
        }

        public List<Student> Students { get; }
        public int MaximumNumberOfStudents { get; }

        public Student AddStudent(string name, string groupNumber)
        {
            if (MaximumNumberOfStudents == Students.Count)
                throw new IsuException("Group is full, student cannot be added");
            Students.Add(new Student(name, groupNumber));
            return Students.Last();
        }

        public Student AddStudent(string name, string groupNumber, int id)
        {
            if (MaximumNumberOfStudents == Students.Count)
                throw new IsuException("Group is full, student cannot be added");
            Students.Add(new Student(name, groupNumber, id));
            return Students.Last();
        }

        public void TransferStudent(Student student, Group oldGroup)
        {
            oldGroup.Students.Remove(student);
            AddStudent(student.Name, GroupName, student.Id);
        }

        public Student GetStudent(int id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Student GetStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
        }
    }
}