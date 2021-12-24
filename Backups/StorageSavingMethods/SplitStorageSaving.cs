using System.Collections.Generic;
using System.IO;

namespace Backups.Storages
{
    public class SplitStorageSaving : IStorageSaving
    {
        public void SavingStorage(RestorePoint restorePoint, IBackupSaving backupSaver, List<FileInfo> savedFiles, IFileSystem system)
        {
            var repositories = new List<Repository>();
            foreach (FileInfo file in savedFiles)
            {
                var choosingRepository = new Repository();
                choosingRepository.AddFile(file);
                repositories.Add(choosingRepository);
            }

            backupSaver.Saver(repositories, restorePoint, system);
        }
    }
}