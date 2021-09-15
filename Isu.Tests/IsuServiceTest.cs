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
            //fixed: implement
            _isuService = new IsuServices();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group timeGroup = _isuService.AddGroup("M3204");
            Student timeStudent = _isuService.AddStudent(timeGroup, "Ivanov Alexey Alexandrovich");
            Assert.Contains(timeStudent, timeGroup.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group timeGroup = _isuService.AddGroup("M3204");
                for (int i = 0; i < 24; i++)
                {
                    _isuService.AddStudent(timeGroup, "Syslov Maxim Petrovich" + i);
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var testNameGroupFirst = new Group("IsuKryta");
                var testNameGroupSecond = new Group("123");
                var testNameGroupThird = new Group("M3156");
                var testNameGroupForth = new Group("G887");
                var testNameGroupFifth = new Group("A5051");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = _isuService.AddGroup("M3204");
            Group newGroup = _isuService.AddGroup("M3255");
            Student testStudent = _isuService.AddStudent(oldGroup, "Testovei Personage");
            _isuService.ChangeStudentGroup(testStudent, newGroup);
            Assert.Contains(testStudent, oldGroup.Students);
        }
    }
}