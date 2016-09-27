using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class MinimumClientVersionException : Exception
    {
        public Version CurrentApiVersion;
        public Version MinimumClientVersion;
        public MinimumClientVersionException(Version currentApiVersion, Version minimumClientVersion) : base()
        {
            this.CurrentApiVersion = currentApiVersion;
            this.MinimumClientVersion = minimumClientVersion;
        }
    }
}
