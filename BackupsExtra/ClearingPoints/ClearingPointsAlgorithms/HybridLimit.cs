using System.Collections.Generic;
using Backups;
using BackupsExtra.ClearingPointsAlgorithms;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class HybridLimit : ILimit
    {
        public void DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob)
        {
            var deletedRestorePoints = new List<RestorePoint>();
            if (backupJob.CheckingAllLimitsOn)
            {
                int index1 = 0;
                foreach (RestorePoint restorePoint in backupJob.Points)
                {
                    if (restorePoint.CreationTime >= backupJob.LimitRestorePointCreationDate || index1 >= backupJob.Points.Count - backupJob.LimitRestorePointNumber)
                    {
                        break;
                    }

                    deletedRestorePoints.Add(restorePoint);
                    index1++;
                }

                restorePointRemoval.Delete(deletedRestorePoints, backupJob);
                return;
            }

            int index2 = 0;
            foreach (RestorePoint restorePoint in backupJob.Points)
            {
                if (restorePoint.CreationTime < backupJob.LimitRestorePointCreationDate || index2 < backupJob.Points.Count - backupJob.LimitRestorePointNumber)
                {
                    deletedRestorePoints.Add(restorePoint);
                }

                index2++;
            }

            restorePointRemoval.Delete(deletedRestorePoints, backupJob);
        }
    }
}