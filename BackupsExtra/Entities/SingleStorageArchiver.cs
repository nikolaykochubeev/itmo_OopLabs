using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class SingleStorageArchiver : IArchiver
    {
        public bool IsMergeble { get; } = false;

        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<BackupsExtra.Entities.JobObject> jobObjects)
        {
            var archivedObjects = jobObjects.Select(jobObject => new BackupsExtra.Entities.ArchivedObject(jobObject)).ToList();
            var storages = new List<Storage> { new (Guid.NewGuid(), directoryPath, archivedObjects.ToArray()) };
            repository.Store(directoryPath, storages);
            return storages;
        }
    }
}