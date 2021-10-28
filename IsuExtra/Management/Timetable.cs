using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class Timetable
    {
        public Timetable(Group certainGroup, List<StudyDay> days)
        {
            CertainGroup = certainGroup;
            Days = days;
        }

        public Group CertainGroup { get; set; }
        public List<StudyDay> Days { get; set; }
    }
}