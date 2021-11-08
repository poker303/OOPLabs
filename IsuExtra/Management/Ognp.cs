using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Management
{
    public class Ognp
    {
        public Ognp(string name)
        {
            if (name[0] - '0' < 1 || name[0] - '0' > 4)
            {
                throw new CreateException("The course can't have such name");

                // throw new IncorrectOrderOfDigitsException();
            }

            if (!char.IsUpper(name[1]))
            {
                throw new CreateException("The course can't have such name");
            }

            Name = name;
            StudyStreams = new List<StudyStream>();
        }

        public string Name { get; set; }
        public List<StudyStream> StudyStreams { get; set; }

        public void AddStudyStreams(List<StudyStream> studyStreams)
        {
            StudyStreams.AddRange(studyStreams.Where(stream => !StudyStreams.Contains(stream)));
        }
    }
}