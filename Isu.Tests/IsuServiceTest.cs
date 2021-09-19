using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
using Isu.Entities;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService(30);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group G = _isuService.AddGroup("M3204");
            Student S = _isuService.AddStudent(G, "Valeriy Zhmushenko");
            Assert.AreEqual(S.GroupName, G.GroupName);
            Assert.Contains(S, G.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group G = _isuService.AddGroup("M3204");
                for (int i = 0; i < 33; i++)
                { 
                    _isuService.AddStudent(G, "Valeriy Zhmushenko");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group G = _isuService.AddGroup("M2345");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup("M3200");
                Group group2 = _isuService.AddGroup("M3204");
                Student student1 = _isuService.AddStudent(group1, "Valeriy Zhmushenko");
                _isuService.ChangeStudentGroup(student1, group2);
            });
        }
    }
}
