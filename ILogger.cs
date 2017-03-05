using PokemonGo.RocketAPI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogDebug(string message);
        //void LogWarning();
        //void LogError();

        void LogCritical(string message, dynamic data);
        void HashStatusUpdate(HashInfo info);
        void LogError(string message);
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
    }
}