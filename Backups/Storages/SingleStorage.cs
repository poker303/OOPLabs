using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Storages
{
    public class SingleStorage : IStorage
    {
        public void SavingStorage(RestorePoint restorePoint, IBackup backupSaver, List<FileInfo> savedFiles, IFileSystem system)
        {
            var repositories = new List<Repository>();
            var choosingRepository = new Repository();
            choosingRepository.AddFiles(savedFiles);
            repositories.Add(choosingRepository);
            backupSaver.Saver(repositories, restorePoint, system);
        }
    }
}