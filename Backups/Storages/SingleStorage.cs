using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Storages
{
    public class SingleStorage
    {
        public SingleStorage(string restorePointName, int restorePointId, string pathToBackup)
        {
            Name = "Files_" + restorePointId;

            string restorePointDirectory = restorePointName + restorePointId;
            string startPath = @$"{pathToBackup}\JobObjects";
            string zipPath = @$"{pathToBackup}\{restorePointDirectory}\{Name}.zip";
            ZipFile.CreateFromDirectory(startPath, zipPath);
        }

        public string Name { get; set; }
    }
}