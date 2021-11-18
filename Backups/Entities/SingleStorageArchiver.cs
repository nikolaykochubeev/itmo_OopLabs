using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Entities
{
    public class SingleStorageArchiver : IArchiver
    {
        public List<Storage> Store(string directoryPath, params JobObject[] jobObjects)
        {
            var guid = Guid.NewGuid();
            ZipArchive zipArchive = ZipFile.Open(directoryPath, ZipArchiveMode.Update);
            Console.WriteLine(guid);
            return (from jobObject in jobObjects
                let entry = zipArchive.CreateEntryFromFile(jobObject.Path, jobObject.ToString())
                select new Storage(guid, directoryPath, new ArchivedObject(jobObject))).ToList();
        }
    }
}