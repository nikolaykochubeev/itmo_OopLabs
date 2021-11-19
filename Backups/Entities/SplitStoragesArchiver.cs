using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class SplitStoragesArchiver : IArchiver
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects)
        {
            var storages = jobObjects.Select(jobObject =>
                new Storage(Guid.NewGuid(), directoryPath, new ArchivedObject(jobObject))).ToList();
            repository.Store(directoryPath, storages);
            return storages;
        }
    }
}