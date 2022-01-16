using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Backups.Storages
{
    public class LocalSaving : IBackupSaving
    {
        public void Saver(List<Repository> repositories, RestorePoint restorePoint, IFileSystem fileSystem)
        {
            int id = 1;
            string name = $"Files_{id.ToString()}";
            string restorePointDirectory = restorePoint.Name + restorePoint.Id;
            fileSystem.AddRestorePoint(Path.Combine(restorePoint.Location, restorePointDirectory));
            foreach (Repository repository in repositories)
            {
                var directory = new DirectoryInfo(Path.Combine(fileSystem.JobObjectsDirectory.FullName, "temp"));
                foreach (FileInfo file in repository.GetFiles())
                {
                    directory.Create();
                    file.CopyTo(Path.Combine(directory.FullName, file.Name));
                }

                string zipPath = Path.Combine(restorePoint.Location, restorePointDirectory, $"Files_{id.ToString()}.zip");
                ZipFile.CreateFromDirectory(directory.FullName, zipPath);
                id++;
                directory.Delete(true);
                restorePoint.AddRepository(repository);
            }
        }
    }
}