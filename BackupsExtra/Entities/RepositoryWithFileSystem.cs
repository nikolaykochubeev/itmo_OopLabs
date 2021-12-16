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
        public void Store(string folderPath, IEnumerable<Storage> storages)
        {
            if (!Directory.Exists(folderPath))
                throw new BackupsException($"Directory {folderPath} doesn't exists");
            Directory.CreateDirectory(folderPath);
            foreach (Storage storage in storages)
            {
                using ZipArchive zipArchive = ZipFile.Open(Path.Combine(folderPath, storage.Id.ToString()) + ".zip", ZipArchiveMode.Update);
                foreach (BackupsExtra.Entities.ArchivedObject archivedObject in storage.ArchivedObjects)
                {
                    if (!File.Exists(archivedObject.FilePath))
                        throw new BackupsException($"File {archivedObject.FilePath} doesn't exists");
                    if (!Directory.Exists(storage.ArchivePath))
                        throw new BackupsException($"File {storage.ArchivePath} doesn't exists");
                    zipArchive.CreateEntryFromFile(archivedObject.FilePath, archivedObject.ToString());
                }
            }
        }

        public void Restore(RestorePoint restorePoint, string path)
        {
            if (restorePoint is null)
                throw new BackupsException("restorePoint can not be null");
            foreach (Storage storage in restorePoint.Storages)
            {
                using ZipArchive zipArchive = ZipFile.OpenRead(storage.ArchivePath);
                foreach (ArchivedObject archivedObject in storage.ArchivedObjects)
                {
                    string pathForRestore = path ?? archivedObject.FilePath;
                    if (File.Exists(pathForRestore))
                    {
                        File.Delete(pathForRestore);
                    }

                    zipArchive.Entries.FirstOrDefault(x => x.Name == Path.GetFileName(archivedObject.FilePath))
                        ?.ExtractToFile(pathForRestore);
                }
            }
        }
    }
}