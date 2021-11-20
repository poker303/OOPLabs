using System;
using System.Collections.Generic;
using System.IO;
using Backups.Storages;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private const string Path = @"C:\Users\Алексей\OneDrive\Рабочий стол\Lab3_BackUpS\";
        private BackupJob _backupJob1;
        private BackupJob _backupJob2;
        
        [SetUp]
        public void Setup()
        {
            _backupJob1 = new BackupJob("Backup1", typeof(SplitStorage), Path);
            _backupJob2 = new BackupJob("Backup2", typeof(SingleStorage), Path);
        }
        
        // Local test with real files.

        // [Test]
        // public void Test()
        // {
        //     RestorePoint restorePoint1 = _backupJob1.CreateRestorePoint("RestorePoint", Path);
        //     RestorePoint restorePoint2 = _backupJob2.CreateRestorePoint("RestorePoint", Path);
        //     var file = new FileInfo(@$"{Path}\JobObjects\FileA.txt");
        //     file.Delete();
        //     RestorePoint restorePoint3 = _backupJob1.CreateRestorePoint("RestorePoint", Path);
        //     RestorePoint restorePoint4 = _backupJob2.CreateRestorePoint("RestorePoint", Path);
        // }
        
        // Virtual tests for git.

        [Test]
        public void SingleStorageTest()
        {
            var file1 = new MyFile("FileA", DateTime.Now);
            var file2 = new MyFile("FileB", DateTime.Now);
            var file3 = new MyFile("FileC", DateTime.Now);
            
            var backupFiles = new List<MyFile>
            {
                file1,
                file2,
                file3
            };
            
            List<List<MyFile>> startResultFiles = _backupJob2.CreateVirtualRestorePoint("RestorePoint", backupFiles);
            int startResultFilesCount = startResultFiles[0].Count;
            backupFiles.Remove(file1);
            List<List<MyFile>> resultFiles = _backupJob2.CreateVirtualRestorePoint("RestorePoint", backupFiles);
            int resultFilesCount = resultFiles[0].Count;
            
            Assert.AreEqual(2, _backupJob2.Points.Count);
            Assert.AreEqual(3, startResultFilesCount);
            Assert.AreEqual(2, resultFilesCount);
        }
        
        [Test]
        public void SplitStorageTest()
        {
            var file1 = new MyFile("FileA", DateTime.Now);
            var file2 = new MyFile("FileB", DateTime.Now);
            var file3 = new MyFile("FileC", DateTime.Now);
            
            var backupFiles = new List<MyFile>
            {
                file1,
                file2,
                file3
            };
        
            int startResultFilesCount = _backupJob1.CreateVirtualRestorePoint("RestorePoint", backupFiles).Count;
            backupFiles.Remove(file1);
            
            int resultFilesCount = _backupJob1.CreateVirtualRestorePoint("RestorePoint", backupFiles).Count;
            Assert.AreEqual(2, _backupJob1.Points.Count);
            Assert.AreEqual(3, startResultFilesCount);
            Assert.AreEqual(2, resultFilesCount);
        }
    }
}