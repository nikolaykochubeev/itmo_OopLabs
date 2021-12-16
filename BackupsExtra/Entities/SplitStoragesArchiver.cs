using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class SplitStoragesArchiver : IArchiver
    {
        public bool IsMergeble { get; } = true;

        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<BackupsExtra.Entities.JobObject> jobObjects)
        {
            var storages = jobObjects.Select(jobObject =>
                new Storage(Guid.NewGuid(), directoryPath, new BackupsExtra.Entities.ArchivedObject(jobObject))).ToList();
            repository.Store(directoryPath, storages);
            return storages;
        }
    }
}