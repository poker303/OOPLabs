using System;
using System.Collections.Generic;

namespace Backups.Storages
{
    public interface IStorage
    {
        public void SavingStorage(Type storageType, string restorePointName, string pathToBackup, int id);
        public List<List<MyFile>> SavingStorage(Type storageType, List<MyFile> files);
    }
}