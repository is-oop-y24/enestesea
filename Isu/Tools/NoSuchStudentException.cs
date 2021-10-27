using System;

namespace Isu.Tools
{
    public class NoSuchStudentException : IsuException
    {
        public NoSuchStudentException()
        {
        }

        public NoSuchStudentException(string message)
            : base(message)
        {
        }

        public NoSuchStudentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}