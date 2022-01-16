using Backups;

namespace BackupsExtra.Merging
{
    public interface IMerging
    {
        void Merge(RestorePoint oldRestorePoint, RestorePoint newRestorePoint, ImprovedBackupJob improvedBackupJobFirst, ImprovedBackupJob improvedBackupJobSecond);
    }
}