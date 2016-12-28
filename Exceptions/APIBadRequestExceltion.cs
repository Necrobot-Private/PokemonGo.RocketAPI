using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class APIBadRequestException:Exception
    {
        public APIBadRequestException(string message) : base(message)
        {

        }
    }
}
