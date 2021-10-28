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

            // foreach (StudyDay day in timetable)
            // {
            //     for (int lesson = 0; lesson < day.Lessons.Count; lesson++)
            //     {
            //         if (day.Lessons[lesson].EndTime > day.Lessons[lesson + 1].StartTime)
            //         {
            //             throw new TimeException("Incorrect schedule, pairs intersect");
            //         }
            //     }
            // }
            Timetable = timetable;
            Students = new List<UpdatedStudent>();

            // StudentsOfStream = Students.AsReadOnly();
            MaxCountOfStudents = _maxCountOfStudents;

            // readonly Students List<Student>();
        }

        public List<StudyDay> Timetable { get; set; }

        // public IReadOnlyList<UpdatedStudent> StudentsOfStream { get; }
        public string StreamName { get; set; }
        public int MaxCountOfStudents { get; set; }
        public List<UpdatedStudent> Students { get; set; }

        // private List<UpdatedStudent> Students { get; }
    }
}