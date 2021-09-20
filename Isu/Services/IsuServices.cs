using System.Collections.Generic;
using System.Linq;

namespace Isu.Services
{
    public class IsuServices : IIsuService
    {
        private List<Group> _allGroups = new List<Group>();
        public Group AddGroup(string name)
        {
            // var course = new CourseNumber(int.Parse(string.Concat(name[2])));
            var newGroup = new Group(name);
            _allGroups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            var newStudent = new Student(group, name);
            group.AddPerson(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            // var findedId = from n in _allGroups
            //     where n == id
            //     select n;
            // foreach (Group n in _allGroups)
            // {
            //     foreach (var a in n.Students)
            //     {
            //         if (a.Id == id)
            //         {
            //             return a;
            //         }
            //     }
            // }
            return _allGroups.SelectMany(n => n.Students).FirstOrDefault(a => a.Id == id);
        }

        public Student FindStudent(string name)
        {
            return _allGroups.SelectMany(n => n.Students).FirstOrDefault(a => a.NameOfStudent == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from n in _allGroups where n.GroupName == groupName select n.Students).FirstOrDefault();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _allGroups.Where(n => n.CourseNumber == courseNumber).SelectMany(n => n.Students).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _allGroups.FirstOrDefault(a => a.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _allGroups.Where(n => n.CourseNumber == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group n in _allGroups.Where(n => student.NameOfStudentGroup == n))
            {
                newGroup.MovePerson(student, n);
                return;
            }
        }
    }
}