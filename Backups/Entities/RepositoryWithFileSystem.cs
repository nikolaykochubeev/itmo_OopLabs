using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class RepositoryWithFileSystem : IRepository
    {
        public void Run(string folderPath, IEnumerable<Storage> storages)
        {
            if (!Directory.Exists(folderPath))
                throw new BackupsException($"Directory {folderPath} doesn't exists");
            IEnumerable<Storage> listStorages = storages.ToList();
            Directory.CreateDirectory(folderPath);
            foreach (Storage storage in listStorages.ToList())
            {
                using ZipArchive zipArchive = ZipFile.Open(Path.Combine(folderPath, storage.Id.ToString()) + ".zip", ZipArchiveMode.Update);
                foreach (ArchivedObject archivedObject in storage.ArchivedObjects)
                {
                    if (!File.Exists(archivedObject.Path))
                        throw new BackupsException($"File {archivedObject.Path} doesn't exists");
                    zipArchive.CreateEntryFromFile(archivedObject.Path, archivedObject.ToString());
                }
            }
        }
    }
}