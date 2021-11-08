using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class Lesson
    {
        public Lesson(string name, TimeSpan startTime, string teacherName, int auditorium)
        {
            var beginningOfSchoolDay = new TimeSpan(8, 20, 0);
            var endOfSchoolDay = new TimeSpan(20, 0, 0);
            Duration = new TimeSpan(1, 30, 0);
            StartTime = startTime;

            if (StartTime < beginningOfSchoolDay || StartTime > endOfSchoolDay)
            {
                throw new TimeException("Wrong lesson start time");
            }

            if (StartTime.Add(Duration) > endOfSchoolDay)
            {
                throw new TimeException("Duration of lesson is too big");
            }

            EndTime = StartTime.Add(Duration);

            // TimeTable = duration;
            Name = name;

            // Stream = stream;
            TeacherName = teacherName;
            Auditorium = auditorium;
        }

        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration { get; set; }

        // public int TimeTable { get; set; }
        // public StudyStream Stream { get; set; }
        private string TeacherName { get; set; }
        private int Auditorium { get; set; }
    }
}