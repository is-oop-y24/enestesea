using System.IO;
using Backups.Tools;

namespace Backups.Classes
{
    public class JobObject
    {
        private readonly FileInfo _fileInfo;

        public JobObject(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new NoSuchFileException("there is no such file there");
            _fileInfo = fileInfo;
        }

        public FileInfo FileInfo => _fileInfo;
    }
}