using POGOProtos.Data;
using POGOProtos.Data.Capture;
using POGOProtos.Map.Pokemon;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels.EncounterModels
{
    public class NormalEncounterResult : PokemonDetails, EncounterResult
    {
        private EncounterResponse response;

        public NormalEncounterResult(Client client, EncounterResponse response) : base(client, response.WildPokemon.PokemonData)
        {
            this.response = response;
        }

        /**
	     * Return the status of the encounter
	     *
	     * @return status of results
	     */
        public EncounterResponse.Types.Status GetStatus()
        {
            return response.Status;
        }

        public bool WasSuccessful()
        {
            return response != null
                    && GetStatus() == EncounterResponse.Types.Status.EncounterSuccess;
        }

        public EncounterResponse.Types.Background GetBackground()
        {
            return response.Background;
        }

        public CaptureProbability GetCaptureProbability()
        {
            return response.CaptureProbability;
        }

        public WildPokemon GetWildPokemon()
        {
            return response.WildPokemon;
        }

        public PokemonData GetPokemonData()
        {
            return response.WildPokemon.PokemonData;
        }

        public EncounterResponse toPrimitive()
        {
            return response;
        }
    }
}
