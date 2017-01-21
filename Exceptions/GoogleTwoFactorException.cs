#region using directives

using System;

#endregion

namespace PokemonGo.RocketAPI.Exceptions
{
    public class GoogleTwoFactorException : Exception
    {
        public GoogleTwoFactorException(string message) : base(message)
        {
        }
    }
}