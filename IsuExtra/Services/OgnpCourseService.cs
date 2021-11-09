using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class OgnpCourseService
    {
        private readonly List<OgnpCourse> _ognpCourses;

        public OgnpCourseService(List<OgnpCourse> ognpCourses = null)
        {
            _ognpCourses = ognpCourses ?? new List<OgnpCourse>();
        }

        public IReadOnlyList<OgnpCourse> OgnpCourses => _ognpCourses;

        public OgnpCourse AddOgnpCourse(MegaFaculty megaFaculty, string ognpCourseName)
        {
            if (_ognpCourses.FirstOrDefault(course => course.Prefix == megaFaculty.Prefix) is not null)
                throw new IsuExtraException("Ognp course for this megafaculty already exist");
            _ognpCourses.Add(new OgnpCourse(ognpCourseName, megaFaculty.Prefix));
            return OgnpCourses.Last();
        }

        public IEnumerable<OgnpCourse> FindStudentOgnpCourses(Guid studentId)
        {
            return _ognpCourses.Where(course =>
                course.Groups.FirstOrDefault(groupExtended => groupExtended.Group.GetStudent(studentId) is not null) is
                    not null);
        }

        public Student FindStudent(Guid studentId)
        {
            return _ognpCourses.SelectMany(thread => thread.Groups).SelectMany(extended => extended.Group.Students)
                .FirstOrDefault(student => student.Id == studentId);
        }

        public OgnpCourse FindOgnpCourseByName(string ognpCourseName)
        {
            return _ognpCourses.FirstOrDefault(course => course.Name == ognpCourseName);
        }

        public List<Student> GetStudentsWithoutSecondOgnp()
        {
            var studentsWithoutOgnp = new List<Student>();
            foreach (IReadOnlyList<Student> students in _ognpCourses.SelectMany(thread => thread.Groups)
                .Select(extended => extended.Group.Students))
                studentsWithoutOgnp.AddRange(students.Where(student => FindStudentOgnpCourses(student.Id).Count() < 2));
            return studentsWithoutOgnp;
        }

        public List<Lesson> GetStudentsListLessons(Guid studentId)
        {
            var lessons = new List<Lesson>();
            lessons.AddRange(_ognpCourses.SelectMany(course => course.Groups)
                .Where(group => group.Group.GetStudent(studentId) is not null)
                .SelectMany(group1 => group1.Schedule.Lessons));
            return lessons;
        }

        public GroupExtended FindGroupByGroupNameAndCourseName(string groupName, string courseName)
        {
            return FindOgnpCourseByName(courseName).Groups
                .FirstOrDefault(extended => extended.Group.GroupName == groupName);
        }

        public Student AddStudent(Student student, string ognpCourseName, string groupName)
        {
            if (FindOgnpCourseByName(ognpCourseName) is null)
                throw new IsuExtraException("Ognp course with this name does not exists");
            if (FindOgnpCourseByName(ognpCourseName).Groups
                .FirstOrDefault(extended => extended.Group.GroupName == groupName) is null)
                throw new IsuExtraException("Group with this name does not exists");
            if (FindOgnpCourseByName(ognpCourseName).Groups
                .FirstOrDefault(extended => extended.Group.GetStudent(student.Id) is not null) is not null)
                throw new IsuExtraException("Student already added to the this group");
            if (FindStudentOgnpCourses(student.Id).Count() >= 2)
                throw new IsuExtraException("the student is already signed up for two ognp courses");

            if (!FindGroupByGroupNameAndCourseName(groupName, ognpCourseName).Schedule.InvarianceIntersectionCheck(GetStudentsListLessons(student.Id)))
                throw new IsuExtraException(" ");
            FindGroupByGroupNameAndCourseName(groupName, ognpCourseName).Group.AddStudent(student);

            return student;
        }

        public void RemoveStudent(Guid studentId, string ognpCourseName, string groupName)
        {
            if (FindOgnpCourseByName(ognpCourseName) is null)
                throw new IsuExtraException("Ognp course with this name does not exists");
            if (FindOgnpCourseByName(ognpCourseName).Groups
                .FirstOrDefault(extended => extended.Group.GroupName == groupName) is null)
                throw new IsuExtraException("Group with this name does not exists");
            if (FindOgnpCourseByName(ognpCourseName).Groups
                .FirstOrDefault(extended =>
                    extended.Group.GetStudent(studentId) is not null &&
                    extended.Group.GroupName == groupName) is not null)
                return;
            FindGroupByGroupNameAndCourseName(groupName, ognpCourseName).Group.RemoveStudent(studentId);
        }
    }
}