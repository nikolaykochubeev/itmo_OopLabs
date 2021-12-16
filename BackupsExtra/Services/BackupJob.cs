using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        private readonly string _directoryPath;
        private readonly IArchiver _archiver;
        private readonly IRepository _repositoryType;
        private readonly ICleaningPointsAlgorithm _cleaningPointsAlgorithm;
        private readonly ILogger _logger;
        public BackupJob(IRepository repositoryType, IArchiver archiver, ICleaningPointsAlgorithm cleaningPointsAlgorithm, IEnumerable<JobObject> jobObjects, string directoryPath, ILogger logger)
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
            _logger = logger;
            _logger.Log("BackupJob successfully added", true);
        }

        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
        public Guid Id { get; }

        public void CreateRestorePoint()
        {
            List<Storage> storages = _archiver.Run(_directoryPath, _repositoryType, _jobObjects);
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, storages, DateTime.Now));
            _logger.Log($"Restore point number {_restorePoints.Count} successfully created", true);
            CleanRestorePoints();
        }

        public void AddObjects(List<JobObject> jobObjects)
        {
            foreach (JobObject jobObject in jobObjects)
            {
                AddObject(jobObject);
            }

            _logger.Log($"{jobObjects.Count()} jobObjects successfully added", true);
        }

        public void AddObject(JobObject jobObject)
        {
            if (_jobObjects.FirstOrDefault(job => job.Id == jobObject.Id) is not null)
            {
                return;
            }

            _jobObjects.Add(jobObject);
            _logger.Log("jobObject successfully added", true);
        }

        public void RemoveJobObject(Guid jobObjectId)
        {
            JobObject jobObject = _jobObjects.FirstOrDefault(job => job.Id == jobObjectId);
            if (jobObject is not null)
            {
                _jobObjects.Remove(jobObject);
            }

            _logger.Log("jobObject successfully removed", true);
        }

        public void Restore(int restorePointNumber, string path = null)
        {
            RestorePoint restorePoint = _restorePoints.FirstOrDefault(point => point.Number == restorePointNumber);
            if (restorePoint == null)
            {
                _logger.Log("restorePoint with this number doesnt exists");
                throw new BackupsExtraException("restorePoint with this number doesnt exists");
            }

            _repositoryType.Restore(restorePoint);
        }

        private void CleanRestorePoints()
        {
            _cleaningPointsAlgorithm.Clean(_restorePoints, _archiver.IsMergeble);
            _logger.Log("RestorePoints successfully clean", true);
        }
    }
}