using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class AuthConfigException  : Exception
    {
            public AuthConfigException(string message): base(message)
        {

        }
    }
}
