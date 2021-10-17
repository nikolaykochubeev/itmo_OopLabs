using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService
    {
        private readonly List<MegaFaculty> _megaFaculties = new ();
        private readonly List<OgnpCourse> _ognpCourses = new ();
        private readonly List<Thread> _threads = new ();

        public IsuExtraService()
        {
        }

        public MegaFaculty AddMegaFaculty(string name, string prefix)
        {
            if (_megaFaculties.FirstOrDefault(faculty => faculty.Prefix == prefix) is not null)
                throw new IsuExtraException("MegaFaculty with this prefix already exists");
            _megaFaculties.Add(new MegaFaculty(Guid.NewGuid(), name, prefix));
            return _megaFaculties.Last();
        }

        public OgnpCourse AddOgnpCourse(Guid megaFacultyId, string name)
        {
            MegaFaculty megaFaculty = _megaFaculties.FirstOrDefault(faculty => faculty.Id == megaFacultyId);
            if (megaFaculty is null)
                throw new IsuExtraException("MegaFaculty with such id does not exist");
            if (_ognpCourses.FirstOrDefault(course => course.Id == megaFacultyId) is not null)
                throw new IsuExtraException("ognpCourse for this megafaculty already exist");
            _ognpCourses.Add(new OgnpCourse(megaFacultyId, name, megaFaculty.Prefix));
            return _ognpCourses.Last();
        }

        public Thread AddThread(Guid ognpCourseId, string threadName, int maxNumberOfStudents, Schedule schedule)
        {
            if (_threads.FirstOrDefault(thread => thread.Group.GroupName == threadName) is not null)
                throw new IsuExtraException("Thread with this name already exist");
            OgnpCourse ognpCourse = _ognpCourses.FirstOrDefault(course => course.Id == ognpCourseId);
            if (ognpCourse is null)
                throw new IsuExtraException("ognpCourse doesn't exist");
            _threads.Add(new Thread(threadName, ognpCourse.Prefix, schedule, maxNumberOfStudents));
            return _threads.Last();
        }

        public IEnumerable<Thread> FindStudentOgnpThreads(Guid studentId)
        {
            return _threads.Where(thread => thread.Group.Students.FirstOrDefault(student => student.Id == studentId) != null);
        }

        public Student FindStudent(Guid studentId)
        {
            return _threads.SelectMany(thread => thread.Group.Students).FirstOrDefault(student => student.Id == studentId);
        }

        public IEnumerable<Thread> FindStudentThreads(Guid studentId)
        {
            return _threads.Where(thread => thread.Group.GetStudent(studentId) is not null);
        }

        public Student AddStudentToThread(string threadName, Student student)
        {
            if (FindStudentThreads(student.Id).Count() >= 2)
                throw new IsuExtraException("The student's maximum number ognp has been exceeded");
            Thread thread = _threads.FirstOrDefault(thread => thread.Name == threadName);
            if (thread is null)
                throw new IsuExtraException("Thread doesn't exists");
            if (thread.Group.GetStudent(student.Id) is not null)
                throw new IsuExtraException("The student is already signed up for this ognpThread");
            return _threads.FirstOrDefault(thread1 => thread1.Name == threadName)?.Group.AddStudent(student);
        }

        public Student RemoveStudentFromThread(Guid studentId, string groupName)
        {
            Thread thread = _threads.FirstOrDefault(thread1 => thread1.Group.GroupName == groupName);
            if (thread is null)
                throw new IsuExtraException("Thread doesn't exist");
            if (thread.Group.GetStudent(studentId) is null)
                throw new IsuExtraException("Student doesn't exist in this thread");
            _threads.FirstOrDefault(thread1 => thread1.Group.GroupName == groupName)?.Group.RemoveStudent(studentId);
            return thread.Group.GetStudent(studentId);
        }

        public List<Student> GetStudentsWithoutSecondOgnp()
        {
            var studentsWithoutOgnp = new List<Student>();
            foreach (IReadOnlyList<Student> students in _threads.Select(thread => thread.Group.Students))
                studentsWithoutOgnp.AddRange(students.Where(student => FindStudentThreads(student.Id).Count() < 2));
            return studentsWithoutOgnp;
        }
    }
}