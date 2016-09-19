using POGOProtos.Data;
using POGOProtos.Enums;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class Pokedex
    {
        private Dictionary<PokemonId, PokedexEntry> pokedexMap = new Dictionary<PokemonId, PokedexEntry>();
        
        public Pokedex()
        {
        }

        public void Reset()
        {
            pokedexMap.Clear();
        }

        /**
         * Add/Update a PokdexEntry.
         *
         * @param entry The entry to add or update
         */
        public void Add(PokedexEntry entry)
        {
            pokedexMap[entry.PokemonId] = entry;
        }

        /**
         * Get a pokedex entry value.
         *
         * @param pokemonId the ID of the pokemon to get
         * @return Entry if in pokedex or null if it doesn't
         */
        public PokedexEntry GetPokedexEntry(PokemonId pokemonId)
        {
            return pokedexMap[pokemonId];
        }
    }

}
