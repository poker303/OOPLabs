using System.IO;
using Backups;

namespace BackupsExtra.Restoration
{
    public class VirtualRestoration : IRestoration
    {
        public void RestorationToOriginalLocation(RestorePoint restorePoint)
        {
            foreach (Repository repository in restorePoint.GetRepositories())
            {
                foreach (FileInfo file in repository.GetFiles())
                {
                    var recoveryDirectory = new DirectoryInfo(file.Directory.ToString());
                    if (!recoveryDirectory.Exists)
                    {
                        recoveryDirectory.Create();
                    }

                    file.CopyTo(file.FullName);
                }
            }
        }

        public void RestorationToDifferentLocation(RestorePoint restorePoint, string path)
        {
            var recoveryDirectory = new DirectoryInfo(path);
            if (!recoveryDirectory.Exists)
            {
                recoveryDirectory.Create();
            }

            foreach (Repository repository in restorePoint.GetRepositories())
            {
                foreach (FileInfo file in repository.GetFiles())
                {
                    file.CopyTo(Path.Combine(recoveryDirectory.FullName, file.Name));
                }
            }
        }
    }
}