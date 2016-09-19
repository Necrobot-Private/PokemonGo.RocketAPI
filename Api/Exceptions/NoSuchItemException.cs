using System;

namespace PokemonGo.RocketAPI.Api.Exceptions
{
    public class NoSuchItemException : Exception
    {

        public NoSuchItemException() : base()
        {
        }

        public NoSuchItemException(String reason) : base(reason)
        {
        }

        public NoSuchItemException(Exception exception) : base("", exception)
        {
        }
    }
}
