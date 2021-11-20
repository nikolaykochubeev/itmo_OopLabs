using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var jobObjects = new List<JobObject>()
            {
                new ("C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups.Tests\\backuptest\\NewFile1.txt"),
                new ("C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups.Tests\\backuptest\\NewFile2.txt"),
                new ("C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups.Tests\\backuptest\\NewFile3.txt"),
            };
            var backupJob = new BackupJob(
                new RepositoryWithFileSystem(),
                new SingleStorageArchiver(),
                jobObjects,
                "C:\\Users\\nikol\\RiderProjects\\nikolaykochubeev\\Backups.Tests\\backuptest");
            backupJob.CreateRestorePoint();
            backupJob.RemoveJobObject(jobObjects[0].Id);
            backupJob.CreateRestorePoint();
        }
    }
}