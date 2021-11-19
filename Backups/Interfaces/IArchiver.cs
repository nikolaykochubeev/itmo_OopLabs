using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects);
    }
}