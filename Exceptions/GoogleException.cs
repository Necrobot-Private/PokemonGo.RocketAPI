#region using directives

using System;

#endregion

namespace PokemonGo.RocketAPI.Exceptions
{
    public class GoogleException : Exception
    {
        public GoogleException(string message) : base(message)
        {
        }
    }
}