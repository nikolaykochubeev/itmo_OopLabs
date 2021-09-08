using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
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
                int matches = new Regex(@"^M3[1-4][0-9][0-9]$").Matches(value).Count;
                if (matches != 1)
                    throw new IsuException("Invalid group number");
                _groupName = value;
            }
        }

        public List<Student> Students { get; }
        public int MaximumNumberOfStudents { get; }

        public Student AddStudent(string name, string groupNumber)
        {
            IsFull();
            Students.Add(new Student(name, groupNumber));
            return Students.Last();
        }

        public void TransferStudent(Student student, Group oldGroup)
        {
            oldGroup.RemoveStudent(student);
            student.GroupName = _groupName;
            AddStudent(student);
        }

        public Student GetStudent(int id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Student GetStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
        }

        private Student AddStudent(Student student)
        {
            IsFull();
            Students.Add(student);
            return Students.Last();
        }

        private bool RemoveStudent(Student student)
        {
            return Students.Remove(student);
        }

        private void IsFull()
        {
            if (MaximumNumberOfStudents == Students.Count)
                throw new IsuException("Group is full, student cannot be added");
        }
    }
}