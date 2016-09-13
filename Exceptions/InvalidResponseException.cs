#region using directives

using System;

#endregion

namespace PokemonGo.RocketAPI.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException()
        {
        }

        public InvalidResponseException(string message)
            : base(message)
        {
        }
    }
}