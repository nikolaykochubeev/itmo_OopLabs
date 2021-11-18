using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Interfaces
{
    public interface IRepository
    {
        public List<Storage> AddStorages(string directoryPath, IArchiver archiver, params JobObject[] jobObjects);
    }
}