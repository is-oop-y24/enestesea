using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Tools
{
    [Serializable]
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}