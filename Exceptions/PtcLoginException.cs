using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class PtcLoginException  : Exception
    {
        public PtcLoginException(string message) : base(message) { }
    }
}
