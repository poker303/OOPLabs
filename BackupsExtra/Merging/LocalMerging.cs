using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups;

namespace BackupsExtra.Merging
{
    public class LocalMerging : IMerging
    {
        public void Merge(RestorePoint oldRestorePoint, RestorePoint newRestorePoint, ImprovedBackupJob improvedBackupJobFirst, ImprovedBackupJob improvedBackupJobSecond)
        {
            string pathToOldRestorePoint =
                Path.Combine(oldRestorePoint.Location, $"{oldRestorePoint.Name}{oldRestorePoint.Id}");
            string pathToNewRestorePoint =
                Path.Combine(newRestorePoint.Location, $"{newRestorePoint.Name}{newRestorePoint.Id}");
            if (oldRestorePoint.GetRepositories().Count == 1 || newRestorePoint.GetRepositories().Count == 1)
            {
                if (improvedBackupJobFirst.Points.Contains(oldRestorePoint))
                {
                    improvedBackupJobFirst.Points.Remove(oldRestorePoint);
                    var directoryFirst = new DirectoryInfo(pathToOldRestorePoint);
                    directoryFirst.Delete(true);
                    return;
                }

                improvedBackupJobSecond.Points.Remove(oldRestorePoint);
                var directorySecond = new DirectoryInfo(pathToOldRestorePoint);
                directorySecond.Delete(true);

                return;
            }

            var oldRestorePointFiles = new List<FileInfo>();
            oldRestorePointFiles.AddRange(oldRestorePoint.GetRepositories().SelectMany(repository => repository.GetFiles()));

            var newRestorePointFiles = new List<FileInfo>();
            var repositories = new List<Repository>();
            foreach (Repository repository in newRestorePoint.GetRepositories())
            {
                newRestorePointFiles.AddRange(repository.GetFiles());
                repositories.Add(repository);
            }

            var tempDirectory = new DirectoryInfo(newRestorePoint.Location);

            foreach (FileInfo file in tempDirectory.GetFiles())
            {
                file.Delete();
            }

            int counter = 1;

            foreach (FileInfo file in oldRestorePointFiles.Where(file => !newRestorePointFiles.Contains(file)))
            {
                var repository = new Repository();
                DirectoryInfo directory = repository.CreateDirectory(Path.Combine(pathToNewRestorePoint, "tempDirectory"));
                file.CopyTo(Path.Combine(directory.FullName, file.Name));
                repository.AddFile(file);
                repositories.Add(repository);
                ZipFile.CreateFromDirectory(directory.FullName, Path.Combine(pathToNewRestorePoint, $"File_{counter++}.zip"));
                directory.Delete(true);
            }

            newRestorePoint.DeleteRepositories(newRestorePoint.GetRepositories().ToList());

            foreach (Repository repository in repositories)
            {
                newRestorePoint.AddRepository(repository);
            }

            improvedBackupJobFirst.Points.Remove(oldRestorePoint);
            var directoryThird = new DirectoryInfo(pathToOldRestorePoint);
            directoryThird.Delete(true);
        }
    }
}