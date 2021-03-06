using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Management;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IManager _manager;
        
        [SetUp]
        public void Setup()
        {
            _manager = new Manager();
        }

        [Test]

        public void AddOGNP_SignUp_Record_Checking()
        {
            var robotics = new MegaFaculty("Robotics");
            var programming = new MegaFaculty("Programming");
            
            
            _manager.AddCourse(robotics, "2R_CyberBez");
            Ognp course1 = robotics.Courses[0];
            Assert.Contains(course1, robotics.Courses);
            
            var date1 = new List<Lesson>() { };
            var date2 = new List<Lesson>() { };
            var date3 = new List<Lesson>()
            {
                new Lesson("CyberBez", new TimeSpan(10,0,0), "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", new TimeSpan(11,40,0), "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", new TimeSpan(13,30,0), "Gavrichenko Alexander", 306)
            };
            var date4 = new List<Lesson>() { };
            var date5 = new List<Lesson>() { };
            var date6 = new List<Lesson>() { };
            
            var streamTimetable1 = new List<StudyDay>()
            {
                new StudyDay((DayOfWeek) 1, date1),
                new StudyDay((DayOfWeek) 2, date2),
                new StudyDay((DayOfWeek) 3, date3),
                new StudyDay((DayOfWeek) 4, date4),
                new StudyDay((DayOfWeek) 5, date5),
                new StudyDay((DayOfWeek) 6, date6),
            };
            
            var dates1 = new List<Lesson>() { };
            var dates2 = new List<Lesson>() { };
            var dates3 = new List<Lesson>(){ };
            var dates4 = new List<Lesson>() { };
            var dates5 = new List<Lesson>() { };
            var dates6 = new List<Lesson>()
            {
                new Lesson("CyberBez", new TimeSpan(10,0,0), "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", new TimeSpan(11,40,0), "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", new TimeSpan(13,30,0), "Gavrichenko Alexander", 306)
            };
            
            var streamTimetable2 = new List<StudyDay>()
            {
                new StudyDay((DayOfWeek) 1, dates1),
                new StudyDay((DayOfWeek) 2, dates2),
                new StudyDay((DayOfWeek) 3, dates3),
                new StudyDay((DayOfWeek) 4, dates4),
                new StudyDay((DayOfWeek) 5, dates5),
                new StudyDay((DayOfWeek) 6, dates6)
            };
            
            var listStreams1 = new List<StudyStream>()
            {
                new StudyStream("Stream1", streamTimetable1),
                new StudyStream("Stream2", streamTimetable2)
            };
            
            course1.AddStudyStreams(listStreams1);

            
            _manager.AddCourse(robotics, "2R_History");
            Ognp course2 = robotics.Courses[1];
            Assert.Contains(course2, robotics.Courses);
            
            var days1 = new List<Lesson>() { };
            var days2 = new List<Lesson>()
            {
                new Lesson("History", new TimeSpan(10,0,0), "Iakovlev Alexey", 212),
                new Lesson("History", new TimeSpan(11,40,0), "Iakovlev Alexey", 212)
            };
            var days3 = new List<Lesson>() { };
            var days4 = new List<Lesson>() { };
            var days5 = new List<Lesson>() { };
            var days6 = new List<Lesson>() { };
            
            var streamTimetable3 = new List<StudyDay>()
            {
                new StudyDay((DayOfWeek) 1, days1),
                new StudyDay((DayOfWeek) 2, days2),
                new StudyDay((DayOfWeek) 3, days3),
                new StudyDay((DayOfWeek) 4, days4),
                new StudyDay((DayOfWeek) 5, days5),
                new StudyDay((DayOfWeek) 6, days6)
            };
            
            var listStreams2 = new List<StudyStream>()
            {
                new StudyStream("Stream3", streamTimetable3)
            };
            
            course2.AddStudyStreams(listStreams2);
            
            _manager.AddCourse(programming, "2P_History");
            _manager.AddCourse(programming, "2P_MPL");

            var day1 = new List<Lesson>()
            {
                new Lesson("Math", new TimeSpan(10,0,0), "Vozianova Anna", 403),
                new Lesson("OOP", new TimeSpan(11,40,0),"Taylor Swift", 331)
            };
            var day2 = new List<Lesson>()
            {
                new Lesson("English", new TimeSpan(10,0,0), "Hovancheva Stashia", 403),
                new Lesson("Theory of probability", new TimeSpan(11,40,0), "Suslina", 427),
                new Lesson("OS", new TimeSpan(13,30,0), "Kazah", 466)
            };
            var day3 = new List<Lesson>() { };
            var day4 = new List<Lesson>()
            {
                new Lesson("Physics", new TimeSpan(8,20,0), "Egorov", 539),
                new Lesson("OOP", new TimeSpan(10,0,0), "Zinchik", 550)
            };
            var day5 = new List<Lesson>() { };
            var day6 = new List<Lesson>() { };
            
            var groupTimetable1 = new List<StudyDay>()
            {
                new StudyDay((DayOfWeek) 1, day1),
                new StudyDay((DayOfWeek) 2, day2),
                new StudyDay((DayOfWeek) 3, day3),
                new StudyDay((DayOfWeek) 4, day4),
                new StudyDay((DayOfWeek) 5, day5),
                new StudyDay((DayOfWeek) 6, day6)
            };
            
            _manager.AddGroup(programming, "P3204", groupTimetable1);
            UpdatedGroup group1 = programming.Groups[0];

            var students1 = new List<UpdatedStudent>()
            {
                new UpdatedStudent(group1, "Lexa"),
                new UpdatedStudent(group1, "Danya"),
                new UpdatedStudent(group1, "Serg"),
                new UpdatedStudent(group1, "Denis")
            };

            _manager.AddStudent(students1, group1);

            // foreach (UpdatedStudent student in students1)
            // {
            //     group1.Students.Add(student);
            // }
            
            _manager.StudentSignUp(students1[0], course1);
            _manager.StudentSignUp(students1[0], course2);
            
            _manager.StudentSignUp(students1[1], course1);
            
            _manager.StudentSignUp(students1[2], course2);
            
            Assert.AreEqual(1, students1[0].Counter);
            Assert.AreEqual(1, students1[1].Counter);
            Assert.AreEqual(0, students1[2].Counter);
            
            _manager.RemovingRecord(students1[0], course1);

            Assert.AreEqual(0, students1[0].Counter);

            Assert.AreEqual(3,_manager.SearchUnregisteredStudents(group1).Count);
            
            Assert.AreEqual(course1.StudyStreams, _manager.FindingStreams(course1));
            
            Assert.AreEqual(1, _manager.FindingStudents(listStreams1[0]).Count);
        }
    }
}