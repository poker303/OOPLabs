using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Storages
{
    public interface IStorage
    {
        void SavingStorage(RestorePoint restorePoint, IBackup backupSaver, List<FileInfo> savedFiles, IFileSystem system);
    }
}