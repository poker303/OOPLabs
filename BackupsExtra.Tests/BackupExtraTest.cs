using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Backups;
using Backups.Storages;
using BackupsExtra.ClearingPoints.ClearingPointsAlgorithms;
using BackupsExtra.ClearingPointsAlgorithms;
using BackupsExtra.Merging;
using BackupsExtra.Restoration;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupExtraTest
    {
        private const string _path = @"C:\Users\Алексей\OneDrive\Рабочий стол\Lab3_BackUpS\";
        
        [Test]
        public void Removal()
        {
            IMerging merging = new VirtualMerging();
            IRestoration restoration = new VirtualRestoration();
            ILimit limit = new PointsNumberLimit();
            IRestorePointRemoval restorePointRemoval = new VirtualRemoval();

            IStorageSaving singleSaver = new SingleStorageSaving();
            IStorageSaving splitSaver = new SplitStorageSaving();
            IBackupSaving virtualSaver = new VirtualSaving();
            IFileSystem fileSystem = new VirtualFileSystem();
            ILoggerService loggerService = new FileLogger();

            var splitBackupJob = new ImprovedBackupJob("split", _path, splitSaver, loggerService, true, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);
            var singleBackupJob = new ImprovedBackupJob("single", _path, singleSaver, loggerService, true, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);

            const string filePath1 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileA.txt";
            const string filePath2 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileB.txt";
            const string filePath3 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileC.txt";

            var file1 = new FileInfo(filePath1);
            var file2 = new FileInfo(filePath2);
            var file3 = new FileInfo(filePath3);

            var savedFiles = new List<FileInfo>
            {
                file1,
                file2,
                file3,
            };
            
            splitBackupJob.AddJobObjects(fileSystem, savedFiles);

            RestorePoint restorePoint1 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint2 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);

            splitBackupJob.DeleteJobObjects(fileSystem, file1);

            RestorePoint restorePoint3 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint4 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);

            Assert.AreEqual(1, singleBackupJob.Points.Count);
            Assert.AreEqual(1, splitBackupJob.Points.Count);
        }

        [Test]
        public void Merge()
        {
            IMerging merging = new VirtualMerging();
            IRestoration restoration = new VirtualRestoration();
            ILimit limit = new PointsNumberLimit();
            IRestorePointRemoval restorePointRemoval = new VirtualRemoval();

            IStorageSaving singleSaver = new SingleStorageSaving();
            IStorageSaving splitSaver = new SplitStorageSaving();
            IBackupSaving virtualSaver = new VirtualSaving();
            IFileSystem fileSystem = new VirtualFileSystem();
            ILoggerService loggerService = new FileLogger();

            var splitBackupJob = new ImprovedBackupJob("split", _path, splitSaver, loggerService, true, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);
            var singleBackupJob = new ImprovedBackupJob("single", _path, singleSaver, loggerService, true, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);

            const string filePath1 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileA.txt";
            const string filePath2 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileB.txt";
            const string filePath3 = @"C:\Users\Алексей\OneDrive\Рабочий стол\fileC.txt";

            var file1 = new FileInfo(filePath1);
            var file2 = new FileInfo(filePath2);
            var file3 = new FileInfo(filePath3);

            var savedFiles = new List<FileInfo>
            {
                file1,
                file2,
                file3,
            };

            splitBackupJob.AddJobObjects(fileSystem, savedFiles);

            RestorePoint restorePoint1 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint2 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint3 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint4 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            splitBackupJob.DeleteJobObjects(fileSystem, file1);

            RestorePoint restorePoint5 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint6 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", virtualSaver, fileSystem.GetJobObjects().ToList(), fileSystem);

            splitBackupJob.Merge(restorePoint4, restorePoint6, splitBackupJob);
            Assert.AreEqual(3, restorePoint6.GetRepositories().Count);
        }
    }
}