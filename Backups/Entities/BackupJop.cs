using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Entities
{
    public class BackupJop
    {
        private readonly List<File> _files;
        private readonly List<RestorePoint> _restorePoints;
        private readonly string _storageMethod;

        public BackupJop(string storageMethod, List<File> files = null)
        {
            _storageMethod = storageMethod;
            _files = files ?? new List<File>();
            _restorePoints = new List<RestorePoint>();
        }

        public RestorePoint CreateRestorePoint()
        {
            _restorePoints.Add(new RestorePoint(DateTime.Now, _storageMethod));
            return _restorePoints.Last();
        }
    }
}