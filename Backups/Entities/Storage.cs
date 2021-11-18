using System;
using System.Collections.Generic;

namespace Backups.Entities
{
    public class Storage
    {
        private readonly List<ArchivedObject> _archivedObjects = new ();
        private string _archivePath;

        public Storage(Guid id, string archivePath, params ArchivedObject[] archivedObjects)
        {
            Id = id;
            _archivePath = archivePath;
            _archivedObjects.AddRange(archivedObjects);
        }

        public Guid Id { get; }
        public IReadOnlyList<ArchivedObject> ArchivedObjects => _archivedObjects;
    }
}