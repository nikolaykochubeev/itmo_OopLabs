using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Tools;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class ZipArchiver : IArchiverType
    {
        public void Archivate(Storage storage, string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new BackupsException($"Directory {folderPath} doesn't exists");
            Directory.CreateDirectory(folderPath);
            using ZipArchive zipArchive = ZipFile.Open(
                Path.Combine(folderPath, storage.Id.ToString()) + ".zip",
                ZipArchiveMode.Update);
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
}