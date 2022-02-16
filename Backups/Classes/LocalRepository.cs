using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Services;
using Backups.Tools;
namespace Backups.Classes;

public class LocalRepository : IRepository
{
    private readonly string _path;
    public LocalRepository(string path)
    {
        _path = path;
    }

    public List<Storage> SaveRestorePoint(List<JobObject> jobObjects, IWayOfStorage wayOfStorage)
    {
        if (wayOfStorage == null)
        {
            throw new NullException("way of storage is invalid");
        }

        string numberOfRestorePoint = NumberOfRestorePoint().ToString();
        List<Storage> storages = wayOfStorage.CreateStorage(jobObjects, numberOfRestorePoint);
        DirectoryInfo directoryOfRestorePoint =
            Directory.CreateDirectory(_path + @"/RestorePoint_" + numberOfRestorePoint);
        foreach (Storage storage in storages)
        {
            Directory.CreateDirectory(@"C:/BackupDirectory");
            foreach (JobObject jobObject in storage.JobObjects)
            {
                File.Copy(jobObject.FileInfo.FullName, @"C:/BackupDirectory/" + jobObject.FileInfo.Name);
            }

            ZipFile.CreateFromDirectory(@"C:/BackupDirectory", directoryOfRestorePoint.FullName + $@"/{storage.Name}.zip");
            Directory.Delete(@"C:/BackupDirectory", true);
        }

        return storages;
    }

    private static int NumberOfRestorePoint()
    {
        int number = 1;
        while (Directory.Exists($@"C:/BackupsDirectory/RestorePoint_{number.ToString()}"))
        {
            number++;
        }

        return number;
    }
}