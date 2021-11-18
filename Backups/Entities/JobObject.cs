using System.IO;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string path)
        {
            if (!File.Exists(path))
                throw new BackupsException($"File {path} doesn't exists");
            Path = path;
        }

        public string Name { get; }
        public string Path { get; }

        public override string ToString()
        {
            return Path.Split("\\").Last();
        }

        protected bool Equals(JobObject other)
        {
            return Path == other.Path;
        }
    }
}