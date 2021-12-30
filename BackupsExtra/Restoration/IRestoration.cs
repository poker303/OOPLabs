using Backups;

namespace BackupsExtra.Restoration
{
    public interface IRestoration
    {
        void RestorationToOriginalLocation(RestorePoint restorePoint);
        void RestorationToDifferentLocation(RestorePoint restorePoint, string path);
    }
}