using System.Collections.Generic;
using Backups.Tools;

namespace Backups.Classes
{
    public class Storage
    {
        private readonly string _name;
        private readonly List<JobObject> _jobObjects;

        public Storage(string name)
        {
            _name = name;
            _jobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects => _jobObjects;
        public string Name => _name;

        public void AddJobObject(JobObject jobObject)
        {
            if (jobObject == null)
            {
                throw new NullException("jobObject is invalid");
            }

            _jobObjects.Add(jobObject);
        }
    }
}