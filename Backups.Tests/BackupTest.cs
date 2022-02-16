using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Classes;
using Backups.Tests;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        [SetUp]
        public void Setup()
        {
            IWayOfStorage wayOfStorage = new SplitStorage();
            List<IRepository> listOfRepositories = new List<IRepository>();
            Directory.CreateDirectory(@"C:/BackupDirectory");
            IRepository local = new LocalRepository(@"C:/BackupDirectory");
            listOfRepositories.Add(local);
            BackupsJob _backupJob = new BackupsJob(listOfRepositories, wayOfStorage);
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
        }
    }
}