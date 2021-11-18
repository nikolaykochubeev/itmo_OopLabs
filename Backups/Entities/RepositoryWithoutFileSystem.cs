using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public List<Storage> AddStorages(string directoryPath, IArchiver archiver, params JobObject[] jobObjects)
        {
            return null;
        }
    }
}