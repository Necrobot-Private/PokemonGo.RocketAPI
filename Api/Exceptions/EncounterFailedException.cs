using System;

namespace PokemonGo.RocketAPI.Api.Exceptions
{
    public class EncounterFailedException : Exception
    {
        public EncounterFailedException() : base()
        {
        }

        public EncounterFailedException(String reason) : base(reason)
        {
        }

        public EncounterFailedException(Exception exception) : base("", exception)
        {
        }

        public EncounterFailedException(String reason, Exception exception) : base(reason, exception)
        {
        }
    }
}
