using System.Collections.Generic;
using BackupsExtra.Classes;

namespace BackupsExtra.Interfaces
{
    public interface IWayOfStorage
    {
        public List<Storage> CreateStorage(List<JobObject> jobObjects, string number);
    }
}