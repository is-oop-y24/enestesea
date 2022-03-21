using System;
namespace BackupsExtra.Tools.Exceptions
{
    public class AlreadyContainsException : BackupsExtraException
    {
        public AlreadyContainsException()
        {
        }

        public AlreadyContainsException(string message)
            : base(message)
        {
        }

        public AlreadyContainsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
