using System;
using System.Collections.Generic;
using BackupsExtra.Tools;

namespace BackupsExtra.Entities
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        public RestorePoint(int amount, List<Storage> storages, DateTime creationTime)
        {
            Number = amount;
            _storages = storages;
            CreationTime = creationTime;
        }

        public IReadOnlyList<Storage> Storages => _storages;
        public int Number { get; }
        public DateTime CreationTime { get; }

        public void AddStorage(Storage storage)
        {
            if (storage is null)
            {
                throw new BackupsExtraException("storage can not be the null");
            }

            _storages.Add(storage);
        }
    }
}