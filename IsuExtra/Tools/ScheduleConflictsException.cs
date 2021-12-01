using System;

namespace IsuExtra.Tools
{
    public class ScheduleConflictsException : IsuExtraException
    {
        public ScheduleConflictsException()
        {
        }

        public ScheduleConflictsException(string message)
            : base(message)
        {
        }

        public ScheduleConflictsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}