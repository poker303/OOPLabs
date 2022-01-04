using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class RestorePoint
    {
        private List<Repository> _repositories;

        public RestorePoint(string name, int id, string restorePointLocation)
        {
            Id = id;
            Name = name;
            CreationTime = DateTime.Now;
            Location = restorePointLocation;
            _repositories = new List<Repository>();
        }

        public string Location { get; }
        public string Name { get; }
        public int Id { get; }
        public DateTime CreationTime { get; set; }

        public ReadOnlyCollection<Repository> GetRepositories()
        {
            return _repositories.AsReadOnly();
        }

        public void AddRepository(Repository repository)
        {
            _repositories.Add(repository);
        }

        public void Delete(Repository repository)
        {
            _repositories.Remove(repository);
        }

        public void DeleteRepositories(List<Repository> repositories)
        {
            foreach (Repository repository in repositories)
            {
                _repositories.Remove(repository);
            }
        }

        public void ChangeCreationTime(DateTime newDate)
        {
            CreationTime = newDate;
        }
    }
}