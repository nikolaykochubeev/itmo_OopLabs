using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Entities
{
    public class SplitStoragesArchiver : IArchiver
    {
        public List<Storage> Store(string directoryPath, params JobObject[] jobObjects)
        {
            return (from jobObject in jobObjects
                let guid = Guid.NewGuid()
                let archivedPath = directoryPath + "/"
                let zipArchive = ZipFile.Open(archivedPath + "/" + guid, ZipArchiveMode.Update).CreateEntry(archivedPath + guid)
                select new Storage(guid, archivedPath, new ArchivedObject(jobObject))).ToList();
        }
    }
}