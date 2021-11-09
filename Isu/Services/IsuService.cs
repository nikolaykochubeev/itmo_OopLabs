using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int MinCourse = 1;
        private const int MaxCourse = 4;
        private const int MinGroup = 0;
        private const int MaxGroup = 99;
        private readonly Dictionary<string, Group> _groups = new ();

        public IsuService(int maximumNumberOfStudents)
        {
            MaximumNumberOfStudents = maximumNumberOfStudents;
        }

        private int MaximumNumberOfStudents { get; }

        public Group AddGroup(string name)
        {
            if (!(name.StartsWith("M3") && name[2] - '0' <= MaxCourse && name[2] - '0' >= MinCourse &&
                  (name[3] + name[4] - '0' is >= MinGroup and <= MaxGroup) && name.Length == 5))
                throw new IsuException("Invalid group number");
            _groups[name] = new Group(name, MaximumNumberOfStudents);
            return _groups[name];
        }

        public Student AddStudent(Group @group, string name)
        {
            return group.AddStudent(new Student(Guid.NewGuid(), name, group.GroupName));
        }

        public Student GetStudent(Guid id)
        {
            return _groups
                .Select(@group => @group.Value.GetStudent(id))
                .FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            return _groups.Values.SelectMany(@group => @group.Students.Where(student => student.Name == name)).FirstOrDefault();
        }

        public IReadOnlyList<Student> FindStudents(string groupName)
        {
            return _groups.ContainsKey(groupName) ? _groups[groupName].Students : new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (string groupNumber in _groups.Keys.Where(groupNumber => groupNumber[2] - '0' == courseNumber.Number))
                students.AddRange(_groups[groupNumber].Students);

            return students;
        }

        public Group FindGroup(string groupName)
        {
            return _groups.ContainsKey(groupName) ? _groups[groupName] : null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return (from name in _groups.Keys where name[2] - '0' == courseNumber.Number select _groups[name]).ToList();
        }

        public Group FindGroup(Student student)
        {
            return _groups.Values.FirstOrDefault(@group => @group.Students.Contains(student));
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student != null) newGroup.TransferStudent(student, _groups[FindGroup(student).GroupName]);
        }
    }
}