using System;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class CaptchaException : Exception
    {
        public string Url { get; set; }

        public CaptchaException(string url)
        {
            Url = url;
        }
    }
}
