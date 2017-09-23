/*
 *     This program is free software: you can redistribute it and/or modify
 *     it under the terms of the GNU General Public License as published by
 *     the Free Software Foundation, either version 3 of the License, or
 *     (at your option) any later version.
 *
 *     This program is distributed in the hope that it will be useful,
 *     but WITHOUT ANY WARRANTY; without even the implied warranty of
 *     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *     GNU General Public License for more details.
 *
 *     You should have received a copy of the GNU General Public License
 *     along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace PokemonGo.RocketAPI.Helpers
{
    public class RandomIdGenerator
    {
        /* 
         * OLD code
         * 
         * private static long MULTIPLIER = 16807;
        private static long MODULUS = 0x7FFFFFFF;

        private long rpcIdHigh = 1;
        private long rpcId = 2;

        /**
         * Generates next request id and increments count
         * @return the next request id
         */
        /*
       public long Next()
       {
           rpcIdHigh = MULTIPLIER * rpcIdHigh % MODULUS;
           return rpcId++ | (rpcIdHigh << 32);
       }
       */

        public static ulong LastRequestID { get; private set; }

        // Thanks to Noctem and Xelwon
        // Lehmer random number generator - https://en.wikipedia.org/wiki/Lehmer_random_number_generator

        ulong MersenePrime = 0x7FFFFFFF;           // M = 2^31 -1 = 2,147,483,647 (Mersenne prime M31)
        ulong PrimeRoot = 0x41A7;                  // A = 16807 (a primitive root modulo M31)
        ulong Quotient = 0x1F31D;                  // Q = 127773 = M / A (to avoid overflow on A * seed)
        ulong Rest = 0xB14;                        // R = 2836 = M % A (to avoid overflow on A * seed)
        public static ulong Hi = 1;
        public static ulong Lo = 2;

        public RandomIdGenerator()
        {
            LastRequestID = (LastRequestID == 0) ? 1 : LastRequestID;
        }

        public ulong Last()
        {
            return LastRequestID;
        }

        // Old method to obtain the request ID
        public ulong NextLehmerRandom()
        {
            Hi = 0;
            Lo = 0;
            ulong NewRequestID;

            Hi = LastRequestID / Quotient;
            Lo = LastRequestID % Quotient;

            NewRequestID = PrimeRoot * Lo - Rest * Hi;
            if (NewRequestID <= 0)
                NewRequestID = NewRequestID + MersenePrime;

            //Logger.Debug($"[OLD LEHMER] {NewRequestID.ToString("X")} [{Hi.ToString("X")},{Lo.ToString("X")}]");

            NewRequestID = NewRequestID % 0x80000000;
            LastRequestID = NewRequestID;

            return NewRequestID;
        }

        // New method to obtain the request ID (extracted from pgoapi)
        // TODO: Check this with pgoapi. This has  not sense for me (Xelwon) .
        // https://github.com/pogodevorg/pgoapi/blob/develop/pgoapi/rpc_api.py
        // Line 82
        public ulong NextSinceAPI0691()
        {
            Hi = PrimeRoot * Hi % MersenePrime;
            ulong NewRequestID = Lo++ | (Hi << 32);
            LastRequestID = NewRequestID;
            //Logger.Debug($"[NEW METHOD] {NewRequestID.ToString("X")} [{Hi.ToString("X")},{Lo.ToString("X")}]");

            return NewRequestID;
        }

        public ulong Next()
        {
            return NextSinceAPI0691();
        }
    }

    public class Uk27IdGenerator
    {
        private static int min = 1000;
        private static int max = 60000;
        private static readonly Random _random = new Random();

        public void Init(int _min, int _max)
        {
            min = _min;
            max = _max;
        }

        public int Next()
        {
            return _random.Next(min, max);
        }
    }
}
