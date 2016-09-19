using POGOProtos.Data;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.InventoryModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class EggPokemon
    {
        private Client client;
	    private PokemonData proto;

        // API METHODS //

        /**
         * Incubate this egg.
         *
         * @param incubator : the incubator
         * @return status of putting egg in incubator
         * @throws LoginFailedException  if failed to login
         * @throws RemoteServerException if the server failed to respond
         */
        public async Task<UseItemEggIncubatorResponse> Incubate(EggIncubator incubator)
        {
		    if (incubator.IsInUse()) {
			    throw new Exception("Incubator already used");
            }
		    return await incubator.HatchEgg(this);
        }

        /**
	     * Get the current distance that has been done with this egg
	     *
	     * @return get distance already walked
	     */
        public double GetEggKmWalked()
        {
            if (!IsIncubate())
                return 0;

            EggIncubator incubator = client.Inventories.Incubators.SingleOrDefault(q => q.GetId().Equals(proto.EggIncubatorId));
            
            // incubator should not be null but why not eh
		    if (incubator == null)
			    return 0;
		    else
			    return proto.EggKmWalkedTarget - (incubator.GetKmTarget() - client.PlayerProfile.Stats.GetKmWalked());
	    }

	    // DELEGATE METHODS BELOW //

	    /**
	     * Build a EggPokemon wrapper from the proto.
	     *
	     * @param proto : the prototype
	     */
	    public EggPokemon(Client client, PokemonData proto)
        {
            this.client = client;

            if (!proto.IsEgg)
            {
                throw new Exception("You cant build a EggPokemon without a valid PokemonData.");
            }
            this.proto = proto;
        }

        public ulong GetId()
        {
            return proto.Id;
        }

        public double GetEggKmWalkedTarget()
        {
            return proto.EggKmWalkedTarget;
        }

        public ulong GetCapturedCellId()
        {
            return proto.CapturedCellId;
        }

        public ulong GetCreationTimeMs()
        {
            return proto.CreationTimeMs;
        }

        public string EggIncubatorId()
        {
            return proto.EggIncubatorId;
        }

        public bool IsIncubate()
        {
            return proto.EggIncubatorId.Length > 0;
        }
        
        public override int GetHashCode()
        {
            return proto.PokemonId.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj is EggPokemon) {
                EggPokemon other = (EggPokemon)obj;
                return (this.GetId() == other.GetId());
            }

            return false;
        }
    }

}
