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
            string name = Path.Combine("Files_", restorePoint.Id.ToString());
            string restorePointDirectory = restorePoint.Name + restorePoint.Id;
            fileSystem.AddRestorePoint(Path.Combine(restorePoint.Path, restorePointDirectory));
            string zipPath = Path.Combine(restorePoint.Path, restorePointDirectory, $"{name}.zip");
            foreach (Repository repository in repositories)
            {
                fileSystem.AddJobObjects(repository.GetFiles().ToList());
                ZipFile.CreateFromDirectory(fileSystem.JobObjectsDirectory.FullName, zipPath);
                foreach (FileInfo file in repository.GetFiles())
                {
                    fileSystem.DeleteJobObject(file);
                }
            }
        }
    }
}