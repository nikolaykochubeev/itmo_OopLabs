using System;
using System.IO;
using BackupsExtra.Entities;

namespace BackupsExtra.Entities
{
    public class ArchivedObject
    {
        public ArchivedObject(JobObject jobObject)
        {
            Id = jobObject.Id;
            FilePath = jobObject.FilePath;
        }

        public Guid Id { get; }
        public string FilePath { get; }

        public override string ToString()
        {
            return Path.GetFileName(FilePath);
        }

        protected bool Equals(ArchivedObject other)
        {
            return FilePath == other.FilePath;
        }
    }
}