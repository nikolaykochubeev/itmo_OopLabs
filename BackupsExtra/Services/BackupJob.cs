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
        private readonly IStorageAlgorithm _storageAlgorithm;
        private readonly IRepository _repositoryType;
        private readonly ICleaningPointsAlgorithm _cleaningPointsAlgorithm;
        private readonly ILogger _logger;
        private readonly IArchiverType _archiverType;
        public BackupJob(IRepository repositoryType, IStorageAlgorithm storageAlgorithm, ICleaningPointsAlgorithm cleaningPointsAlgorithm, IEnumerable<JobObject> jobObjects, string directoryPath, ILogger logger, IArchiverType archiverType)
        {
            Id = Guid.NewGuid();
            _repositoryType = repositoryType ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _storageAlgorithm = storageAlgorithm ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _cleaningPointsAlgorithm = cleaningPointsAlgorithm ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            _jobObjects = jobObjects.ToList();
            if (directoryPath == string.Empty)
            {
                throw new BackupsExtraException("String cannot be empty");
            }

            _directoryPath = directoryPath;
            _logger = logger;
            _archiverType = archiverType;
            _logger.Log("BackupJob successfully added", true);
        }

        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
        public Guid Id { get; }

        public void CreateRestorePoint()
        {
            List<Storage> storages = _storageAlgorithm.Run(_directoryPath, _repositoryType, _archiverType, _jobObjects);
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, storages, DateTime.Now));
            _logger.Log($"Restore point number {_restorePoints.Count} successfully created", true);
            CleanRestorePoints();
        }

        public void AddObjects(params JobObject[] jobObjects)
        {
            foreach (JobObject jobObject in jobObjects)
            {
                if (_jobObjects.FirstOrDefault(job => job.Id == jobObject.Id) is not null)
                {
                    continue;
                }

                _jobObjects.Add(jobObject);
            }

            _logger.Log($"{jobObjects.Count()} jobObjects successfully added", true);
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

            foreach (Storage storage in restorePoint.Storages)
            {
                using ZipArchive zipArchive = ZipFile.OpenRead(storage.ArchivePath);
                foreach (ArchivedObject archivedObject in storage.ArchivedObjects)
                {
                    string pathForRestore = path ?? archivedObject.FilePath;
                    if (File.Exists(pathForRestore))
                    {
                        File.Delete(pathForRestore);
                    }

                    zipArchive.Entries.FirstOrDefault(x => x.Name == Path.GetFileName(archivedObject.FilePath))
                        ?.ExtractToFile(pathForRestore);
                }
            }
        }

        private void CleanRestorePoints()
        {
            _cleaningPointsAlgorithm.Clean(_restorePoints);
            _logger.Log("RestorePoints successfully clean", true);
        }
    }
}