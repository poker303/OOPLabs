namespace Isu.Services
{
    public class Student
    {
        private static int _totalId;

        public Student(Group nameOfStudentGroup, string name)
        {
            this.Name = name;
            this.NameOfStudentGroup = nameOfStudentGroup;
            this.Id = ++_totalId;
        }

        public string Name { get; private set;  }
        public Group NameOfStudentGroup { get; }
        public int Id { get; private set;  }
    }
}