using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Backups
{
    public class VirtualFileSystem : IFileSystem
    {
        private List<List<FileInfo>> _points;
        private List<FileInfo> _jobObjects;
        private List<Repository> _repositories;

        public VirtualFileSystem()
        {
            _points = new List<List<FileInfo>>();
            _jobObjects = new List<FileInfo>();
            _repositories = new List<Repository>();
        }

        public override ReadOnlyCollection<List<FileInfo>> GetRestorePoints()
        {
            return _points.AsReadOnly();
        }

        public override List<FileInfo> AddRestorePoint(string path)
        {
            var files = new List<FileInfo>();
            _points.Add(files);
            return files;
        }

        public override void AddJobObjects(List<FileInfo> files)
        {
            _jobObjects.AddRange(files);
        }

        public override void DeleteJobObject(FileInfo file)
        {
            _jobObjects.Remove(file);
        }

        public override void AddRepository(Repository repository, RestorePoint restorePoint)
        {
            _repositories.Add(repository);
            restorePoint.AddRepository(repository);
        }

        public override ReadOnlyCollection<FileInfo> GetJobObjects()
        {
            return _jobObjects.AsReadOnly();
        }
    }
}