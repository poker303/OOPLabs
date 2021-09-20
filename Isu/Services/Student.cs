namespace Isu.Services
{
    public class Student
    {
        private static int _totalId;

        public Student(Group group, string name)
        {
            this.Name = name;
            this.Group = group;
            this.Id = ++_totalId;
        }

        public string Name { get; private set;  }
        public Group Group { get; }
        public int Id { get; private set;  }
    }
}