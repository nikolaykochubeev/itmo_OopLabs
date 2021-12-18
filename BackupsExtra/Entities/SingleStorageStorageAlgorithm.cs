using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class SingleStorageStorageAlgorithm : IStorageAlgorithm
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IArchiverType archiverType, IEnumerable<JobObject> jobObjects)
        {
            var archivedObjects = jobObjects.Select(jobObject => new BackupsExtra.Entities.ArchivedObject(jobObject)).ToList();
            var storages = new List<Storage> { new (Guid.NewGuid(), directoryPath, archivedObjects.ToArray()) };
            repository.Store(directoryPath, storages, archiverType);
            return storages;
        }
    }
}