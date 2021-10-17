using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService(20);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            _isuService.AddStudent(_isuService.AddGroup("M3200"), "Иван Иванович");
            CollectionAssert.Contains(_isuService.FindGroup("M3200").Students, _isuService.FindStudent("Иван Иванович"));
        }  

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3200");
                for (int i = 0; i < 100; i++)
                    _isuService.AddStudent(_isuService.FindGroup("M3200"), "Стас Васильев");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3508");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            _isuService.AddGroup("M3200");
            _isuService.AddGroup("M3201");
            _isuService.AddStudent(_isuService.FindGroup("M3201"), "qwerty");
            _isuService.ChangeStudentGroup(_isuService.FindStudent("qwerty"), _isuService.FindGroup("M3200"));
            CollectionAssert.Contains(_isuService.FindGroup("M3200").Students, _isuService.FindStudent("qwerty"));
            CollectionAssert.DoesNotContain(_isuService.FindGroup("M3201").Students, _isuService.FindStudent("qwerty"));
        }
    }
}