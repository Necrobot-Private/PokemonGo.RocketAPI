using POGOProtos.Data;
using POGOProtos.Data.Capture;
using POGOProtos.Networking.Responses;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels.EncounterModels
{
    public interface EncounterResult
    {
        bool WasSuccessful();

        EncounterResponse.Types.Status GetStatus();

        PokemonData GetPokemonData();

        CaptureProbability GetCaptureProbability();
    }
}
