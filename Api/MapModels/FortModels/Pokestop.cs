using Google.Common.Geometry;
using POGOProtos.Inventory.Item;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.MapModels.FortModels
{
    public class Pokestop : BaseRpc
    {
	    public FortData FortData;
        public long cooldownCompleteTimestampMs;

        /**
         * Instantiates a new Pokestop.
         *
         * @param api      the api
         * @param fortData the fort data
         */
        public Pokestop(Client client, FortData fortData) : base(client)
        {
            this.FortData = fortData;
            this.cooldownCompleteTimestampMs = fortData.CooldownCompleteTimestampMs;
        }

        /**
         * Returns the distance to a pokestop.
         *
         * @return the calculated distance
         */
        public double GetDistance()
        {
            S2LatLng pokestop = S2LatLng.FromDegrees(GetLatitude(), GetLongitude());
            S2LatLng player = S2LatLng.FromDegrees(Client.CurrentLatitude, Client.CurrentLongitude);
            return pokestop.GetEarthDistance(player);
        }

        /**
         * Returns whether or not a pokestop is in range.
         *
         * @return true when in range of player
         */
        public bool InRange()
        {
            return GetDistance() <= Client.ApiSettings.FortSettings.InteractionRangeInMeters;
        }

        /**
         * Returns whether or not the lured pokemon is in range.
         *
         * @return true when the lured pokemon is in range of player
         */
        public bool InRangeForLuredPokemon()
        {
            return GetDistance() <= Client.ApiSettings.MapSettings.PokemonVisibilityRange;
        }

        /**
         * can user loot this from current position.
         *
         * @return true when lootable
         */
        public bool CanLoot()
        {
            return CanLoot(false);
        }

        /**
         * Can loot bool.
         *
         * @param ignoreDistance the ignore distance
         * @return the bool
         */
        public bool CanLoot(bool ignoreDistance)
        {
            bool active = cooldownCompleteTimestampMs < Utils.GetTime(true);
            if (!ignoreDistance)
            {
                return active && InRange();
            }
            return active;
        }

        public String GetId()
        {
            return FortData.Id;
        }

        public double GetLatitude()
        {
            return FortData.Latitude;
        }

        public double GetLongitude()
        {
            return FortData.Longitude;
        }

        /**
         * Loots a pokestop for pokeballs and other items.
         *
         * @return PokestopLootResult
         */
        public async Task<PokestopLootResult> Loot()
        {
            var searchMessage = new FortSearchMessage
            {
                FortId = GetId(),
                FortLatitude = GetLatitude(),
                FortLongitude = GetLongitude(),
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude
            };

            FortSearchResponse response = await PostProtoPayload<Request, FortSearchResponse>(RequestType.FortSearch, searchMessage);

            cooldownCompleteTimestampMs = response.CooldownCompleteTimestampMs;
            return new PokestopLootResult(response);
        }
	
	    /**
	     * Adds a modifier to this pokestop. (i.e. add a lure module)
	     *
	     * @param item the modifier to add to this pokestop
	     * @return true if success
	     */
	    public async Task<bool> AddModifier(ItemId item)
        {
            var msg = new AddFortModifierMessage
            {
                ModifierType = item,
                FortId = GetId(),
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude
            };

            AddFortModifierResponse response = await PostProtoPayload<Request, AddFortModifierResponse>(RequestType.AddFortModifier, msg);

            // Sadly the server response does not contain any information to verify if the request was successful
            return true;
	    }
        
	    /**
	     * Get more detailed information about a pokestop.
	     *
	     * @return FortDetails
	     */
	    public async Task<FortDetails> GetDetails()
        {
            var reqMsg = new FortDetailsMessage
            {
                FortId = GetId(),
                Latitude = GetLatitude(),
                Longitude = GetLongitude()
            };

            FortDetailsResponse response = await PostProtoPayload<Request, FortDetailsResponse>(RequestType.FortDetails, reqMsg);
            return new FortDetails(response);
	    }

        
	    /**
	     * Returns whether this pokestop has an active lure.
	     *
	     * @return lure status
	     */
	    //@Deprecated
        public bool HasLurePokemon()
        {
            return FortData.LureInfo != null && FortData.LureInfo.LureExpiresTimestampMs > Utils.GetTime(true);
        }

        /**
         * Returns whether this pokestop has an active lure when detected on map.
         *
         * @return lure status
         */
        public async Task<bool> HasLure()
        {
            try
            {
                return await HasLure(false);
            }
            catch (Exception) {
                // No need
            }

            return false;
        }

        /**
	     * Returns whether this pokestop has an active lure.
	     *
	     * @param updateFortDetails to make a new request and Get updated lured status
	     * @return lure status
	     * @throws LoginFailedException  If login failed.
	     * @throws RemoteServerException If server communications failed.
	     */
        public async Task<bool> HasLure(bool updateFortDetails) {
		    if (updateFortDetails) {
                IList<FortModifier> modifiers = (await GetDetails()).GetModifier();
			    foreach (FortModifier modifier in modifiers) {
				    if (modifier.ItemId == ItemId.ItemTroyDisk) {
					    return true;
				    }
			    }

			    return false;
		    }

		    return FortData.ActiveFortModifier.Contains(ItemId.ItemTroyDisk);
	    }
    }
}
