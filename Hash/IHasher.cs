using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public interface IHasher
    {
        Task<HashResponseContent> RequestHashesAsync(HashRequestContent request);
    }
}
