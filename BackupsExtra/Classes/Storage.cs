using System.Collections.Generic;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Classes
{
    public class Storage
    {
        private readonly string _name;
        private readonly List<JobObject> _jobObjects;

        public Storage(string name)
        {
            if (name == null) throw new NullException("name is empty");
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