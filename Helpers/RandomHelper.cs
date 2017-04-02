#region using directives

using System;
using System.Threading.Tasks;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random _random = new Random();

        public static long GetLongRandom(long min, long max)
        {
            var buf = new byte[8];
            _random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand%(max - min)) + min;
        }

        public static void RandomSleep(int min, int max)
        {
            Task.Delay(  (_random.Next(min, max))).Wait();
        }

        public static void RandomSleep(int average)
        {
            RandomSleep(average-100, average+100);
        }

        public async static Task RandomDelay(int average)
        {
            await RandomDelay(average-100, average+100);
        }

        public async static Task RandomDelay(int min, int max)
        {
            await Task.Delay(  (_random.Next(min, max)));
        }
    }
}