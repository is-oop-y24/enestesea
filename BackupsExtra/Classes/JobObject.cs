using System.IO;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Classes
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