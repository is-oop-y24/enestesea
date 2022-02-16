using System.Collections.Generic;
using BackupsExtra.Classes;

namespace BackupsExtra.Interfaces
{
    public interface IRepository
    {
        List<Storage> SaveRestorePoint(List<JobObject> jobObjects, IWayOfStorage wayOfStorage);
        void Restore(RestorePoint restorePoint);
        void Restore(RestorePoint restorePoint, string path);
        void DeletePoint(RestorePoint restorePoint);
        public void MergePoints(RestorePoint restorePoint1, RestorePoint restorePoint2, RestorePoint newRestorePoint);
    }
}
