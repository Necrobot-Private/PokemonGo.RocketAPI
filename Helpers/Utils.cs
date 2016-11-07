#region using directives

using System;
using System.Collections.Generic;
using System.Data.HashFunction;
using System.IO;
using System.Linq;
using System.Net;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class Utils
    {
        public static ulong FloatAsUlong(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static long GetTime(bool ms = false)
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);

            if (ms)
                return (long)Math.Round(timeSpan.TotalMilliseconds);
            return (long)Math.Round(timeSpan.TotalSeconds);
        }
        
        public static uint GenerateLocation1(byte[] authTicket, double lat, double lng, double alt)
        {
            byte[] locationBytes = BitConverter.GetBytes(lat).Reverse()
                .Concat(BitConverter.GetBytes(lng).Reverse())
                .Concat(BitConverter.GetBytes(alt).Reverse()).ToArray();

            return HashBuilder.Hash32Salt(locationBytes, HashBuilder.Hash32(authTicket));
        }
        
        public static uint GenerateLocation2(double lat, double lng, double alt)
        {
            byte[] locationBytes = BitConverter.GetBytes(lat).Reverse()
                .Concat(BitConverter.GetBytes(lng).Reverse())
                .Concat(BitConverter.GetBytes(alt).Reverse()).ToArray();
            return HashBuilder.Hash32(locationBytes);
        }

        public static ulong GenerateRequestHash(byte[] authTicket, byte[] hashRequest)
        {
            ulong seed = HashBuilder.Hash64(authTicket);
            return HashBuilder.Hash64Salt64(hashRequest, seed);
        }
    }
}