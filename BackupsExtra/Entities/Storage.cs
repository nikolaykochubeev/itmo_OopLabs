using System;
using System.Collections.Generic;
using System.IO;

namespace BackupsExtra.Entities
{
    public class Storage
    {
        private readonly List<ArchivedObject> _archivedObjects = new ();

        public Storage(Guid id, string archivePath, params ArchivedObject[] archivedObjects)
        {
            Id = id;
            ArchivePath = archivePath;
            _archivedObjects.AddRange(archivedObjects);
        }

        public string ArchivePath { get; }

        public Guid Id { get; }

        public IReadOnlyList<ArchivedObject> ArchivedObjects => _archivedObjects;

        public void DeleteArchive()
        {
            if (File.Exists(ArchivePath))
                File.Delete(ArchivePath);
        }
    }
}