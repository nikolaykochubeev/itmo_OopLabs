using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuExtraService _isuExtraService;
        private IsuService _isuService;
        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _isuService = new IsuService(23);
        }

        [Test]
        public void ReachMaxStudentPerOgnpGroup_ThrowException()
        {
            MegaFaculty megaFaculty = _isuExtraService.AddMegaFaculty("KTY", "N2");
            OgnpCourse ognpCourse = _isuExtraService.AddOgnpCourse(megaFaculty.Id, megaFaculty.Name);
            _isuExtraService.AddThread(ognpCourse.Id, "N2112", 12, new Schedule(new List<Pair>()));
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i <= 99; i++)
                    _isuExtraService.AddStudentToThread("N2112", new Student(Guid.NewGuid(), "Ivan", "M3101"));
            });
        }
        //
        // [Test]
        // public void CreateOgnpGroupWithInvalidName_ThrowException()
        // {
        //     MegaFaculty megaFaculty = _isuExtraService.AddMegaFacultyAndOgnp("KTY", "N2");
        //     OgnpCourse ognpCourse = megaFaculty.OgnpCourseMegafaculty;
        //     Assert.Catch<IsuException>(() =>
        //     {
        //         _isuExtraService.AddOgnpGroup(ognpCourse.Id, "M3112", 12);
        //     });
        // }
        //
        // [Test]
        // public void ReachMaxOgnpGroupAmount_ThrowException()
        // {
        //     MegaFaculty megaFacultyKty = _isuExtraService.AddMegaFacultyAndOgnp("KTY", "N2");
        //     MegaFaculty megaFacultyTint = _isuExtraService.AddMegaFacultyAndOgnp("TINT", "M3");
        //     MegaFaculty megaFacultyFtmi = _isuExtraService.AddMegaFacultyAndOgnp("FTMI", "Z4");
        //     _isuExtraService.AddOgnpGroup(megaFacultyKty.OgnpCourseMegafaculty.Id, "N2112", 12);
        //     _isuExtraService.AddOgnpGroup(megaFacultyTint.OgnpCourseMegafaculty.Id, "M3112", 12);
        //     _isuExtraService.AddOgnpGroup(megaFacultyFtmi.OgnpCourseMegafaculty.Id, "Z4112", 12);
        //     var student = new Student(Guid.NewGuid(), "Ivan", "M3101");
        //     _isuExtraService.AddStudentToOgnpGroup("N2112", student);
        //     _isuExtraService.AddStudentToOgnpGroup("M3112", student);
        //     Assert.Catch<IsuExtraException>(() =>
        //     {
        //         _isuExtraService.AddStudentToOgnpGroup("Z4112", student);
        //     });
        // }
    }
}