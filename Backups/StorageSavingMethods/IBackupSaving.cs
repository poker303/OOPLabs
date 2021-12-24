using System.Collections.Generic;

namespace Backups.Storages
{
    public interface IBackupSaving
    {
        void Saver(List<Repository> repositories, RestorePoint restorePoint, IFileSystem fileSystem);
    }
}