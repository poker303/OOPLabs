using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Storages
{
    public class SplitStorage : IStorage
    {
        public void SavingStorage(RestorePoint restorePoint, IBackup backupSaver, List<FileInfo> savedFiles, IFileSystem system)
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