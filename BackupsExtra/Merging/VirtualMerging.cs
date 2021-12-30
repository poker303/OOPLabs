using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;

namespace BackupsExtra.Merging
{
    public class VirtualMerging : IMerging
    {
        public void Merge(RestorePoint oldRestorePoint, RestorePoint newRestorePoint, ImprovedBackupJob improvedBackupJob1, ImprovedBackupJob improvedBackupJob2)
        {
            if (oldRestorePoint.GetRepositories().Count == 1 || newRestorePoint.GetRepositories().Count == 1)
            {
                if (improvedBackupJob1.Points.Contains(oldRestorePoint))
                {
                    improvedBackupJob1.Points.Remove(oldRestorePoint);
                    return;
                }

                improvedBackupJob2.Points.Remove(oldRestorePoint);

                return;
            }

            var oldRestorePointFiles = new List<FileInfo>();
            foreach (Repository repository in oldRestorePoint.GetRepositories())
            {
                oldRestorePointFiles.AddRange(repository.GetFiles());
            }

            var newRestorePointFiles = new List<FileInfo>();
            var repositories = new List<Repository>();
            foreach (Repository repository in newRestorePoint.GetRepositories())
            {
                newRestorePointFiles.AddRange(repository.GetFiles());
                repositories.Add(repository);
            }

            foreach (FileInfo file in oldRestorePointFiles.Where(file => !newRestorePointFiles.Contains(file)))
            {
                newRestorePointFiles.Add(file);
                var repository = new Repository();
                repository.AddFile(file);
                repositories.Add(repository);
            }

            newRestorePoint.DeleteRepositories(newRestorePoint.GetRepositories().ToList());

            foreach (Repository repository in repositories)
            {
                newRestorePoint.AddRepository(repository);
            }

            improvedBackupJob1.Points.Remove(oldRestorePoint);
        }
    }
}