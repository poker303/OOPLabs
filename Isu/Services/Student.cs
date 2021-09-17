namespace Isu.Services
{
    public class Student
    {
        private static int _totalId;

        public Student(Group group, string name)
        {
            this.Name = name;
            this.GroupName = group;
            this.Id = ++_totalId;
        }

        public string Name { get; set;  }
        public Group GroupName { get; }
        public int Id { get; set;  }
    }
}