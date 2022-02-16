using System;

namespace Isu.Tools
{
    public class InvalidNameOfGroupException : IsuException
    {
        public InvalidNameOfGroupException()
        {
        }

        public InvalidNameOfGroupException(string message)
            : base(message)
        {
        }

        public InvalidNameOfGroupException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}