using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Backups.Exceptions;

namespace Backups
{
    public class LocalFileSystem : IFileSystem
    {
        private List<List<FileInfo>> _points;
        private List<Repository> _repositories;
        private List<FileInfo> _jobObjects;
        public LocalFileSystem(string pathToObjects)
        {
            _repositories = new List<Repository>();
            _points = new List<List<FileInfo>>();
            JobObjectsDirectory = new DirectoryInfo(pathToObjects);
            if (JobObjectsDirectory.Exists)
            {
                throw new AddingDirectoryException("The directory already exists.");
            }

            JobObjectsDirectory.Create();

            _jobObjects = new List<FileInfo>();
        }

        public override ReadOnlyCollection<List<FileInfo>> GetRestorePoints()
        {
            var tempRestorePoints = new List<List<FileInfo>>();
            tempRestorePoints.AddRange(_points);
            return tempRestorePoints.AsReadOnly();
        }

        public override List<FileInfo> AddRestorePoint(string path)
        {
            var directory = new DirectoryInfo(path);
            if (directory.Exists)
            {
                throw new AddingDirectoryException("The directory already exists.");
            }

            var files = new List<FileInfo>();
            _points.Add(files);
            directory.Create();
            return files;
        }

        public override void AddJobObjects(List<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(JobObjectsDirectory.FullName, file.Name));
                _jobObjects.Add(file);
            }
        }

        public override void DeleteJobObject(FileInfo file)
        {
            foreach (FileInfo fill in JobObjectsDirectory.GetFiles())
            {
                if (fill.Name == file.Name)
                {
                    fill.Delete();
                }
            }

            _jobObjects.Remove(file);
        }

        public override void AddRepository(Repository repository, RestorePoint restorePoint)
        {
            _repositories.Add(repository);
        }

        public override ReadOnlyCollection<FileInfo> GetJobObjects()
        {
            return _jobObjects.AsReadOnly();
        }
    }
}