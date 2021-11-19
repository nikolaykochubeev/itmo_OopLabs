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
                new SingleStorage(),
                new List<JobObject>() { new ("C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups\\NewFile1.txt") },
                "C:\\Users\\nikol\\Desktop\\backuptest");
            backupJob.CreateRestorePoint();
        }
    }
}