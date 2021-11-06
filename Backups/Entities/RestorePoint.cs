using System;
using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private DateTime _creationTime;
        private string _storageMethod;
        private List<File> _backups = new ();

        public RestorePoint(DateTime creationTime, string storageMethod)
        {
            _creationTime = creationTime;
            _storageMethod = storageMethod;
        }
    }
}