﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Backups.Classes;
using Backups.Services;
using Backups.Tools;

namespace Backups
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
            BackupsJob backupJob = new BackupsJob(listOfRepositories, wayOfStorage);
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

            backupJob.AddJobObject(@"C:/BackupDirectory/File1.docx");
            backupJob.AddJobObject(@"C:/BackupDirectory/File2.docx");
            backupJob.CreateRestorePoint();
            Console.WriteLine(backupJob.JobObjectsNumber);
            backupJob.DeleteJobObjectByPath(@"C:/BackupDirectory/File1.docx");
            backupJob.CreateRestorePoint();
            Console.WriteLine(backupJob.JobObjectsNumber);
            Console.WriteLine(backupJob.RestorePointsNumber);
        }
    }
}
