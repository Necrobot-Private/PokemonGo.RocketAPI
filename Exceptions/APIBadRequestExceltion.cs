using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class APIBadRequestException:Exception
    {
        public APIBadRequestException(string message) : base(message)
        {

        }
    }
}
