using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var backupJob = new BackupJob(
                new RepositoryWithFileSystem(),
                new SingleStorageArchiver(),
                new List<JobObject>() { new JobObject("C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups\\Program.cs") });
            backupJob.CreateRestorePoint(DateTime.Now);
        }
    }
}