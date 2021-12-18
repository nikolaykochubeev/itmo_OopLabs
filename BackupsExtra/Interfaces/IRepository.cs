using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IRepository
    {
        void Store(string folderPath, IEnumerable<Storage> storages, IArchiverType archiverType);
    }
}