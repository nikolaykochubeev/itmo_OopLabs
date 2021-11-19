using System;
using System.Linq;

namespace Backups.Entities
{
    public class ArchivedObject
    {
        public ArchivedObject(JobObject jobObject)
        {
            Id = jobObject.Id;
            Path = jobObject.Path;
        }

        public Guid Id { get; }
        public string Path { get; }
        public override string ToString()
        {
            return Path.Split("\\").Last();
        }

        protected bool Equals(JobObject other)
        {
            return Path == other.Path;
        }

        protected bool Equals(ArchivedObject other)
        {
            return Path == other.Path;
        }
    }
}