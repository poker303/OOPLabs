namespace Isu.Services
{
    public class Student
    {
        private static int _totalId;

        public Student(string groupName, string name)
        {
            this.Name = name;
            this.GroupName = groupName;
            this.Id = ++_totalId;
        }

        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Id { get; set; }
    }
}