using POGOProtos.Enums;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class EvolutionForm
    {
        private PokemonId pokemonId;

        public EvolutionForm(PokemonId pokemonId)
        {
            this.pokemonId = pokemonId;
        }

        public bool IsFullyEvolved()
        {
            return EvolutionInfo.IsFullyEvolved(pokemonId);
        }

        public List<EvolutionForm> GetEvolutionForms()
        {
            return EvolutionInfo.GetEvolutionForms(pokemonId);
        }

        public int GetEvolutionStage()
        {
            return EvolutionInfo.GetEvolutionStage(pokemonId);
        }

        public PokemonId getPokemonId()
        {
            return pokemonId;
        }
    }

}
