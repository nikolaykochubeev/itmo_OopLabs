using System;
using System.Collections.Generic;
using Isu.Entities;

namespace Isu.Services
{
    public interface IIsuService
    {
        public Group AddGroup(string name);
        public Student AddStudent(Group @group, string name);

        public Student GetStudent(Guid id);

        public Student FindStudent(string name);

        public IReadOnlyList<Student> FindStudents(string groupName);

        public List<Student> FindStudents(CourseNumber courseNumber);

        public Group FindGroup(string groupName);

        public List<Group> FindGroups(CourseNumber courseNumber);

        public Group FindGroup(Student student);

        public void ChangeStudentGroup(Student student, Group newGroup);
    }
}