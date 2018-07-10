using System;

namespace BookProcessor.Implementation
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.WriteLine("ERROR: " + message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine("INFO: " + message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine("WARN: " + message);
        }
    }
}
