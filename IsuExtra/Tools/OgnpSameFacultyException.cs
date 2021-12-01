using System;

namespace IsuExtra.Tools
{
    public class OgnpSameFacultyException : IsuExtraException
    {
        public OgnpSameFacultyException()
        {
        }

        public OgnpSameFacultyException(string message)
            : base(message)
        {
        }

        public OgnpSameFacultyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}