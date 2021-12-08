using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups.Storages
{
    public class VirtualSaving : IBackup
    {
        // добавить реализацию
        public void Saver(List<Repository> repositories, RestorePoint restorePoint, IFileSystem fileSystem)
        {
            foreach (Repository repository in repositories)
            {
                fileSystem.AddRepository(repository, restorePoint);
            }
        }
    }
}