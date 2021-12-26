using System.Collections.Generic;
using System.IO;

namespace Backups.Storages
{
    public class SingleStorageSaving : IStorageSaving
    {
        public void SavingStorage(RestorePoint restorePoint, IBackupSaving backupSaver, List<FileInfo> savedFiles, IFileSystem system)
        {
            var repositories = new List<Repository>();
            var choosingRepository = new Repository();
            choosingRepository.AddFiles(savedFiles);
            repositories.Add(choosingRepository);
            backupSaver.Saver(repositories, restorePoint, system);
        }
    }
}