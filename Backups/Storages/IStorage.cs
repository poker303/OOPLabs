using System;
using System.Collections.Generic;

namespace Backups.Storages
{
    public interface IStorage
    {
        public bool SavingStorage(Type storageType, string restorePointName, string pathToBackup, int id);
        public List<List<MyFile>> SavingVirtualStorage(Type storageType, List<MyFile> files);
    }
}