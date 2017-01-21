using System;

namespace PokemonGo.RocketAPI.Authentication.Exceptions
{
    public class WrongCredentialsException : Exception
    {

        public WrongCredentialsException(string message) : base(message)
        {
            
        }

    }
}
