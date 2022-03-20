using System;

namespace Backups.Tools
{
    public class NullException : BackupsException
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