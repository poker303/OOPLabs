using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Exceptions;
using Backups.Storages;

namespace Backups
{
    public class BackupJob : IStorage
    {
        private int _restorePointId = 0;

        // location path = @"C:\Users\Алексей\Desktop\Lab3_BackUpS\";
        public BackupJob(string name, Type storage, string location)
        {
            Name = name;
            Path = location;
            Storage = storage;
            Points = new List<RestorePoint>();
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public Type Storage { get; }
        public List<MyFile> Files { get; set; }

        public List<RestorePoint> Points { get; set; }

        public RestorePoint CreateRestorePoint(string restorePointName, string pathToBackup)
        {
            var directory = new DirectoryInfo(pathToBackup);
            if (!directory.Exists)
            {
                directory.Create();
            }

            var directory2 = new DirectoryInfo(@$"{pathToBackup}\JobObjects");
            if (!directory2.Exists)
            {
                directory2.Create();
            }

            _restorePointId++;

            var restorePoint = new RestorePoint(restorePointName, _restorePointId, Path);
            Points.Add(restorePoint);
            SavingStorage(Storage, restorePointName, Path, _restorePointId);

            return restorePoint;
        }

        public List<List<MyFile>> CreateVirtualRestorePoint(string restorePointName, List<MyFile> files)
        {
            var restorePoint = new RestorePoint(restorePointName, _restorePointId, Path);
            Points.Add(restorePoint);

            return SavingVirtualStorage(Storage, files);
        }

        public bool SavingStorage(Type storageType, string restorePointName, string pathToBackup, int id)
        {
            if (storageType == typeof(SingleStorage))
            {
                return CreateSingleStorage(restorePointName, id, pathToBackup);
            }

            if (storageType == typeof(SplitStorage))
            {
                return CreateSplitStorage(restorePointName, id, pathToBackup);
            }

            throw new InvalidStorageTypeException("Enter a different type of save.");
        }

        public List<List<MyFile>> SavingVirtualStorage(Type storageType, List<MyFile> files)
        {
            if (storageType == typeof(SingleStorage))
            {
                return CreateVirtualSingleStorage(files);
            }

            if (storageType == typeof(SplitStorage))
            {
                return CreateVirtualSplitStorage(files);
            }

            throw new InvalidStorageTypeException("Enter a different type of save.");
        }

        private bool CreateSingleStorage(string restorePointName, int id, string pathToBackup)
        {
            var jobObjectsDirectory = new DirectoryInfo(@$"{pathToBackup}\JobObjects");

            if (jobObjectsDirectory.GetFiles().Length == 0)
            {
                return true;
            }

            var singleStorage = new SingleStorage(restorePointName, id, pathToBackup);

            return true;
        }

        private List<List<MyFile>> CreateVirtualSingleStorage(List<MyFile> files)
        {
            var backupFiles = new List<List<MyFile>> { files };
            return backupFiles;
        }

        private bool CreateSplitStorage(string restorePointName, int id, string pathToBackup)
        {
            var splitStorage = new SplitStorage(restorePointName, id, pathToBackup);

            return true;
        }

        private List<List<MyFile>> CreateVirtualSplitStorage(List<MyFile> files)
        {
            return files.Select(file => new List<MyFile> { file }).ToList();
        }
    }
}