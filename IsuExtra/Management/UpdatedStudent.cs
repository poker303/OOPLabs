using System;
using System.Collections.Generic;

namespace IsuExtra.Management
{
    public class UpdatedStudent : Isu.Services.Student
    {
        public UpdatedStudent(UpdatedGroup updatedGroup, string name)
        : base(updatedGroup, name)
        {
            StudentTimetable = updatedGroup.Timetable;
            Counter = 0;
        }

        public List<StudyDay> StudentTimetable { get; set; }
        public int Counter { get; set; }
    }
}