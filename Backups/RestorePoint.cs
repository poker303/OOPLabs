using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Backups.Storages;

namespace Backups
{
    public class RestorePoint
    {
        // пересмотреть навсякий
        private List<Repository> _repositories;

        public RestorePoint(string name, int id, string restorePointLocation)
        {
            Id = id;
            Name = name;
            CreationTime = DateTime.Now;
            Path = restorePointLocation;
            _repositories = new List<Repository>();
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        private DateTime CreationTime { get; set; }

        public ReadOnlyCollection<Repository> GetRepositories()
        {
            return _repositories.AsReadOnly();
        }

        public void AddRepository(Repository repository)
        {
            _repositories.Add(repository);
        }
    }
}