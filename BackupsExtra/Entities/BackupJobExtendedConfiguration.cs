using Backups.Interfaces;
using Backups.Tools;
using BackupsExtra.Tools;

namespace BackupsExtra.Entities
{
    public class BackupJobExtendedConfiguration
    {
        private string _directoryPath;

        public BackupJobExtendedConfiguration(string directoryPath, IRepository repositoryType, IArchiver archiver)
        {
            RepositoryType = repositoryType ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            Archiver = archiver ?? throw new BackupsExtraException("Any of the constructor arguments are null");
            if (directoryPath == string.Empty)
                throw new BackupsException("String cannot be empty");
            _directoryPath = directoryPath;
        }

        public IArchiver Archiver { get; }
        public IRepository RepositoryType { get; }
    }
}