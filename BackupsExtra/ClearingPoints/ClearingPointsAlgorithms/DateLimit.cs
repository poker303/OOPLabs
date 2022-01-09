using System;
using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;
using BackupsExtra.Exceptions;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class DateLimit : ILimit
    {
        public List<RestorePoint> DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob, List<ILimit> limits)
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
                throw new DeletingAllPointsException("Will have to delete all the point, it's impossible.");
            }

            return deletedRestorePoints;
        }
    }
}