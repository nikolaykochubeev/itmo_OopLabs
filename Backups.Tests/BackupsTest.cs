using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {

        [Test]
        public void SplitStoragesWithRepositoryWithoutFileSystemTest()  
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem(), new SplitStoragesArchiver(), new List<JobObject>(),
                "Abstract directory");
            var jobObjects = new List<JobObject> { new("File 1"), new ("File 2") };
            backupJob.AddObjects(jobObjects);
            backupJob.CreateRestorePoint();
            backupJob.RemoveJobObject(jobObjects[0].Id);
            backupJob.CreateRestorePoint();
            
            Assert.AreEqual(backupJob.RestorePoints.Count, 2);
            Assert.AreEqual(backupJob.RestorePoints[0].Storages.Count, 2);
            Assert.AreEqual(backupJob.RestorePoints[1].Storages.Count, 1);
        }
    }
}