using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Interfaces
{
    public interface IRepository
    {
        public void Run(string folderPath, IEnumerable<Storage> storages);
    }
}