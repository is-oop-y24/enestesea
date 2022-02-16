using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Classes
{
    [Serializable]
    public class BackupsJob
    {
        private readonly List<RestorePoint> _restorePoints;
        private readonly List<IRepository> _repositories;
        private readonly ICleaner _cleanAlgorithm;
        private IWayOfStorage _wayOfStorage;
        private List<ILogger> _loggers;
        private bool _mergeCheck;
        private List<JobObject> _jobObjects;
        public BackupsJob(List<IRepository> repositories, IWayOfStorage wayOfStorage, ICleaner cleanAlgorithm, List<ILogger> loggers, bool mergeCheck)
        {
             if (repositories.Count == 0) throw new InvalidAmountException("Amount of repositories is 0");
             if (loggers == null) throw new NullException("No loggers");
             _repositories = repositories;
             _jobObjects = new List<JobObject>();
             _wayOfStorage = wayOfStorage;
             _restorePoints = new List<RestorePoint>();
             _cleanAlgorithm = cleanAlgorithm;
             _loggers = loggers;
             _mergeCheck = mergeCheck;
        }

        public JobObject AddJobObject(string path)
        {
            if (path == null)
            {
                throw new NullException("Path is invalid");
            }

            JobObject jobObject = new JobObject(path);
            if (_jobObjects.Contains(jobObject)) throw new AlreadyContainsException("Already contains this");
            _jobObjects.Add(jobObject);
            foreach (ILogger logger in _loggers)
            {
                logger.Log($"{jobObject} was added");
            }

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
            foreach (ILogger logger in _loggers)
            {
                logger.Log($"{jobObject} was removed");
            }
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (jobObject == null)
            {
                throw new NullException("jobObject is invalid");
            }

            _jobObjects.Remove(jobObject);
            foreach (ILogger logger in _loggers)
            {
                logger.Log($"{jobObject} was removed");
            }
        }

        public RestorePoint CreateRestorePoint()
        {
            SplitStorage split = new SplitStorage();
            List<Storage> storages = new List<Storage>();
            RestorePoint restorePoint =
                new RestorePoint(DateTime.Now, _restorePoints.Count, split, storages);
            foreach (IRepository repository in _repositories)
            {
                storages = repository.SaveRestorePoint(_jobObjects, _wayOfStorage);
            }

            restorePoint.ListStorages.AddRange(storages);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void AddLogger(ILogger logger)
        {
            if (logger == null)
            {
                throw new NullException("Logger is invalid");
            }

            _loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            if (logger == null)
            {
                throw new NullException("Logger is invalid");
            }

            _loggers.Remove(logger);
        }

        public void RestoreDefault(RestorePoint restorePoint)
        {
            foreach (IRepository repository in _repositories)
            {
                repository.Restore(restorePoint);
            }

            _jobObjects = restorePoint.ListStorages.SelectMany(storage => storage.JobObjects).ToList();
        }

        public void RestoreToSelectedDirectory(RestorePoint restorePoint, string path)
        {
            foreach (IRepository repository in _repositories)
            {
                repository.Restore(restorePoint, path);
            }
        }

        public void DeleteRestorePoint(RestorePoint restorePoint)
        {
            foreach (IRepository repository in _repositories)
            {
                repository.DeletePoint(restorePoint);
            }

            _restorePoints.Remove(restorePoint);
        }

        public RestorePoint Merge(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            if (restorePoint1.WayOfStorage.GetType() == typeof(SingleStorage) || restorePoint2.WayOfStorage.GetType() == typeof(SingleStorage))
            {
                DeleteRestorePoint(restorePoint1);
                return restorePoint2;
            }

            List<JobObject> list = restorePoint2.ListStorages.SelectMany(storage => storage.JobObjects).ToList();
            List<JobObject> tempList = new List<JobObject>(list);
            foreach (JobObject jobObject in restorePoint1.ListStorages.SelectMany(storage => storage.JobObjects.Where(jobObject => !tempList.Contains(jobObject))))
            {
                list.Add(jobObject);
            }

            List<Storage> storages = restorePoint2.WayOfStorage.CreateStorage(list, _restorePoints.Count.ToString());
            RestorePoint restorePoint =
                new RestorePoint(DateTime.Now, _restorePoints.Count, restorePoint2.WayOfStorage, storages);
            foreach (IRepository repository in _repositories)
            {
                repository.MergePoints(restorePoint1, restorePoint2, restorePoint);
            }

            DeleteRestorePoint(restorePoint1);
            DeleteRestorePoint(restorePoint2);
            return restorePoint;
        }
    }
}