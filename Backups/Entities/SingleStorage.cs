using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class SingleStorage : IArchiver
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects)
        {
            var archivedObjects = (from jobObject in jobObjects
                select new ArchivedObject(jobObject)).ToList();
            var storages = new List<Storage> { new (Guid.NewGuid(), directoryPath, archivedObjects.ToArray()) };
            repository.Run(directoryPath, storages);
            return storages;
        }
    }
}