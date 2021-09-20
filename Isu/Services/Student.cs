namespace Isu.Services
{
    public class Student
    {
        private static int _totalId;

        public Student(Group nameOfStudentGroup, string nameOfStudent)
        {
            this.NameOfStudent = nameOfStudent;
            this.NameOfStudentGroup = nameOfStudentGroup;
            this.Id = ++_totalId;
        }

        public string NameOfStudent { get; set;  }
        public Group NameOfStudentGroup { get; }
        public int Id { get; set;  }
    }
}