using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Storages;

namespace Backups
{
    public class Backup
    {
        private DateTime _creationTime;

        // private int Size { get; set; }
        public Backup(int id, Type storageType)
        {
            Id = id;

            // Size;
            _creationTime = DateTime.Now;
            BackupingFiles = new List<File>();
            Points = new List<RestorePoint>();
        }

        public int Id { get; set; }
        public List<File> BackupingFiles { get; set; }

        public List<RestorePoint> Points { get; set; }

        public void AddFiles(params File[] files)
        {
            foreach (File file in files)
            {
                BackupingFiles.Add(file);
            }
        }

        public void ChangeFile(string fileName, int size)
        {
            File file = BackupingFiles.Find(f => f.FileName == fileName);
            if (file == null) throw new Exception("File was not found");
            file.ChangeSize(size);
        }

        public void RemoveFile(string name)
        {
            File file = BackupingFiles.Find(f => f.FileName == name);
            if (file == null) throw new Exception($"File {name} was not found");
            BackupingFiles.Remove(file);
        }
    }
}

public File GetFileByName(string name)
{
    // foreach ((string key, int value) in _filesInPoint)
    // {
    //     if (key != name) continue;
    //     return new File(key, value);
    // }
    return null;
}

public void AddFile(File file)
{
    throw new System.NotImplementedException();
}
