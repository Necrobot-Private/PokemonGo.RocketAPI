using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class HasherException :Exception
    {
        public HasherException(string message): base(message)
        {

        }
    }
}
