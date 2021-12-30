using System.IO;
using System.IO.Compression;
using Backups;

namespace BackupsExtra.Restoration
{
    public class LocalRestoration : IRestoration
    {
        public void RestorationToOriginalLocation(RestorePoint restorePoint)
        {
            var restorePointDirectory = new DirectoryInfo($"{restorePoint.Location}{restorePoint.Name}{restorePoint.Id}");
            var tempUnzipDirectory = new DirectoryInfo(Path.Combine(restorePointDirectory.FullName, "tempUnzipDirectory"));
            tempUnzipDirectory.Create();

            foreach (FileInfo archive in restorePointDirectory.GetFiles())
            {
                ZipFile.ExtractToDirectory(archive.FullName, tempUnzipDirectory.FullName);
            }

            foreach (FileInfo file in tempUnzipDirectory.GetFiles())
            {
                var directory = new DirectoryInfo(file.Directory.ToString());
                if (!directory.Exists)
                {
                    directory.Create();
                }

                file.CopyTo(file.FullName);
            }

            tempUnzipDirectory.Delete(true);
        }

        public void RestorationToDifferentLocation(RestorePoint restorePoint, string path)
        {
            var recoveryDirectory = new DirectoryInfo(path);
            if (!recoveryDirectory.Exists)
            {
                recoveryDirectory.Create();
            }

            var restorePointDirectory = new DirectoryInfo($"{restorePoint.Location}{restorePoint.Name}{restorePoint.Id}");
            var tempUnzipDirectory = new DirectoryInfo(Path.Combine(restorePointDirectory.FullName, "tempUnzipDirectory"));
            tempUnzipDirectory.Create();

            foreach (FileInfo archive in restorePointDirectory.GetFiles())
            {
                ZipFile.ExtractToDirectory(archive.FullName, tempUnzipDirectory.FullName);
            }

            foreach (FileInfo file in tempUnzipDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(path, file.Name));
            }

            tempUnzipDirectory.Delete(true);
        }
    }
}