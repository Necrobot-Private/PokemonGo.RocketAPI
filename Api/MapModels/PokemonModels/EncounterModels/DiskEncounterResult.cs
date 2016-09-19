using POGOProtos.Data;
using POGOProtos.Data.Capture;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels.EncounterModels
{
    public class DiskEncounterResult : PokemonDetails, EncounterResult
    {
        public DiskEncounterResponse response;

        public DiskEncounterResult(Client client, DiskEncounterResponse response) : base(client, response.PokemonData)
        {
            this.response = response;
        }
        
        public bool WasSuccessful()
        {
            return response != null
                    && response.Result == DiskEncounterResponse.Types.Result.Success;
        }
        
        //TODO: i have conveted the DiskEncounter response to maintain compatibility, if not required
        //i think will be better to remove this method

        /**
	     * Return the status of the encounter
	     *
	     * @return status of results
	     */
        public EncounterResponse.Types.Status GetStatus()
        {
            if (response == null)
                return EncounterResponse.Types.Status.EncounterNotFound; // TODO Review this. In java code this was a null value.

            switch (response.Result)
            {
                case DiskEncounterResponse.Types.Result.Unknown:
                    return EncounterResponse.Types.Status.EncounterError;
                case DiskEncounterResponse.Types.Result.Success:
                    return EncounterResponse.Types.Status.EncounterSuccess;
                case DiskEncounterResponse.Types.Result.NotAvailable:
                    return EncounterResponse.Types.Status.EncounterNotFound;
                case DiskEncounterResponse.Types.Result.NotInRange:
                    return EncounterResponse.Types.Status.EncounterNotInRange;
                case DiskEncounterResponse.Types.Result.EncounterAlreadyFinished:
                    return EncounterResponse.Types.Status.EncounterAlreadyHappened;
                case DiskEncounterResponse.Types.Result.PokemonInventoryFull:
                    return EncounterResponse.Types.Status.PokemonInventoryFull;
                default:
                    // Should never Get here unless new enum values were added.
                    return EncounterResponse.Types.Status.EncounterError;
            }
        }

        public CaptureProbability GetCaptureProbability()
        {
            return response.CaptureProbability;
        }

        public PokemonData GetPokemonData()
        {
            return response.PokemonData;
        }

        public DiskEncounterResponse ToPrimitive()
        {
            return response;
        }
    }

}
