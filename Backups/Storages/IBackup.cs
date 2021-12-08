using System;
using System.Collections.Generic;

namespace Backups.Storages
{
    public interface IBackup
    {
        void Saver(List<Repository> repositories, RestorePoint restorePoint, IFileSystem fileSystem);
    }
}