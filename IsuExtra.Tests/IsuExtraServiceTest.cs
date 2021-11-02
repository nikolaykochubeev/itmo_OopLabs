using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private MegaFacultyService _megaFacultyService;
        private OgnpCourseService _ognpCourseService;
        private IsuService _isuService;
        [SetUp]
        public void Setup()
        {
            _megaFacultyService = new MegaFacultyService();
            _ognpCourseService = new OgnpCourseService();
            _isuService = new IsuService(23);
        }

        [Test]
        public void ReachMaxStudentPerOgnpGroup_ThrowException()
        {
            MegaFaculty megaFaculty = _megaFacultyService.AddMegaFaculty("KTY", "N2");
            OgnpCourse ognpCourse = _ognpCourseService.AddOgnpCourse(megaFaculty, "ognpName");
            ognpCourse.AddGroup(new Group("КИБ 4.2", 12));
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i <= 99; i++)
                    _ognpCourseService.AddStudent(new Student(Guid.NewGuid(), "Bjarne Stroustrup", "N2202", "N2"), "ognpName", "КИБ 4.2");
            });
        }

        [Test]
        public void TryingToAddThirdOgnpCourseToStudent_ThrowException()
        {
            MegaFaculty megaFacultyKty = _megaFacultyService.AddMegaFaculty("KTY", "N2");
            MegaFaculty megaFacultyTint = _megaFacultyService.AddMegaFaculty("TINT", "M3");
            MegaFaculty megaFacultyFtmi = _megaFacultyService.AddMegaFaculty("FTMI", "Z4");
            _ognpCourseService.AddOgnpCourse(megaFacultyKty, "KTY")
                .AddGroup(new Group("Б 4.2", 12));
            _ognpCourseService.AddOgnpCourse(megaFacultyTint, "TINT")
                .AddGroup(new Group("ИБ 4.2", 12));
            _ognpCourseService.AddOgnpCourse(megaFacultyFtmi, "FTMI")
                .AddGroup(new Group("КИБ 4.2", 12));
            var student = new Student(Guid.NewGuid(), "Ivan", "M3101");
            
            _ognpCourseService.AddStudent(student, "KTY", "Б 4.2");
            _ognpCourseService.AddStudent(student, "TINT", "ИБ 4.2");
            Assert.Catch<IsuExtraException>(() =>
            {
                _ognpCourseService.AddStudent(student, "FTMI", "КИБ 4.2");
            });
        }
    }
}