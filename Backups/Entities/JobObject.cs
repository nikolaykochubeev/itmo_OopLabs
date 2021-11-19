using System;
using System.IO;
using System.Linq;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            Id = Guid.NewGuid();
            FilePath = filePath;
        }

        public JobObject(JobObject jobObject)
        {
            Id = jobObject.Id;
            FilePath = jobObject.FilePath;
        }

        public string FilePath { get; }
        public Guid Id { get; }

        public override string ToString()
        {
            return Path.GetFileName(FilePath);
        }

        protected bool Equals(JobObject other)
        {
            return Id == other.Id;
        }
    }
}