using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Responses;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.MapModels.FortModels
{
    public class PokestopLootResult
    {
        private FortSearchResponse response;

        public PokestopLootResult(FortSearchResponse response)
        {
            this.response = response;
        }

        public bool WasSuccessful()
        {
            return response.Result == FortSearchResponse.Types.Result.Success || response.Result == FortSearchResponse.Types.Result.InventoryFull;
        }

        public FortSearchResponse.Types.Result GetResult()
        {
            return response.Result;
        }

        public IList<ItemAward> GetItemsAwarded()
        {
            return response.ItemsAwarded;
        }

        public int GetExperience()
        {
            return response.ExperienceAwarded;
        }

        public FortSearchResponse ToPrimitive()
        {
            return response;
        }
    }
}
