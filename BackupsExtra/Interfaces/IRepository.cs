using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IRepository
    {
        public void Store(string folderPath, IEnumerable<Storage> storages);
        public void Restore(RestorePoint restorePoint);
    }
}