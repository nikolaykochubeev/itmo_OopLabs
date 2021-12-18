using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Tools;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class RepositoryWithFileSystem : IRepository
    {
        public void Store(string folderPath, IEnumerable<Storage> storages, IArchiverType archiverType)
        {
            foreach (Storage storage in storages)
            {
                archiverType.Archivate(storage, folderPath);
            }
        }
    }
}