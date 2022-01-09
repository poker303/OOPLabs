using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public interface ILimit
    {
        List<RestorePoint> DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob, List<ILimit> limits = null);
    }
}