using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class BackupJobManager
    {
        private readonly List<BackupJob> _backupJobs;

        public BackupJobManager(string path = null)
        {
            _backupJobs = path == null ? new List<BackupJob>() : JsonConvert.DeserializeObject<List<BackupJob>>(File.ReadAllText(path));
        }

        public void AddBackupJob(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new BackupsExtraException("backupJob can not be the null");
            _backupJobs.Add(backupJob);
        }

        public BackupJob GetBackupJob(Guid backUpJobId)
        {
            return _backupJobs.FirstOrDefault(job => job.Id == backUpJobId);
        }

        public void Export(string path)
        {
            File.WriteAllText(Path.GetFullPath(path), JsonConvert.SerializeObject(
                _backupJobs,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                }));
        }
    }
}