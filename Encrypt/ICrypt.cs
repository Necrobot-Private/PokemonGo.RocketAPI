using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Encrypt
{
    public interface ICrypt
    {
        byte[] Encrypt(byte[] input, uint ms);
    }
}
