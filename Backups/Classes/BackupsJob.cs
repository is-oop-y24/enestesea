using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Classes
{
    public class BackupsJob
    {
        private readonly List<JobObject> _jobObjects;
        private readonly List<RestorePoint> _restorePoints;
        private readonly List<IRepository> _repositories;
        private readonly IWayOfStorage _wayOfStorage;

        public BackupsJob(List<IRepository> repositories, IWayOfStorage wayOfStorage)
        {
            if (repositories.Count == 0) throw new InvalidAmountException("Amount of repositories is 0");
            _repositories = repositories;
            _jobObjects = new List<JobObject>();
            _wayOfStorage = wayOfStorage;
            _restorePoints = new List<RestorePoint>();
        }

        public int JobObjectsNumber => _jobObjects.Count;
        public int RestorePointsNumber => _restorePoints.Count;

        public JobObject AddJobObject(string path)
        {
            if (path == null)
            {
                throw new NullException("Path is invalid");
            }

            JobObject jobObject = new JobObject(path);
            if (_jobObjects.Contains(jobObject)) throw new AlreadyContainsException("Already contains this");
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public void DeleteJobObjectByPath(string path)
        {
            if (path == null)
            {
                throw new NullException("Path is invalid");
            }

            JobObject jobObject = _jobObjects.FirstOrDefault(jobObject => jobObject.FileInfo.FullName == path);
            _jobObjects.Remove(jobObject);
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (jobObject == null)
            {
                throw new NullException("jobObject is invalid");
            }

            _jobObjects.Remove(jobObject);
        }

        public RestorePoint CreateRestorePoint()
        {
            RestorePoint restorePoint = new RestorePoint(DateTime.Now);
            List<Storage> storages = new List<Storage>();
            foreach (IRepository repository in _repositories)
            {
                storages = repository.SaveRestorePoint(_jobObjects, _wayOfStorage);
            }

            restorePoint.ListStorages.AddRange(storages);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }
    }
}