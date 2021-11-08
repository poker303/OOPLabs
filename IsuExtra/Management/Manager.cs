using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class Manager : IManager
    {
        public void AddStudent(List<UpdatedStudent> students, UpdatedGroup group)
        {
            group.Students.AddRange(students.Where(student => !group.Students.Contains(student)));
        }

        public void AddGroup(MegaFaculty faculty, string groupName, List<StudyDay> timetable)
        {
            var newGroup = new UpdatedGroup(groupName, timetable);
            if (faculty.Name[0] == groupName[0])
            {
                faculty.Groups.Add(newGroup);
            }
            else
            {
                throw new InvalidGroupNameException("Wrong name of group - rename it");
            }
        }

        public void AddCourse(MegaFaculty faculty, string courseName)
        {
            var newCourse = new Ognp(courseName);
            if (faculty.Name[0] == courseName[1])
            {
                faculty.Courses.Add(newCourse);
            }
            else
            {
                throw new InvalidGroupNameException("Wrong name of course - rename it");
            }
        }

        public List<StudyStream> FindingStreams(Ognp course)
        {
            return course.StudyStreams;
        }

        public List<UpdatedStudent> FindingStudents(StudyStream stream)
        {
            return stream.Students;
        }

        public void StudentSignUp(UpdatedStudent student, Ognp ognp)
        {
            var addedSchedule = new List<StudyDay>();
            int suitableLessons = 0;
            bool flag = false;
            if (student.Counter == 2)
            {
                throw new SignUpException("The student has already been enrolled for the maximum number of OGNP");
            }

            if (student.Group.GroupName[0] == ognp.Name[1])
            {
                throw new SignUpException("The group and the OGNP are located in the same faculty");
            }
            else
            {
                if (ognp.StudyStreams.Any(stream => stream.Students.Contains(student)))
                {
                    flag = true;
                }

                if (flag == false)
                {
                    foreach (StudyStream stream in ognp.StudyStreams.Where(stream => stream.Students.Count < stream.MaxCountOfStudents))
                    {
                        for (int day = 1; day <= 6; day++)
                        {
                            foreach (Lesson lesson in stream.Timetable[day - 1].Lessons)
                            {
                                if (student.StudentTimetable[day - 1].Lessons.Count == 0)
                                {
                                    suitableLessons = stream.Timetable[day - 1].Lessons.Count;
                                }

                                if (student.StudentTimetable[day - 1].Lessons.Where((prevLesson, j) =>
                                    prevLesson.EndTime < lesson.StartTime &&
                                    lesson.EndTime < student.StudentTimetable[day - 1].Lessons[j + 1].StartTime).Any())
                                {
                                    suitableLessons++;
                                }
                            }

                            if (suitableLessons == stream.Timetable[day - 1].Lessons.Count)
                            {
                                addedSchedule.Add(stream.Timetable[day - 1]);
                            }

                            suitableLessons = 0;
                        }

                        bool isNoEquals = stream.Timetable.Any(x => !addedSchedule.Contains(x));
                        if (!isNoEquals)
                        {
                            stream.Students.Add(student);
                            student.Counter++;
                            for (int date = 1; date <= 6; date++)
                            {
                                foreach (Lesson lesson in stream.Timetable[date - 1].Lessons)
                                {
                                    student.StudentTimetable[date - 1].Lessons.Add(lesson);
                                }
                            }

                            addedSchedule.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    throw new SignUpException("The student has already signed up for this OGNP");
                }
            }
        }

        public void RemovingRecord(UpdatedStudent student, Ognp ognp)
        {
            StudyStream desireStream = ognp.StudyStreams.FirstOrDefault();
            foreach (StudyStream stream in ognp.StudyStreams.Where(stream => stream.Students.Contains(student)))
            {
                desireStream = stream;
            }

            for (int date = 1; date <= 6; date++)
            {
                foreach (Lesson lesson in desireStream.Timetable[date - 1].Lessons)
                {
                    student.StudentTimetable[date - 1].Lessons.Remove(lesson);
                }
            }

            student.Counter--;
        }

        public List<UpdatedStudent> SearchUnregisteredStudents(UpdatedGroup group)
        {
            return group.Students.Where(student => student.Counter == 0).ToList();
        }
    }
}