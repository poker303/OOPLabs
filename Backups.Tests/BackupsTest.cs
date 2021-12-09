using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Storages;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private const string _path = @"C:\Users\Алексей\OneDrive\Рабочий стол\Lab3_BackUpS\";
        private IStorageSaving _singleSaver = new SingleStorageSaving();
        private IStorageSaving _splitSaver = new SplitStorageSaving();
        private IBackupSaving _virtualSaver = new VirtualSaving();
        private IFileSystem _fileSystem = new VirtualFileSystem();

        [Test]
        public void VirtualStorageTest()
        {
            var splitBackupJob = new BackupJob("split",_path, _splitSaver);
            var singleBackupJob = new BackupJob("single",_path, _singleSaver);

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
                file3
            };
            
            splitBackupJob.AddJobObjects(_fileSystem, savedFiles);

            RestorePoint restorePoint1 = singleBackupJob.CreateRestorePoint("SingleRestorePoint",_virtualSaver,
                _fileSystem.GetJobObjects().ToList(), _fileSystem);
            RestorePoint restorePoint2 = splitBackupJob.CreateRestorePoint("SplitRestorePoint",_virtualSaver, 
                _fileSystem.GetJobObjects().ToList(), _fileSystem);
            
            splitBackupJob.DeleteJobObjects(_fileSystem, file1);

            RestorePoint restorePoint3 = singleBackupJob.CreateRestorePoint("SingleRestorePoint",_virtualSaver, 
                _fileSystem.GetJobObjects().ToList(), _fileSystem);
            RestorePoint restorePoint4 = splitBackupJob.CreateRestorePoint("SplitRestorePoint",_virtualSaver, 
                _fileSystem.GetJobObjects().ToList(), _fileSystem);

            Assert.AreEqual(1, restorePoint1.GetRepositories().Count);
            Assert.AreEqual(3, restorePoint2.GetRepositories().Count);
            Assert.AreEqual(1, restorePoint3.GetRepositories().Count);
            Assert.AreEqual(2, restorePoint4.GetRepositories().Count);
        }
    }
}