using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class ItemBag : BaseRpc
    {
	    private Dictionary<ItemId, Item> items = new Dictionary<ItemId, Item>();

        public ItemBag(Client client) : base(client)
        {
        }

        public void Reset()
        {
            items.Clear();
        }

        public void AddItem(Item item)
        {
            items[item.GetItemId()] = item;
        }

        /**
         * Remove item result.
         *
         * @param id       the id
         * @param quantity the quantity
         * @return the result
         */
        public async Task<RecycleInventoryItemResponse> RemoveItem(ItemId id, int quantity)
        {
            Item item = GetItem(id);
		    if (item.Count < quantity) {
			    throw new Exception("You cannont remove more quantity than you have");
            }

            var msg = new RecycleInventoryItemMessage
            {
                ItemId = id,
                Count = quantity
            };

            RecycleInventoryItemResponse response = await PostProtoPayload<Request, RecycleInventoryItemResponse>(RequestType.RecycleInventoryItem, msg);

            if (response.Result == RecycleInventoryItemResponse.Types.Result.Success) {
                item.Count = response.NewCount;
		    }

            return response;
        }

	    /**
	     * Gets item.
	     *
	     * @param type the type
	     * @return the item
	     */
	    public Item GetItem(ItemId type)
        {
            if (type == ItemId.ItemUnknown) // TODO: Double-check this is not type == ItemId.Unrecognized
            {
                throw new Exception("You cannot get item for UNRECOGNIZED");
            }

            // prevent returning null
            if (!items.ContainsKey(type))
            {
                return new Item(new ItemData()
                {
                    Count = 0,
                    ItemId = type
                });
            }

            return items[type];
        }

        public Dictionary<ItemId, Item>.ValueCollection GetItems()
        {
            return items.Values;
        }

        /**
         * Get used space inside of player inventory.
         *
         * @return used space
         */
        public int GetItemsCount()
        {
            int ct = 0;
            foreach (Item item in items.Values)
            {
                ct += item.Count;
            }
            return ct;
        }

        /**
         * use an item with itemID
         *
         * @param type type of item
         * @throws RemoteServerException the remote server exception
         * @throws LoginFailedException  the login failed exception
         */
        public async Task UseItem(ItemId type) {
		    if (type == ItemId.ItemUnknown) {
			    throw new Exception("You cannot use item for UNRECOGNIZED");
		    }

		    switch (type) {
			    case ItemId.ItemIncenseOrdinary:
                case ItemId.ItemIncenseSpicy:
                case ItemId.ItemIncenseCool:
                case ItemId.ItemIncenseFloral:

                    await UseIncense(type);
				    break;
			    default:
				    break;
		    }
	    }

	    /**
	     * use an incense
	     *
	     * @param type type of item
	     * @throws RemoteServerException the remote server exception
	     * @throws LoginFailedException  the login failed exception
	     */
	    public async Task<UseIncenseResponse> UseIncense(ItemId type) {
            var useIncenseMessage = new UseIncenseMessage()
            {
                IncenseType = type
            };

            // TODO : We need to set IncenseTypeValue = type.Number

            return await PostProtoPayload<Request, UseIncenseResponse>(RequestType.UseIncense, useIncenseMessage);
	    }


	    /**
	     * use an item with itemID
	     *
	     * @throws RemoteServerException the remote server exception
	     * @throws LoginFailedException  the login failed exception
	     */
	    public async Task<UseIncenseResponse> UseIncense() {

            return await UseIncense(ItemId.ItemIncenseOrdinary);
	    }

	    /**
	     * use a lucky egg
	     *
	     * @return the xp boost response
	     * @throws RemoteServerException the remote server exception
	     * @throws LoginFailedException  the login failed exception
	     */
	    public async Task<UseItemXpBoostResponse> UseLuckyEgg() {
            var xpMsg = new UseItemXpBoostMessage()
            {
                ItemId = ItemId.ItemLuckyEgg
            };

            return await PostProtoPayload<Request, UseItemXpBoostResponse>(RequestType.UseItemXpBoost, xpMsg);
	    }
    }

}
