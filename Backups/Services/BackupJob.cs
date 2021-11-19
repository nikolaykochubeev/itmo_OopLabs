using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects = new ();

        private readonly List<RestorePoint> _restorePoints = new ();

        private string _directoryPath;
        private IArchiver _archiver;
        private IRepository _repositoryType;

        public BackupJob(IRepository repositoryType, IArchiver archiver, IEnumerable<JobObject> jobObjects, string directoryPath)
        {
            Id = Guid.NewGuid();
            _archiver = archiver;
            _jobObjects.AddRange(jobObjects);
            _directoryPath = directoryPath;
            _repositoryType = repositoryType;
        }

        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
        public Guid Id { get; }

        public void CreateRestorePoint()
        {
            List<Storage> storages = _archiver.Run(_directoryPath, _repositoryType, _jobObjects);
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, storages, DateTime.Now));
        }

        public void AddObjects(IEnumerable<JobObject> jobObjects)
        {
            _jobObjects.AddRange(jobObjects);
        }

        public void AddObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(Guid jobObjectId)
        {
            JobObject jobObject = _jobObjects.FirstOrDefault(job => job.Id == jobObjectId);
            if (jobObject is not null)
                _jobObjects.Remove(jobObject);
        }
    }
}