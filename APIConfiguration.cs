using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI
{
    public class APIConfiguration
    {
        //TODO : Migrate other configuration to here - or may by TinyIOC is good choice to do binding.

        public static ILogger Logger = new DefaultConsoleLogger();
    }
}
