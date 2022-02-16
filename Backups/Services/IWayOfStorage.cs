using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Services
{
    public interface IWayOfStorage
    {
        public List<Storage> CreateStorage(List<JobObject> jobObjects, string number);
    }
}