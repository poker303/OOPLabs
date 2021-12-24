using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Backups
{
    public abstract class IFileSystem
    {
        public DirectoryInfo JobObjectsDirectory { get; set; }
        public abstract ReadOnlyCollection<List<FileInfo>> GetRestorePoints();
        public abstract List<FileInfo> AddRestorePoint(string path);
        public abstract void AddJobObjects(List<FileInfo> files);
        public abstract void DeleteJobObject(FileInfo file);
        public abstract void AddRepository(Repository repository, RestorePoint restorePoint);
        public abstract ReadOnlyCollection<FileInfo> GetJobObjects();
    }
}