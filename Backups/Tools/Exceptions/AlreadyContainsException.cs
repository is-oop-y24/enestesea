using System;

namespace Backups.Tools
{
    public class AlreadyContainsException : BackupsException
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
