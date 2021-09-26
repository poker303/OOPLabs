using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const int MaxCountOfStudents = 22;

        public Group(string groupName)
        {
            if (groupName.Length != 5)
            {
                throw new InvalidGroupNameException("The group has the wrong length");

                // throw new InvalidGroupLengthException();
            }

            if (groupName[2] - '0' < 1 || groupName[2] - '0' > 4)
            {
                throw new InvalidGroupNameException("The group has an invalid course");

                // throw new IncorrectOrderOfDigitsException();
            }

            if (!char.IsUpper(groupName[0]) || !int.TryParse(groupName.Substring(1), out int _))
            {
                throw new InvalidGroupNameException("The group can't have such name");

                // throw new InvalidGroupNameException();
            }

            this.GroupName = groupName;
            this.CourseNumber = new CourseNumber(groupName[2] - '0');
            this.Students = new List<Student>();
            this.StudentsOfGroup = Students.AsReadOnly();

            // readonly Students List<Student>();
        }

        public IList<Student> StudentsOfGroup { get; }
        public CourseNumber CourseNumber { get; }
        public string GroupName { get; set; }

        private List<Student> Students { get; }

        public void AddPerson(Student person)
        {
            if (Students.Count >= MaxCountOfStudents)
            {
                throw new LimitStudentsIsuException();
            }

            Students.Add(person);
        }

        public void ExcludePerson(Student person) { Students.Remove(person); }

        public void MovePerson(Student person, Group oldGroup)
        {
            oldGroup.ExcludePerson(person);
            AddPerson(person);
        }
    }
}