using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class StudyStream
    {
        private static int _maxCountOfStudents = 22;

        public StudyStream(string streamName, List<StudyDay> timetable)
        {
            StreamName = streamName;
            Timetable = timetable;
            Students = new List<UpdatedStudent>();
            MaxCountOfStudents = _maxCountOfStudents;
            TestDays = 0;
        }

        public int TestDays { get; set; }
        public List<StudyDay> Timetable { get; set; }
        public string StreamName { get; set; }
        public int MaxCountOfStudents { get; set; }
        public List<UpdatedStudent> Students { get; set; }
    }
}