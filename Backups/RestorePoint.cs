using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Storages;

namespace Backups
{
    public class RestorePoint
    {
        public RestorePoint(string name, int id, string restorePointLocation)
        {
            Id = id;
            Name = name;
            CreationTime = DateTime.Now;
            var directory = new DirectoryInfo(@$"{restorePointLocation}\{Name}{Id}");

            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        public List<FileInfo> Files { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        private DateTime CreationTime { get; set; }
    }
}