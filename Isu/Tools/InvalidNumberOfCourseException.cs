using System;

namespace Isu.Tools
{
    public class InvalidNumberOfCourseException : IsuException
    {
        public InvalidNumberOfCourseException()
        {
        }

        public InvalidNumberOfCourseException(string message)
            : base(message)
        {
        }

        public InvalidNumberOfCourseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}