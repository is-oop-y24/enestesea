using System;

namespace IsuExtra.Tools
{
    public class StudentAlreadyHave2CoursesException : IsuExtraException
    {
        public StudentAlreadyHave2CoursesException()
        {
        }

        public StudentAlreadyHave2CoursesException(string message)
            : base(message)
        {
        }

        public StudentAlreadyHave2CoursesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}