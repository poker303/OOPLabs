// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using Backups.Exceptions;
// using Backups.Storages;
//
// namespace Backups
// {
//     public class BackupService : IStorage
//     {
//         private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
//
//         public BackupJob AddBackupJob(string name, Type storageType, string location)
//         {
//             return new BackupJob(name, storageType, location);
//         }
//
//         // public bool AddRestorePoint(BackupJob backupJob, string restorePointName, string backupPlace)
//         // {
//         //     RestorePoint restorePoint = backupJob.CreateRestorePoint(restorePointName, backupPlace);
//         //     _restorePoints.Add(restorePoint);
//         //     return SavingStorage(backupJob.Storage, restorePointName, backupPlace, restorePoint.Id);
//         // }
//         // public List<List<MyFile>> AddVirtualRestorePoint(BackupJob backupJob, List<MyFile> files)
//         // {
//         //     RestorePoint restorePoint = backupJob.CreateVirtualRestorePoint();
//         //     _restorePoints.Add(restorePoint);
//         //     return SavingVirtualStorage(backupJob.Storage, files);
//         // }
//         //
//         // public bool SavingStorage(Type storageType, string restorePointName, string backupPlace, int id)
//         // {
//         //     if (storageType == typeof(SingleStorage))
//         //     {
//         //         return CreateSingleStorage(restorePointName, id, backupPlace);
//         //     }
//         //
//         //     if (storageType == typeof(SplitStorage))
//         //     {
//         //         return CreateSplitStorage(restorePointName, id, backupPlace);
//         //     }
//         //
//         //     throw new WrongStorageTypeException();
//         // }
//
//         // public List<List<MyFile>> SavingVirtualStorage(Type storageType, List<MyFile> files)
//         // {
//         //     if (storageType == typeof(SingleStorage))
//         //     {
//         //         return CreateVirtualSingleStorage(files);
//         //     }
//         //
//         //     if (storageType == typeof(SplitStorage))
//         //     {
//         //         return CreateVirtualSplitStorage(files);
//         //     }
//         //
//         //     throw new WrongStorageTypeException();
//         // }
//         //
//         // public List<RestorePoint> GetRestorePoints()
//         // {
//         //     return _restorePoints;
//         // }
//         //
//         // private bool CreateSingleStorage(string restorePointName, int id, string backupPlace)
//         // {
//         //     var jobObjectsDirectory = new DirectoryInfo(@$"{backupPlace}\JobObjects");
//         //
//         //     if (jobObjectsDirectory.GetFiles().Length == 0)
//         //     {
//         //         return true;
//         //     }
//         //
//         //     var singleStorage = new SingleStorage(restorePointName, id, backupPlace);
//         //
//         //     return true;
//         // }
//         //
//         // private List<List<MyFile>> CreateVirtualSingleStorage(List<MyFile> files)
//         // {
//         //     var backupFiles = new List<List<MyFile>> { files };
//         //     return backupFiles;
//         // }
//         //
//         // private bool CreateSplitStorage(string restorePointName, int id, string backupPlace)
//         // {
//         //     var splitStorage = new SplitStorage(restorePointName, id, backupPlace);
//         //
//         //     // return splitStorage
//         //     return true;
//         // }
//         //
//         // private List<List<MyFile>> CreateVirtualSplitStorage(List<MyFile> files)
//         // {
//         //     return files.Select(file => new List<MyFile> { file }).ToList();
//         // }
//     }
// }