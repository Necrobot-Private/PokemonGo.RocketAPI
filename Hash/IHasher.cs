using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public interface IHasher
    {
        long Client_Unknown25 { get; }
        Task<HashResponseContent> RequestHashesAsync(HashRequestContent request);
    }
}
