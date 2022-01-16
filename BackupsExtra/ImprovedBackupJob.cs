using System;
using System.Collections.Generic;
using System.IO;
using Backups;
using Backups.Storages;
using BackupsExtra.ClearingPoints.ClearingPointsAlgorithms;
using BackupsExtra.ClearingPointsAlgorithms;
using BackupsExtra.Merging;
using BackupsExtra.Restoration;

namespace BackupsExtra
{
    public class ImprovedBackupJob : BackupJob
    {
        private int _restorePointId = 0;
        private IStorageSaving _storage;
        private ILoggerService _loggerService;
        private IMerging _merging;
        private IRestoration _restoration;
        private ILimit _limit;
        private IRestorePointRemoval _restorePointRemoval;
        private List<ILimit> _limits;

        public ImprovedBackupJob(
            string name,
            string location,
            IStorageSaving storage,
            ILoggerService loggerService,
            int limitRestorePointNumber,
            DateTime limitRestorePointCreationDate,
            bool checkingAllLimitsOn,
            IMerging merging,
            IRestoration restoration,
            ILimit limit,
            IRestorePointRemoval restorePointRemoval,
            List<ILimit> limits = null)
            : base(name, location, storage)
        {
            _storage = storage;
            _loggerService = loggerService;
            LimitRestorePointNumber = limitRestorePointNumber;
            LimitRestorePointCreationDate = limitRestorePointCreationDate;
            CheckingAllLimitsOn = checkingAllLimitsOn;
            _merging = merging;
            _restoration = restoration;
            _limit = limit;
            _restorePointRemoval = restorePointRemoval;
            _limits = limits;
        }

        public int LimitRestorePointNumber { get; }
        public bool CheckingAllLimitsOn { get; }

        public DateTime LimitRestorePointCreationDate { get; }

        public new RestorePoint CreateRestorePoint(string restorePointName, IBackupSaving backupSaver, List<FileInfo> savedFiles, IFileSystem system)
        {
            _restorePointId++;
            var restorePoint = new RestorePoint(restorePointName, _restorePointId, Location);
            _storage.SavingStorage(restorePoint, backupSaver, savedFiles, system);
            Points.Add(restorePoint);
            _restorePointRemoval.Delete(_limit.DeleteExcessRestorePoints(_restorePointRemoval, this), this);
            _loggerService.LoggerOutput("Restore point has been created successfully.");
            return restorePoint;
        }

        public override void AddJobObjects(IFileSystem system, List<FileInfo> files)
        {
            system.AddJobObjects(files);
            _loggerService.LoggerOutput("Job objects have been added successfully.");
        }

        public void Merge(RestorePoint restorePoint1, RestorePoint restorePoint2, ImprovedBackupJob backupJob)
        {
            _merging.Merge(restorePoint1, restorePoint2, this, backupJob);
        }

        public void Restoration(RestorePoint restorePoint, string path)
        {
            _restoration.RestorationToDifferentLocation(restorePoint, path);
        }
    }
}