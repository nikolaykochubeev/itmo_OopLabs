using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;

        private readonly List<RestorePoint> _restorePoints = new ();

        private string _directoryPath;
        private IArchiver _archiver;
        private IRepository _repositoryType;

        public BackupJob(IRepository repositoryType, IArchiver archiver, IEnumerable<JobObject> jobObjects, string directoryPath)
        {
            Id = Guid.NewGuid();

            _repositoryType = repositoryType ?? throw new BackupsException("Any of the constructor arguments are null");
            _archiver = archiver ?? throw new BackupsException("Any of the constructor arguments are null");
            _jobObjects = jobObjects.ToList();
            if (directoryPath == string.Empty)
                throw new BackupsException("String cannot be empty");
            _directoryPath = directoryPath;
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
            foreach (JobObject jobObject in jobObjects)
            {
                AddObject(jobObject);
            }
        }

        public void AddObject(JobObject jobObject)
        {
            if (_jobObjects.FirstOrDefault(job => job.Id == jobObject.Id) is not null)
                return;
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