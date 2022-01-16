using System.Collections.Generic;
using Backups;

namespace BackupsExtra.ClearingPointsAlgorithms
{
    public class VirtualRemoval : IRestorePointRemoval
    {
        public void Delete(List<RestorePoint> deletedRestorePoints, ImprovedBackupJob backupJob)
        {
            backupJob.DeleteRestorePoints(deletedRestorePoints);
        }
    }
}