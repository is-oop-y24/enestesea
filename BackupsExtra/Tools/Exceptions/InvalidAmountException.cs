using System;

namespace BackupsExtra.Tools.Exceptions
{
    public class InvalidAmountException : BackupsExtraException
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
