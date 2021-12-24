using System.Collections.Generic;
using System.IO;

namespace Backups.Storages
{
    public interface IStorageSaving
    {
        void SavingStorage(RestorePoint restorePoint, IBackupSaving backupSaver, List<FileInfo> savedFiles, IFileSystem system);
    }
}