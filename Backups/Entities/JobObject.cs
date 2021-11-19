using System;
using System.Linq;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string path)
        {
            Path = path;
            Id = Guid.NewGuid();
        }

        public string Path { get; }
        public Guid Id { get; }

        public override string ToString()
        {
            return Path.Split("\\").Last();
        }

        protected bool Equals(JobObject other)
        {
            return Id == other.Id;
        }
    }
}