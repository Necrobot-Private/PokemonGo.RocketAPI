using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Rpc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class Inventories : BaseRpc
    {
        public ItemBag ItemBag;
        public PokeBank PokeBank;
        public CandyJar CandyJar;
        public Pokedex Pokedex;
        public List<EggIncubator> Incubators = new List<EggIncubator>();
        public Hatchery Hatchery;
        public long LastInventoryUpdate = 0;

        /**
         * Creates Inventories and initializes content.
         *
         * @param api PokemonGo api
         */
        public Inventories(Client client) : base(client)
        {
            ItemBag = new ItemBag(client);
            PokeBank = new PokeBank();
            CandyJar = new CandyJar(client);
            Pokedex = new Pokedex();
            Hatchery = new Hatchery(client);
        }

        /**
         * Updates the inventories with latest data.
         *
         * @throws LoginFailedException  the login failed exception
         * @throws RemoteServerException the remote server exception
         */
        public async Task UpdateInventories() {
            await UpdateInventories(false);
        }

        /**
	     * Updates the inventories with the latest data.
	     *
	     * @param forceUpdate For a full update if true
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
        public async Task UpdateInventories(bool forceUpdate) {
		    if (forceUpdate) {
			    LastInventoryUpdate = 0;
			    ItemBag.Reset();
			    PokeBank.Reset();
			    CandyJar.Reset();
			    Pokedex.Reset();
			    Incubators.Clear();
			    Hatchery.Reset();
		    }

            var invReqMsg = new GetInventoryMessage()
            {
                LastTimestampMs = LastInventoryUpdate
            };
            GetInventoryResponse response = await PostProtoPayload<Request, GetInventoryResponse>(RequestType.GetInventory, invReqMsg);
           
            UpdateInventories(response);
	    }

	    /**
	     * Updates the inventories with the latest data.
	     *
	     * @param response the get inventory response
	     */
	    public void UpdateInventories(GetInventoryResponse response)
        {
            foreach (InventoryItem inventoryItem in response.InventoryDelta.InventoryItems)
            {
                InventoryItemData itemData = inventoryItem.InventoryItemData;

                // Hatchery
                if (itemData.PokemonData != null && itemData.PokemonData.PokemonId == PokemonId.Missingno && itemData.PokemonData.IsEgg)
                {
                    Hatchery.AddEgg(new EggPokemon(Client, itemData.PokemonData));
                }

                // PokeBank
                if (itemData.PokemonData != null && itemData.PokemonData.PokemonId != PokemonId.Missingno)
                {
                    PokeBank.AddPokemon(new Pokemon(Client, inventoryItem.InventoryItemData.PokemonData));
                }

                // Items
                if (itemData.Item != null && itemData.Item.ItemId != ItemId.ItemUnknown)
                {
                    ItemData item = itemData.Item;
                    ItemBag.AddItem(new Item(item));
                }

                // CandyJar
                if (itemData.Candy != null && itemData.Candy.FamilyId != PokemonFamilyId.FamilyUnset)
                {
                    CandyJar.SetCandy(
                            itemData.Candy.FamilyId,
                            itemData.Candy.Candy_
                    );
                }
                // player stats
                if (itemData.PlayerStats != null)
                {
                    Client.PlayerProfile.Stats = new Stats(itemData.PlayerStats);
                }

                // Pokedex
                if (itemData.PokedexEntry != null)
                {
                    Pokedex.Add(itemData.PokedexEntry);
                }

                if (itemData.EggIncubators != null)
                {
                    foreach (POGOProtos.Inventory.EggIncubator incubator in itemData.EggIncubators.EggIncubator)
                    {
                        Incubators.Add(new EggIncubator(Client, incubator));
                    }
                }

                LastInventoryUpdate = Utils.GetTime(true);
            }
        }
    }
}
