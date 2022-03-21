using System.Collections.Generic;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace Backups.Tools
{
    public class SplitStorage : IWayOfStorage
    {
        public List<Storage> CreateStorage(List<JobObject> jobObjects, string number)
        {
            List<Storage> listOfStorages = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                string nameOfStorage = NameCreator.Create(number, jobObject.FileInfo.Name);
                Storage storage = new Storage(nameOfStorage);
                storage.AddJobObject(jobObject);
                listOfStorages.Add(storage);
            }

            return listOfStorages;
        }
    }
}