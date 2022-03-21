using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Classes
{
    public class LocalRepository : IRepository
    {
        private readonly string _path;
        public LocalRepository(string path)
        {
            _path = path;
        }

        public static int NewNumber()
        {
            int number = 1;
            while (Directory.Exists($@"C:/BackupDirectory/RestorePoint_{number.ToString()}"))
            {
                number++;
            }

            return number;
        }

        public List<Storage> SaveRestorePoint(List<JobObject> jobObjects, IWayOfStorage wayOfStorage)
        {
            if (wayOfStorage == null)
            {
                throw new NullException("way of storage is invalid");
            }

            string numberOfRestorePoint = NewNumber().ToString();
            List<Storage> storages = wayOfStorage.CreateStorage(jobObjects, numberOfRestorePoint);
            DirectoryInfo directoryOfRestorePoint =
                Directory.CreateDirectory(_path + @"/RestorePoint_" + numberOfRestorePoint);
            foreach (Storage storage in storages)
            {
                Directory.CreateDirectory(@"C:/NewFolder");
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    File.Copy(jobObject.FileInfo.FullName, @"C:/NewFolder/" + jobObject.FileInfo.Name);
                }

                ZipFile.CreateFromDirectory(@"C:/NewFolder", directoryOfRestorePoint.FullName + $@"/{storage.Name}.zip");
                Directory.Delete(@"C:/NewFolder", true);
            }

            return storages;
        }

        public void Restore(RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.ListStorages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    Unpack.Unpacking(
                        _path + "/RestorePoint_" + restorePoint.Number + "/" + storage.Name + ".zip",
                        jobObject.FileInfo.DirectoryName);
                }
            }
        }

        public void Restore(RestorePoint restorePoint, string path)
        {
            foreach (Storage storage in restorePoint.ListStorages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    Unpack.Unpacking(
                        _path + "/RestorePoint_" + restorePoint.Number + "/" + storage.Name + ".zip", path);
                }
            }
        }

        public void DeletePoint(RestorePoint restorePoint)
        {
            throw new System.NotImplementedException();
        }

        public void MergePoints(RestorePoint restorePoint1, RestorePoint restorePoint2, RestorePoint newRestorePoint)
        {
            string numberOfPoint = newRestorePoint.Number.ToString();
            Directory.CreateDirectory(@"C:/BackupsMerge");
            Restore(restorePoint1, @"C:/BackupsMerge");
            Restore(restorePoint2, @"C:/BackupsMerge");
            DirectoryInfo restorePointDir = Directory.CreateDirectory(_path + @"/RestorePoint_" + numberOfPoint);
            foreach (Storage storage in newRestorePoint.ListStorages)
            {
                Directory.CreateDirectory(@"C:/BackupsNew");
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    File.Copy(jobObject.FileInfo.FullName, @"C:/BackupsNew" + jobObject.FileInfo.Name);
                }

                ZipFile.CreateFromDirectory(@"C:/BackupsNew", restorePointDir.FullName + $@"C:/BackupsMerge{storage.Name}");
                Directory.Delete(@"C:/BackupsNew", true);
            }

            Directory.Delete(@"C:/BackupsMerge", true);
        }
    }
}