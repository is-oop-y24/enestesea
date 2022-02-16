using System;

namespace Backups.Tools
{
    public class NoSuchFileException : BackupsException
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