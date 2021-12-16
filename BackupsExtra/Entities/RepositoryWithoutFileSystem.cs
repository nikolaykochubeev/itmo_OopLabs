using System.Collections.Generic;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public void Store(string folderPath, IEnumerable<Storage> storages)
        {
        }

        public void Restore(RestorePoint restorePoint)
        {
        }
    }
}