using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Services;

namespace Backups.Entities
{
    public class RepositoryWithFileSystem : IRepository
    {
        private readonly List<Storage> _storages = new ();

        public IArchiver Archiver { get; private set; }
        public string DirectoryPath { get; private set; }

        public IReadOnlyList<Storage> Storages => _storages;

        public List<Storage> AddStorages(string directoryPath, IArchiver archiver, params JobObject[] jobObjects)
        {
            DirectoryPath = directoryPath;
            Archiver = archiver;
            List<Storage> storages =
                Archiver.Store(Directory.CreateDirectory(DirectoryPath + "\\" + DateTime.Now).FullName, jobObjects);
            _storages.AddRange(storages);
            return storages;
        }
    }
}