using System.Collections.Generic;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace Backups.Tools
{
    public class SingleStorage : IWayOfStorage
    {
        public List<Storage> CreateStorage(List<JobObject> jobObjects, string number)
        {
            List<Storage> listOfStorages = new List<Storage>();
            string nameOfStorage = NameCreator.Create(number);
            Storage storage = new Storage(nameOfStorage);
            foreach (JobObject jobObject in jobObjects)
            {
                storage.AddJobObject(jobObject);
            }

            listOfStorages.Add(storage);
            return listOfStorages;
        }
    }
}