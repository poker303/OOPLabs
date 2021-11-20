using System.Collections.Generic;
using Isu.Services;

namespace IsuExtra.Management
{
    public class UpdatedGroup : Group
    {
        public UpdatedGroup(string name, List<StudyDay> timetable)
            : base(name)
        {
            Timetable = timetable;
            Students = new List<UpdatedStudent>();
        }

        public List<StudyDay> Timetable { get; set; }
        public List<UpdatedStudent> Students { get; set; }
    }
}