using System.Linq;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Tools
{
    public static class NameCreator
    {
        public static string Create(string number)
        {
            return "SingleStorage" + "_" + number;
        }

        public static string Create(string number, string nameOfFile)
        {
            if (string.IsNullOrEmpty(nameOfFile)) throw new BackupsExtraException("Invalid name of file");
            string subString = $"_{number}";
            string[] subStrings = nameOfFile.Split('.');

            return subStrings.First() + subString;
        }
    }
}