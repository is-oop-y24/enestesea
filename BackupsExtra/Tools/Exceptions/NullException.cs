using System;

namespace BackupsExtra.Tools.Exceptions
{
    public class NullException : BackupsExtraException
    {
        public NullException()
        {
        }

        public NullException(string message)
            : base(message)
        {
        }

        public NullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}