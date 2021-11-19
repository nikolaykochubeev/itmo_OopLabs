using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class RepositoryWithFileSystem : IRepository
    {
        public void Store(string folderPath, IEnumerable<Storage> storages)
        {
            if (!Directory.Exists(folderPath))
                throw new BackupsException($"Directory {folderPath} doesn't exists");
            Directory.CreateDirectory(folderPath);
            foreach (Storage storage in storages)
            {
                using ZipArchive zipArchive = ZipFile.Open(Path.Combine(folderPath, storage.Id.ToString()) + ".zip", ZipArchiveMode.Update);
                foreach (ArchivedObject archivedObject in storage.ArchivedObjects)
                {
                    if (!File.Exists(archivedObject.FilePath))
                        throw new BackupsException($"File {archivedObject.FilePath} doesn't exists");
                    if (!Directory.Exists(storage.ArchivePath))
                        throw new BackupsException($"File {storage.ArchivePath} doesn't exists");
                    zipArchive.CreateEntryFromFile(archivedObject.FilePath, archivedObject.ToString());
                }
            }
        }
    }
}