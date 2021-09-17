using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        public CourseNumber(int courseNum)
        {
            if ((courseNum >= 1) && (courseNum <= 4))
            {
                this.CourseNum = courseNum;
            }
            else
            {
                throw new InvalidCourseNumberException();
            }
        }

        private int CourseNum { get; }
    }
}