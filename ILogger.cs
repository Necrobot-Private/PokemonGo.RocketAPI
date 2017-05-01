using PokemonGo.RocketAPI.Logging;
using System;

namespace PokemonGo.RocketAPI
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogDebug(string message);
        void LogCritical(string message, dynamic data);
        void HashStatusUpdate(HashInfo info);
        void LogError(string message);
        void LogFlaggedInit(string message);
        void LogErrorInit(string message);
    }

    public class DefaultConsoleLogger : ILogger
    {
        public void HashStatusUpdate(HashInfo info)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (HASH SERVER)  [{info.MaskedAPIKey}] in last 1 minute  {info.Last60MinAPICalles} request/min , AVG: {info.Last60MinAPIAvgTime:0.00} ms/request , Fastest : {info.Fastest}, Slowest: {info.Slowest}");
        }

        public void LogCritical(string message, dynamic data)
        {
            Console.WriteLine("ERROR - CRITICAL " + message);
        }

        public void LogDebug(string message)
        {
            Console.WriteLine("Debug : " + message);
        }

        public void LogError(string message)
        {
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public void LogFlaggedInit(string message)
        {
            Console.WriteLine(message);
        }

        public void LogErrorInit(string message)
        {
            Console.WriteLine(message);
        }
    }
}