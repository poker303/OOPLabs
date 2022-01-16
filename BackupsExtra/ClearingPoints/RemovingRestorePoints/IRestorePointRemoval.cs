using System.Collections.Generic;
using Backups;

namespace BackupsExtra.ClearingPointsAlgorithms
{
    public interface IRestorePointRemoval
    {
        void Delete(List<RestorePoint> deletedRestorePoints, ImprovedBackupJob backupJob);
    }
}