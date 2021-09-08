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
            Assert.AreEqual(_isuService.FindGroup("M3200").GetStudent("Иван Иванович").GroupName, _isuService.FindStudent("Иван Иванович").GroupName);
            Assert.Contains(_isuService.FindStudent("Иван Иванович"), _isuService.FindGroup("M3200").Students);
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
            _isuService.AddGroup("M3201");
            _isuService.ChangeStudentGroup(_isuService.AddStudent(_isuService.FindGroup("M3201"), "Иван Иванович"), _isuService.AddGroup("M3200"));
            Assert.Contains(_isuService.FindStudent("Иван Иванович"), _isuService.FindGroup("M3200").Students);
            CollectionAssert.DoesNotContain(_isuService.FindGroup("M3201").Students, _isuService.FindStudent("Иван Иванович"));
        }
    }
}