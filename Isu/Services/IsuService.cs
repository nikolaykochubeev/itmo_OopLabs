using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly Dictionary<string, Group> _groups = new ();

        public IsuService(int maximumNumberOfStudents)
        {
            MaximumNumberOfStudents = maximumNumberOfStudents;
        }

        private int MaximumNumberOfStudents { get; }

        public Group AddGroup(string name)
        {
            _groups[name] = new Group(name, MaximumNumberOfStudents);
            return _groups[name];
        }

        public Student AddStudent(Group @group, string name)
        {
            return group.AddStudent(name, group.GroupName);
        }

        public Student GetStudent(int id)
        {
            return _groups
                .Select(@group => @group.Value.GetStudent(id))
                .FirstOrDefault();
        }

        public Student FindStudent(string name)
        {
            return _groups.Select(a => a.Value.GetStudent(name)).FirstOrDefault();
        }

        public List<Student> FindStudents(string groupName)
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

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            newGroup.TransferStudent(student, _groups[student.GroupName]);
        }
    }
}