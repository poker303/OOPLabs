using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const int MaxCountOfStudents = 22;
        private int _studentsPerGroup;

        public Group(string groupName)
        {
            if (groupName.Length != 5)
            {
                throw new InvalidGroupLengthException();
            }

            if (groupName[2] - '0' < 1 || groupName[2] - '0' > 4)
            {
                throw new IncorrectOrderOfDigitsException();
            }

            if (!char.IsUpper(groupName[0]) || !int.TryParse(groupName.Substring(1), out int _))
            {
                throw new InvalidGroupNameException();
            }

            this.GroupName = groupName;
            this.CourseNumber = new CourseNumber(groupName[2]);
            this.Students = new List<Student>();
        }

        public List<Student> Students { get; set; }
        public CourseNumber CourseNumber { get; }
        public string GroupName { get; set; }

        public void AddPerson(Student person)
        {
            if (_studentsPerGroup >= MaxCountOfStudents)
            {
                throw new LimitStudentsIsuException();
            }
            else
            {
                ++_studentsPerGroup;
                Students.Add(person);
            }
        }

        public void DisposalPerson(Student person) { Students.Remove(person); }

        public void ShiftingPerson(Student person, Group oldGroup)
        {
            oldGroup.DisposalPerson(person);
            AddPerson(person);
        }
    }
}