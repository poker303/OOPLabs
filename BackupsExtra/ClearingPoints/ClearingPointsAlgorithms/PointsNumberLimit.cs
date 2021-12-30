using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class PointsNumberLimit : ILimit
    {
        public void DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob)
        {
            if (backupJob.Points.Count <= backupJob.LimitRestorePointNumber)
            {
                return;
            }

            var deletedRestorePoints = new List<RestorePoint>() { backupJob.Points[0] };
            restorePointRemoval.Delete(deletedRestorePoints, backupJob);
        }
    }
}