using POGOProtos.Inventory;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class EggIncubator : BaseRpc
    {
        private POGOProtos.Inventory.EggIncubator proto;

	    /**
	     * Create new EggIncubator with given proto.
	     *
	     * @param api   the api
	     * @param proto the proto
	     */
	    public EggIncubator(Client client, POGOProtos.Inventory.EggIncubator proto) : base(client)
        {
            this.proto = proto;
        }

        /**
            * Returns the remaining uses.
            *
            * @return uses remaining
            */
        public int GetUsesRemaining()
        {
            return proto.UsesRemaining;
        }

        /**
            * Hatch an egg.
            *
            * @param egg the egg
            * @return status of putting egg in incubator
            * @throws RemoteServerException the remote server exception
            * @throws LoginFailedException  the login failed exception
            */
        public async Task<UseItemEggIncubatorResponse> HatchEgg(EggPokemon egg)
        {
            var reqMsg = new UseItemEggIncubatorMessage()
            {
                ItemId = proto.Id,
                PokemonId = egg.GetId()
            };

            UseItemEggIncubatorResponse response = await PostProtoPayload<Request, UseItemEggIncubatorResponse>(RequestType.UseItemEggIncubator, reqMsg);
            
            await Client.Inventories.UpdateInventories(true);

            return response;
	    }

	    /**
	     * Get incubator id.
	     *
	     * @return the id
	     */
	    public String GetId()
        {
            return proto.Id;
        }

        /**
         * Get incubator type.
         *
         * @return EggIncubatorType
         */
        public EggIncubatorType GetIncubatorType()
        {
            return proto.IncubatorType;
        }

        /**
         * Get the total distance you need to walk to hatch the current egg.
         *
         * @return total distance to walk to hatch the egg (km)
         */
        public double GetKmTarget()
        {
            return proto.TargetKmWalked;
        }

        /**
         * Get the distance walked before the current egg was incubated.
         *
         * @return distance to walked before incubating egg
         * @deprecated Wrong method name, use {@link #getKmStart()}
         */
        public double GetKmWalked()
        {
            return GetKmStart();
        }

        /**
         * Get the distance walked before the current egg was incubated.
         *
         * @return distance walked before incubating egg (km)
         */
        public double GetKmStart()
        {
            return proto.StartKmWalked;
        }

        /**
         * Gets the total distance to walk with the current egg before hatching.
         *
         * @return total km between incubation and hatching
         */
        public double GetHatchDistance()
        {
            return GetKmTarget() - GetKmStart();
        }

        /**
         * Get the distance walked with the current incubated egg.
         *
         * @return distance walked with the current incubated egg (km)
         */
        public double GetKmCurrentlyWalked()
        {
            return Client.PlayerProfile.Stats.GetKmWalked() - GetKmStart();
        }

        /**
         * Get the distance left to walk before this incubated egg will hatch.
         *
         * @return distance to walk before hatch (km)
         */
        public double GetKmLeftToWalk()
        {
            return GetKmTarget() - Client.PlayerProfile.Stats.GetKmWalked();
        }

        /**
         * Is the incubator currently being used
         *
         * @return currently used or not
         */
        public bool IsInUse()
        {
            return GetKmTarget() > Client.PlayerProfile.Stats.GetKmWalked();
        }
    }
}
