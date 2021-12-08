using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Exceptions;
using Backups.Storages;

namespace Backups
{
    public class BackupJob
    {
        private int _restorePointId = 0;
        private IStorage _storage;

        // location path = @"C:\Users\Алексей\Desktop\Lab3_BackUpS\";
        public BackupJob(string name, string location, IStorage storage)
        {
            Name = name;
            Path = location;
            _storage = storage;
            Points = new List<RestorePoint>();
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<FileInfo> Files { get; set; }

        public List<RestorePoint> Points { get; set; }

        public RestorePoint CreateRestorePoint(string restorePointName, IBackup backupSaver, List<FileInfo> savedFiles, IFileSystem system)
        {
            _restorePointId++;
            var restorePoint = new RestorePoint(restorePointName, _restorePointId, Path);
            _storage.SavingStorage(restorePoint, backupSaver, savedFiles, system);
            Points.Add(restorePoint);
            return restorePoint;
        }

        public void AddJobObjects(IFileSystem system, List<FileInfo> files)
        {
            system.AddJobObjects(files);
        }

        public void DeleteJobObjects(IFileSystem system, FileInfo file)
        {
            system.DeleteJobObject(file);
        }
    }
}