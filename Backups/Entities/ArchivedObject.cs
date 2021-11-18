namespace Backups.Entities
{
    public class ArchivedObject
    {
        public ArchivedObject(JobObject jobObject)
        {
            Path = jobObject.Path;
        }

        public ArchivedObject(string path)
        {
            Path = path;
        }

        public string Path { get; }

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