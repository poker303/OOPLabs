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
            OGNP course1 = robotics.Courses[0];
            Assert.Contains(course1, robotics.Courses);
            
            var date1 = new List<Lesson>() { };
            var date2 = new List<Lesson>() { };
            var date3 = new List<Lesson>()
            {
                new Lesson("CyberBez", "10:00", 90, "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", "11:40", 90, "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", "13:30", 90, "Gavrichenko Alexander", 306)
            };
            var date4 = new List<Lesson>() { };
            var date5 = new List<Lesson>() { };
            var date6 = new List<Lesson>() { };
            
            var streamTimetable1 = new List<StudyDay>()
            {
                new StudyDay("Monday", date1),
                new StudyDay("Tuesday", date2),
                new StudyDay("Wednesday", date3),
                new StudyDay("Thursday", date4),
                new StudyDay("Friday", date5),
                new StudyDay("Saturday", date6),
            };
            
            var dates1 = new List<Lesson>() { };
            var dates2 = new List<Lesson>() { };
            var dates3 = new List<Lesson>(){ };
            var dates4 = new List<Lesson>() { };
            var dates5 = new List<Lesson>() { };
            var dates6 = new List<Lesson>()
            {
                new Lesson("CyberBez", "10:00", 90, "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", "11:40", 90, "Gavrichenko Alexander", 212),
                new Lesson("CyberBez", "13:30", 90, "Gavrichenko Alexander", 306)
            };
            
            var streamTimetable2 = new List<StudyDay>()
            {
                new StudyDay("Monday", dates1),
                new StudyDay("Tuesday", dates2),
                new StudyDay("Wednesday", dates3),
                new StudyDay("Thursday", dates4),
                new StudyDay("Friday", dates5),
                new StudyDay("Saturday", dates6),
            };
            
            var listStreams1 = new List<StudyStream>()
            {
                new StudyStream("Stream1", streamTimetable1),
                new StudyStream("Stream2", streamTimetable2)
            };
            
            course1.AddStudyStreams(listStreams1);

            
            _manager.AddCourse(robotics, "2R_History");
            OGNP course2 = robotics.Courses[1];
            Assert.Contains(course2, robotics.Courses);
            
            var days1 = new List<Lesson>() { };
            var days2 = new List<Lesson>()
            {
                new Lesson("History", "10:00", 90, "Iakovlev Alexey", 212),
                new Lesson("History", "11:40", 90, "Iakovlev Alexey", 212)
            };
            var days3 = new List<Lesson>() { };
            var days4 = new List<Lesson>() { };
            var days5 = new List<Lesson>() { };
            var days6 = new List<Lesson>() { };
            
            var streamTimetable3 = new List<StudyDay>()
            {
                new StudyDay("Monday", days1),
                new StudyDay("Tuesday", days2),
                new StudyDay("Wednesday", days3),
                new StudyDay("Thursday", days4),
                new StudyDay("Friday", days5),
                new StudyDay("Saturday", days6),
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
                new Lesson("Math", "10:00", 90, "Vozianova Anna", 403),
                new Lesson("OOP", "11:40", 90, "Taylor Swift", 331)
            };
            var day2 = new List<Lesson>()
            {
                new Lesson("English", "10:00", 90, "Hovancheva Stashia", 403),
                new Lesson("Theory of probability", "11:40", 90, "Suslina", 427),
                new Lesson("OS", "13:30", 90, "Kazah", 466)
            };
            var day3 = new List<Lesson>() { };
            var day4 = new List<Lesson>()
            {
                new Lesson("Physics", "8:20", 90, "Egorov", 539),
                new Lesson("OOP", "10:00", 90, "Zinchik", 550)
            };
            var day5 = new List<Lesson>() { };
            var day6 = new List<Lesson>() { };
            
            var groupTimetable1 = new List<StudyDay>()
            {
                new StudyDay("Monday", day1),
                new StudyDay("Tuesday", day2),
                new StudyDay("Wednesday", day3),
                new StudyDay("Thursday", day4),
                new StudyDay("Friday", day5),
                new StudyDay("Saturday", day6),
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
            
            Assert.AreEqual(2, _manager.FindingStudents(listStreams1[0]));
        }
    }
}