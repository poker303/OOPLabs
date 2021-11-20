using System.IO;
using System.IO.Compression;

namespace Backups.Storages
{
    public class SplitStorage
    {
        public SplitStorage(string restorePointName, int restorePointId, string pathToBackup)
        {
            var jobObjectsDirectory = new DirectoryInfo(@$"{pathToBackup}\JobObjects");
            string restorePointDirectory = restorePointName + restorePointId;

            foreach (FileInfo file in jobObjectsDirectory.GetFiles())
            {
                Name = Path.GetFileNameWithoutExtension(
                    @$"{pathToBackup}\JobObjects\{file.Name}") + "_" + restorePointId;

                string pathFileToAdd =
                    @$"{pathToBackup}\JobObjects\{file.Name}";
                string archivePath =
                    @$"{pathToBackup}\{restorePointDirectory}\{Name}.zip";

                using ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
                zipArchive.CreateEntryFromFile(pathFileToAdd, file.Name);
            }
        }

        public string Name { get; set; }
    }
}
