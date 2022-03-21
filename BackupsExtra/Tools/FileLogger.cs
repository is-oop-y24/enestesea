using System;
using System.IO;
using System.Text;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Tools
{
    [Serializable]
    public class FileLogger : ILogger
    {
        private string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void Log(string message)
        {
            using (FileStream fileStream = File.OpenWrite(_path))
            {
                byte[] information = new UTF8Encoding(true).GetBytes(message);
                fileStream.Write(information, 0, information.Length);
            }
        }
    }
}