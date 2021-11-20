using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class SingleStorageArchiver : IArchiver
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects)
        {
            var archivedObjects = jobObjects.Select(jobObject => new ArchivedObject(jobObject)).ToList();
            var storages = new List<Storage> { new (Guid.NewGuid(), directoryPath, archivedObjects.ToArray()) };
            repository.Store(directoryPath, storages);
            return storages;
        }
    }
}