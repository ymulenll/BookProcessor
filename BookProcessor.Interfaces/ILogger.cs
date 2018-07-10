﻿
namespace BookProcessor.Interfaces
{
    public interface ILogger
    {
        void LogWarning(string message);

        void LogInfo(string message);

        void LogError(string message);
    }
}
