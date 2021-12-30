using System.Collections.Generic;
using System.IO;
using Backups;

namespace BackupsExtra.ClearingPointsAlgorithms
{
    public class LocalRemoval : IRestorePointRemoval
    {
        public void Delete(List<RestorePoint> deletedRestorePoints, ImprovedBackupJob backupJob)
        {
            foreach (RestorePoint restorePoint in backupJob.Points)
            {
                foreach (RestorePoint restorePointToDelete in deletedRestorePoints)
                {
                    if (restorePoint != restorePointToDelete) continue;
                    var directory = new DirectoryInfo(Path.Combine(restorePointToDelete.Location, $"{restorePointToDelete.Name}{restorePointToDelete.Id}"));
                    directory.Delete(true);
                }
            }

            backupJob.DeleteRestorePoints(deletedRestorePoints);
        }
    }
}