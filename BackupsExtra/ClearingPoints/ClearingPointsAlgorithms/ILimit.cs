using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public interface ILimit
    {
        void DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob);
    }
}