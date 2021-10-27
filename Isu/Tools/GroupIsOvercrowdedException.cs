using System;

namespace Isu.Tools
{
    public class GroupIsOvercrowdedException : IsuException
    {
        public GroupIsOvercrowdedException()
        {
        }

        public GroupIsOvercrowdedException(string message)
            : base(message)
        {
        }

        public GroupIsOvercrowdedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}