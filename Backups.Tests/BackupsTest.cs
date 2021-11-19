using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private BackupJob _backupJob;
        [SetUp]
        public void Setup()
        {
            _backupJob = new BackupJob(new RepositoryWithoutFileSystem(),
                new SingleStorage()
                ,new List<JobObject>(),
                "Abstract directory");
        }

        [Test]
        public void CreatingRestorePointWithRepositoryWithoutFileSystemTest_EverythingWorks()
        {
            var jobObject = new JobObject("Abstract file path");
            _backupJob.AddObject(jobObject);
            _backupJob.CreateRestorePoint();
            Assert.AreEqual(jobObject.Id, _backupJob.RestorePoints[0].Storages[0].ArchivedObjects[0].Id);
        }
    }
}