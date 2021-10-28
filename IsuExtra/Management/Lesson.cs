using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class Lesson
    {
        public Lesson(string name, string startTime, int duration, string teacherName, int auditorium)
        {
            Duration = duration;
            if (startTime.Length == 4)
            {
                StartTime = ((startTime[0] - '0') * 60) + ((startTime[2] - '0') * 10) + (startTime[3] - '0');
            }
            else if (startTime.Length == 5)
            {
                StartTime = ((startTime[0] - '0') * 60 * 10) + ((startTime[1] - '0') * 60) +
                            ((startTime[3] - '0') * 10) + (startTime[4] - '0');
            }
            else
            {
                throw new TimeException("The lesson start time is set incorrectly");
            }

            if (StartTime < 500 || StartTime > 1080)
            {
                throw new TimeException("Wrong lesson start time");
            }

            if (StartTime + Duration > 1080)
            {
                throw new TimeException("Duration of lesson is too big");
            }

            EndTime = StartTime + Duration;

            // TimeTable = duration;
            Name = name;

            // Stream = stream;
            TeacherName = teacherName;
            Auditorium = auditorium;
        }

        public string Name { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public int Duration { get; set; }

        // public int TimeTable { get; set; }
        // public StudyStream Stream { get; set; }
        private string TeacherName { get; set; }
        private int Auditorium { get; set; }
    }
}