using System.Collections.Generic;

namespace Backups.Storages
{
    public class VirtualSaving : IBackupSaving
    {
        public void Saver(List<Repository> repositories, RestorePoint restorePoint, IFileSystem fileSystem)
        {
            foreach (Repository repository in repositories)
            {
                fileSystem.AddRepository(repository, restorePoint);
            }
        }
    }
}