using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;

        private readonly List<RestorePoint> _restorePoints = new ();

        private string _directoryPath;
        private IArchiver _archiverType;
        private IRepository _repositoryType;

        public BackupJob(IRepository repositoryType, IArchiver archiverType, List<JobObject> jobObjects,  string directoryPath = "C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups.Tests\\Directory")
        {
            Id = Guid.NewGuid();
            _archiverType = archiverType;
            _jobObjects = jobObjects;
            _directoryPath = directoryPath;
            _repositoryType = repositoryType;
        }

        public Guid Id { get; }

        public RestorePoint CreateRestorePoint(DateTime dateTime)
        {
            IRepository repository = _repositoryType;
            repository.AddStorages(_directoryPath, _archiverType, _jobObjects.ToArray());
            var restorePoint = new RestorePoint(_restorePoints.Count, repository, DateTime.Now);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public JobObject AddJobObject(string filePath)
        {
            JobObject jobObject = _jobObjects.FirstOrDefault(obj => obj.Path == filePath);
            if (jobObject is not null)
                return jobObject;
            jobObject = new JobObject(filePath);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public void RemoveJobObject(string filePath)
        {
            JobObject jobObject = _jobObjects.FirstOrDefault(job => job.Path == filePath);
            if (jobObject is not null)
                _jobObjects.Remove(jobObject);
        }
    }
}