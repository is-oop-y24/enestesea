using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Tools;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;
using NUnit.Framework;
using ILogger = BackupsExtra.Interfaces.ILogger;

namespace BackupsExtra.Tests
{
    public class test
    {     
        private BackupsJob _backupJob;

        [SetUp]
        public void Setup()
        {
            var listOfRep = new List<IRepository>();
            Directory.CreateDirectory(@"C:/BackupDirectory");
            var localRepository = new LocalRepository(@"C:/BackupDirectory");
            listOfRep.Add(localRepository);
            var wayOfStorage = new SplitStorage();
            var logger = new ConsoleLogger();
            var loggers = new List<ILogger>() {logger};
            var cleaner = new AmountAlgorithm(12);
            _backupJob = new BackupsJob(listOfRep, wayOfStorage, cleaner, loggers, true);
            using (FileStream fs = File.Create(@"C:/BackupDirectory/File1.docx"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("Test1");
                fs.Write(info, 0, info.Length);
            }
            using (FileStream fs = File.Create(@"C:/BackupDirectory/File2.docx"))
            {   
                byte[] info = new UTF8Encoding(true).GetBytes("Test2");
                fs.Write(info, 0, info.Length);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(@"C:/BackupDirectory", true);
        }

        [Test]
        public void CheckRestorePointsAndStorages()
        {
            _backupJob.AddJobObject(@"C:/BackupDirectory/File1.docx");
            _backupJob.AddJobObject(@"C:/BackupDirectory/File2.docx");
            _backupJob.CreateRestorePoint();
            _backupJob.DeleteJobObjectByPath(@"C:/BackupDirectory/File1.docx");
            _backupJob.CreateRestorePoint();
            if (!File.Exists(@"C:/BackupDirectory/RestorePoint_1/File1_1.zip") && !File.Exists(@"C:/BackupDirectory/RestorePoint_1/File2_1.zip") && !File.Exists(@"C:/BackupsDirectory/RestorePoint_2/File2_2.zip"))
            {
                Assert.Fail();
            }

        }

        [Test]
        public void DirectoryCheck()
        {
            _backupJob.AddJobObject(@"C:/BackupDirectory/File1.docx");
            _backupJob.AddJobObject(@"C:/BackupDirectory/File2.docx");
            _backupJob.CreateRestorePoint();
            _backupJob.CreateRestorePoint();
            if (!File.Exists(@"C:/BackupDirectory/RestorePoint_1/File1_1.zip") && !File.Exists(@"C:/BackupDirectory/RestorePoint_1/File2_1.zip") && !File.Exists(@"C:/BackupsDirectory/RestorePoint_2/File1_2.zip") && !File.Exists(@"C:/BackupsDirectory/RestorePoint_2/File2_2.zip"))
            {
                Assert.Fail();
            }
        }

        [Test]

        public void Recovery()
        {
            _backupJob.AddJobObject(@"C:/BackupDirectory/File1.docx");
            _backupJob.AddJobObject(@"C:/BackupDirectory/File2.docx");
            RestorePoint firstRestorePoint = _backupJob.CreateRestorePoint();
            _backupJob.CreateRestorePoint();
            File.Delete(@"C:/BackupDirectory/File1.docx");
            File.Delete(@"C:/BackupDirectory/File2.docx");
            _backupJob.RestoreDefault(firstRestorePoint);
            if (!File.Exists(@"C:/BackupDirectory/File1.docx") && !File.Exists(@"C:/BackupDirectory/File2.docx"))
            {
                Assert.Fail();
            }
        }
    }
}
