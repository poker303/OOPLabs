using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Management
{
    public interface IManager
    {
        void AddStudent(List<UpdatedStudent> students, UpdatedGroup group);
        void AddGroup(MegaFaculty faculty, string groupName, List<StudyDay> timetable);
        void AddCourse(MegaFaculty faculty, string courseName);
        List<StudyStream> FindingStreams(OGNP course);
        List<UpdatedStudent> FindingStudents(StudyStream stream);
        void StudentSignUp(UpdatedStudent student, OGNP ognp);
        void RemovingRecord(UpdatedStudent student, OGNP ognp);
        List<UpdatedStudent> SearchUnregisteredStudents(UpdatedGroup group);
    }
}