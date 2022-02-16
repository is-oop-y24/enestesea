using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups.Tools;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;

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
        }
    }
}
