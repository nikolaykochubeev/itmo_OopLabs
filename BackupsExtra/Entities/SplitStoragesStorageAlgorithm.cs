using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class SplitStoragesStorageAlgorithm : IStorageAlgorithm
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IArchiverType archiverType, IEnumerable<BackupsExtra.Entities.JobObject> jobObjects)
        {
            var storages = jobObjects.Select(jobObject =>
                new Storage(Guid.NewGuid(), directoryPath, new BackupsExtra.Entities.ArchivedObject(jobObject))).ToList();
            repository.Store(directoryPath, storages, archiverType);
            return storages;
        }
    }
}