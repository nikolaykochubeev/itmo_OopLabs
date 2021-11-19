using System.Collections.Generic;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public void Run(string folderPath, IEnumerable<Storage> storages)
        {
        }
    }
}