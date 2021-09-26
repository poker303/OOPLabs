using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        public CourseNumber(int courseNum)
        {
            if ((courseNum < 1) || (courseNum > 4))
            {
                throw new InvalidCourseNumberException();
            }

            this.CourseNum = courseNum;
        }

        private int CourseNum { get; }
    }
}