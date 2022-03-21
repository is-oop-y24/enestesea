using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Services
{
    public interface IRepository
    {
        List<Storage> SaveRestorePoint(List<JobObject> jobObjects, IWayOfStorage wayOfStorage);
    }
}
