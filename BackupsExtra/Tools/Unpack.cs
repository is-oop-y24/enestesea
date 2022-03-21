using System.IO.Compression;

namespace BackupsExtra.Tools
{
    public static class Unpack
    {
        public static void Unpacking(string file, string dir)
        {
            ZipFile.ExtractToDirectory(file, dir, true);
        }
    }
}