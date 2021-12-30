﻿using System;
using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class DateLimit : ILimit
    {
        public void DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob)
        {
            var deletedRestorePoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in backupJob.Points)
            {
                if (restorePoint.CreationTime < backupJob.LimitRestorePointCreationDate)
                {
                    deletedRestorePoints.Add(restorePoint);
                }
            }

            if (deletedRestorePoints.Count == backupJob.Points.Count)
            {
                throw new Exception("Will have to delete all the point, it's impossible.");
            }

            restorePointRemoval.Delete(deletedRestorePoints, backupJob);
        }
    }
}