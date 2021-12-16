using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra.Services
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;
        private readonly List<RestorePoint> _restorePoints = new ();

        private string _directoryPath;
        private IArchiver _archiver;
        private IRepository _repositoryType;
        private ICleaningPointsAlgorithm _cleaningPointsAlgorithm;
        public BackupJob(IRepository repositoryType, IArchiver archiver, ICleaningPointsAlgorithm cleaningPointsAlgorithm, IEnumerable<JobObject> jobObjects, string directoryPath)
        {
            Id = Guid.NewGuid();
            _repositoryType = repositoryType ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _archiver = archiver ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _cleaningPointsAlgorithm = cleaningPointsAlgorithm ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _jobObjects = jobObjects.ToList();
            if (directoryPath == string.Empty)
            {
                throw new BackupsExtraException("String cannot be empty");
            }

            _directoryPath = directoryPath;
        }

        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
        public Guid Id { get; }

        public void CreateRestorePoint()
        {
            List<Storage> storages = _archiver.Run(_directoryPath, _repositoryType, _jobObjects);
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, storages, DateTime.Now));
            CleanRestorePoints();
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
            {
                return;
            }

            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(Guid jobObjectId)
        {
            JobObject jobObject = _jobObjects.FirstOrDefault(job => job.Id == jobObjectId);
            if (jobObject is not null)
            {
                _jobObjects.Remove(jobObject);
            }
        }

        private void CleanRestorePoints()
        {
            _cleaningPointsAlgorithm.Clean(_restorePoints, _archiver.IsMergeble);
        }
    }
}