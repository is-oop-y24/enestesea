using System;

namespace BackupsExtra.Tools.Exceptions
{
    public class NoSuchFileException : BackupsExtraException
    {
        public NoSuchFileException()
        {
        }

        public NoSuchFileException(string message)
            : base(message)
        {
        }

        public NoSuchFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}