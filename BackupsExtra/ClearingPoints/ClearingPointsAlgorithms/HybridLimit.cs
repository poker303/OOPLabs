using System.Collections.Generic;
using System.Linq;
using Backups;
using Backups.Exceptions;
using BackupsExtra.ClearingPointsAlgorithms;
using BackupsExtra.Exceptions;

namespace BackupsExtra.ClearingPoints.ClearingPointsAlgorithms
{
    public class HybridLimit : ILimit
    {
        public List<RestorePoint> DeleteExcessRestorePoints(IRestorePointRemoval restorePointRemoval, ImprovedBackupJob backupJob, List<ILimit> limits)
        {
            var deletedRestorePoints = new List<RestorePoint>();
            if (backupJob.CheckingAllLimitsOn)
            {
                deletedRestorePoints = limits[0].DeleteExcessRestorePoints(restorePointRemoval, backupJob);
                foreach (ILimit limit in limits)
                {
                    List<RestorePoint> tempDeletedRestorePoints = deletedRestorePoints;
                    if (limit is HybridLimit)
                    {
                        throw new WrongLimitException("You can't use hybrid limit in hybrid.");
                    }

                    List<RestorePoint> tempRestorePoints = limit.DeleteExcessRestorePoints(restorePointRemoval, backupJob);
                    foreach (RestorePoint tempRestorePoint in tempRestorePoints.Where(tempRestorePoint => tempDeletedRestorePoints.Contains(tempRestorePoint)))
                    {
                        tempDeletedRestorePoints.Remove(tempRestorePoint);
                    }

                    foreach (RestorePoint tempDeletedRestorePoint in tempDeletedRestorePoints)
                    {
                        deletedRestorePoints.Remove(tempDeletedRestorePoint);
                    }
                }
            }

            foreach (ILimit limit in limits)
            {
                if (limit is HybridLimit)
                {
                    throw new WrongLimitException("You can't use hybrid limit in hybrid.");
                }

                List<RestorePoint> tempRestorePoints = limit.DeleteExcessRestorePoints(restorePointRemoval, backupJob);
                foreach (RestorePoint tempRestorePoint in tempRestorePoints.Where(tempRestorePoint => !deletedRestorePoints.Contains(tempRestorePoint)))
                {
                    deletedRestorePoints.Add(tempRestorePoint);
                }
            }

            return deletedRestorePoints;
        }
    }
}