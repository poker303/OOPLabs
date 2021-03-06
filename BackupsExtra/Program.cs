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

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            const string _path = @"C:\Users\Алексей\OneDrive\Рабочий стол\Lab3_BackUpS\";
            const string _pathToJobObjects = @"C:\Users\Алексей\OneDrive\Документы\GitHub\poker303\BackupsExtra\JobObjects";

            IMerging merging = new LocalMerging();
            IRestoration restoration = new LocalRestoration();
            ILimit limit = new DateLimit();
            IRestorePointRemoval restorePointRemoval = new LocalRemoval();

            IStorageSaving singleSaver = new SingleStorageSaving();
            IStorageSaving splitSaver = new SplitStorageSaving();
            IBackupSaving localSaver = new LocalSaving();
            IFileSystem fileSystem = new LocalFileSystem(_pathToJobObjects);
            ILoggerService loggerService = new FileLogger(true);

            var splitBackupJob = new ImprovedBackupJob("split", _path, splitSaver, loggerService, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);
            var singleBackupJob = new ImprovedBackupJob("single", _path, singleSaver, loggerService, 1, DateTime.Now, true, merging, restoration, limit, restorePointRemoval);

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

            RestorePoint restorePoint1 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", localSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint2 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", localSaver, fileSystem.GetJobObjects().ToList(), fileSystem);

            splitBackupJob.DeleteJobObjects(fileSystem, file1);

            RestorePoint restorePoint3 = singleBackupJob.CreateRestorePoint("SingleRestorePoint", localSaver, fileSystem.GetJobObjects().ToList(), fileSystem);
            RestorePoint restorePoint4 = splitBackupJob.CreateRestorePoint("SplitRestorePoint", localSaver, fileSystem.GetJobObjects().ToList(), fileSystem);

            splitBackupJob.Merge(restorePoint2, restorePoint4, splitBackupJob);
            singleBackupJob.Restoration(restorePoint3, "C:/Users/Алексей/Desktop/RestorationDirectory");
        }
    }
}