using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Exceptions
{
    public class PtcLoginException  : Exception
    {
        public PtcLoginException(string message) : base(message) { }
    }
}
