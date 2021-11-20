using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public void Store(string folderPath, IEnumerable<Storage> storages)
        {
        }
    }
}