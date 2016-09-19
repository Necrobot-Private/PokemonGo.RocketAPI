using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;
using PokemonGo.RocketAPI.Rpc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class Hatchery : BaseRpc
    {
        public HashSet<EggPokemon> eggs = new HashSet<EggPokemon>();
        
        public Hatchery(Client client) : base(client)
        {
        }

        public void Reset()
        {
            eggs.Clear();
        }

        public void AddEgg(EggPokemon egg)
        {
            eggs.Add(egg);
        }

        /**
         * Get if eggs has hatched.
         *
         * @return list of hatched eggs
         * @throws RemoteServerException e
         * @throws LoginFailedException  e
         */
        public async Task<List<HatchedEgg>> QueryHatchedEggs() {
            GetHatchedEggsResponse response = await PostProtoPayload<Request, GetHatchedEggsResponse>(RequestType.GetHatchedEggs, new GetHatchedEggsMessage());
            
            await Client.Inventories.UpdateInventories();
            List<HatchedEgg> eggs = new List<HatchedEgg>();
            for (int i = 0; i < response.PokemonId.Count; i++) {
			    eggs.Add(new HatchedEgg(response.PokemonId[i],
					    response.ExperienceAwarded[i],
					    response.CandyAwarded[i],
					    response.StardustAwarded[i]));
	        }
            return eggs;
	    }
    }

}
