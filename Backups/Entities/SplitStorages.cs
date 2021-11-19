using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class SplitStorages : IArchiver
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects)
        {
            var storages = (from jobObject in jobObjects
                select new Storage(Guid.NewGuid(), directoryPath, new ArchivedObject(jobObject))).ToList();
            repository.Run(directoryPath, storages);
            return storages;
        }
    }
}