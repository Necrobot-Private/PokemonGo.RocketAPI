using POGOProtos.Enums;
using PokemonGo.RocketAPI.Api.PokemonModels;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class PokeBank
    {
        public List<Pokemon> Pokemons = new List<Pokemon>();

        public PokeBank()
        {
        }

        public void Reset()
        {
            Pokemons.Clear();
        }

        /**
         * Add a pokemon to the pokebank inventory.  Will not add duplicates (pokemon with same id).
         *
         * @param pokemon Pokemon to add to the inventory
         */
        public void AddPokemon(Pokemon pokemon)
        {
            int numAlreadyAdded = Pokemons.Count(q => pokemon.GetId() == q.GetId());
            if (numAlreadyAdded < 1)
                Pokemons.Add(pokemon);
        }

	    /**
	     * Gets pokemon by pokemon id.
	     *
	     * @param id the id
	     * @return the pokemon by pokemon id
	     */
	    public IEnumerable<Pokemon> GetPokemonByPokemonId(PokemonId id)
        {
            return Pokemons.Where(q => q.GetPokemonId() == id);
	    }

	    /**
	     * Remove pokemon.
	     *
	     * @param pokemon the pokemon
	     */
	    public void RemovePokemon(Pokemon pokemon)
        {
            Pokemons.RemoveAll(q => q.GetId() == pokemon.GetId());
        }

	    /**
	     * Get a pokemon by id.
	     *
	     * @param id the id
	     * @return the pokemon
	     */
	    public Pokemon GetPokemonById(ulong id)
        {
            return Pokemons.SingleOrDefault(q => q.GetId() == id);
        }
    }

}
