using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public interface IHasher
    {
        HashResponseContent RequestHashes(HashRequestContent request);
    }
}
