using System.Linq;

namespace Backups.Tools
{
    public static class NameCreator
    {
        public static string Create(string number)
        {
            return "SingleStorage" + "_" + number;
        }

        public static string Create(string number, string nameOfFile)
        {
            if (string.IsNullOrEmpty(nameOfFile)) throw new BackupsException("Invalid name of file");
            string subString = $"_{number}";
            string[] subStrings = nameOfFile.Split('_');

            return subString.First() + subString;
        }
    }
}