using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class PointsNumberLimit : ILimit
    {
        public List<RestorePoint> DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob, List<ILimit> limits)
        {
            if (backupJob.Points.Count <= backupJob.LimitRestorePointNumber)
            {
                return new List<RestorePoint>();
            }

            var deletedRestorePoints = new List<RestorePoint>() { backupJob.Points[0] };
            return deletedRestorePoints;
        }
    }
}