namespace PokemonGo.RocketAPI
{
    public static class APIConfiguration
    {
        //TODO : Migrate other configuration to here - or may by TinyIOC is good choice to do binding.
        public static ILogger Logger = new DefaultConsoleLogger();

        public static ICaptchaResolver CaptchaResolver  = null;
    }
}
