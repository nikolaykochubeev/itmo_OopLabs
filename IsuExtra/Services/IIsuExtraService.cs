using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        public MegaFaculty AddMegaFacultyAndOgnp(string name, string prefix);

        public Group AddOgnpGroup(string groupName, int maxNumberOfStudents);

        public Group FindStudentGroup(Guid studentId);
        public Student FindStudent(Guid studentId);

        public int FindAmountStudentsOgnp(Guid studentId);

        public Student AddStudentToOgnpGroup(string groupName, Student student);

        public void RemoveStudentFromOgnp(Guid studentId, string groupName);

        public Thread GetOgnpThread(Guid id);

        public List<Student> GetOgnpGroupListStudents(string groupName);

        public List<Student> GetStudentsWithoutSecondOgnp();
    }
}