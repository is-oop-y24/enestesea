using System;

namespace Backups.Tools
{
    public class InvalidAmountException : BackupsException
    {
        public InvalidAmountException()
        {
        }

        public InvalidAmountException(string message)
            : base(message)
        {
        }

        public InvalidAmountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
