using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Management
{
    public class MegaFaculty
    {
        public MegaFaculty(string name)
        {
            Name = name;
            Courses = new List<Ognp>();
            Groups = new List<UpdatedGroup>();
        }

        public string Name { get; set; }
        public List<Ognp> Courses { get; set; }
        public List<UpdatedGroup> Groups { get; set; }

        // public void AddGroup(Group group)
        // {
        //     Groups.Add(group);
        // }
        //
        // public void AddCourse(OGNP course)
        // {
        //     Courses.Add(course);
        // }
    }
}