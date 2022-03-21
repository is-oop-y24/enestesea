using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Tools;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            IWayOfStorage wayOfStorage = new SplitStorage();
            List<IRepository> listOfRepositories = new List<IRepository>();
            Directory.CreateDirectory(@"C:/BackupDirectory");
            IRepository local = new LocalRepository(@"C:/BackupDirectory");
            listOfRepositories.Add(local);
            var cleaner = new AmountAlgorithm(2);
            var looger = new ConsoleLogger();
            var list = new List<ILogger>();
            list.Add(looger);
            BackupsJob backupJob = new BackupsJob(listOfRepositories, wayOfStorage, cleaner, list, false);
            using (FileStream fileStream = File.Create(@"C:/BackupDirectory/File1.docx"))
            {
                byte[] information = new UTF8Encoding(true).GetBytes("Test1");
                fileStream.Write(information, 0, information.Length);
            }

            using (FileStream fileStream = File.Create(@"C:/BackupDirectory/File2.docx"))
            {
                byte[] information = new UTF8Encoding(true).GetBytes("Test2");
                fileStream.Write(information, 0, information.Length);
            }

            var j = backupJob.AddJobObject(@"C:/BackupDirectory/File1.docx");
            backupJob.AddJobObject(@"C:/BackupDirectory/File2.docx");
            backupJob.CreateRestorePoint();
            Console.WriteLine(backupJob.JobObjectsNumber);
            backupJob.DeleteJobObject(j);
            backupJob.CreateRestorePoint();
            Console.WriteLine(backupJob.JobObjectsNumber);
            Console.WriteLine(backupJob.RestorePointsNumber);
            IWayOfStorage wayOfStorage2 = new SingleStorage();
            List<IRepository> listOfRepositories2 = new List<IRepository>();
            Directory.CreateDirectory(@"C:/BackupDirectory2");
            IRepository local2 = new LocalRepository(@"C:/BackupDirectory2");
        }
    }
}
