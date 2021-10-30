using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class StudyDay
    {
        public StudyDay(string name, List<Lesson> lessons)
        {
            Name = name;
            Lessons = lessons;
        }

        public string Name { get; set; }
        public List<Lesson> Lessons { get; set; }

        public void AddLesson(Lesson lesson)
        {
            for (int i = 0; i < Lessons.Count; i++)
            {
                if ((Lessons[i].StartTime + Lessons[i].Duration < lesson.StartTime) &&
                    (lesson.StartTime + lesson.Duration < Lessons[i + 1].StartTime))
                {
                    if (!Lessons.Contains(lesson))
                    {
                        Lessons.Add(lesson);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    throw new CreateException("You can't add a lesson the time is already taken");
                }
            }
        }
    }
}