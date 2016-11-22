#region using directives

using System.Threading.Tasks;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using Google.Protobuf.Collections;
using POGOProtos.Inventory;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Inventory : BaseRpc
    {
        internal long LastInventoryTimestampMs;

        /// <summary>
        ///     Gets the last received <see cref="RepeatedField{T}" /> from PokémonGo.<br />
        ///     Only use this if you know what you are doing.
        /// </summary>
        public RepeatedField<InventoryItem> InventoryItems { get; } = new RepeatedField<InventoryItem>();

        public Inventory(Client client) : base(client)
        {
        }

        internal void RemoveInventoryItems(IEnumerable<InventoryItem> items)
        {
            foreach (var item in items)
            {
                InventoryItems.Remove(item);
            }
        }

        internal void UpdateInventoryItems(InventoryDelta delta)
        {
            if (delta?.InventoryItems == null || delta.InventoryItems.All(i => i == null))
            {
                return;
            }
            InventoryItems.AddRange(delta.InventoryItems.Where(i => i != null));
            // Only keep the newest ones
            foreach (var deltaItem in delta.InventoryItems.Where(d => d?.InventoryItemData != null))
            {
                var oldItems = new List<InventoryItem>();
                if (deltaItem.InventoryItemData.PlayerStats != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.PlayerStats != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PlayerCurrency != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.PlayerCurrency != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PlayerCamera != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.PlayerCamera != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.InventoryUpgrades != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.InventoryUpgrades != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PokedexEntry != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            i =>
                                i.InventoryItemData?.PokedexEntry != null &&
                                i.InventoryItemData.PokedexEntry.PokemonId ==
                                deltaItem.InventoryItemData.PokedexEntry.PokemonId)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.Candy != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            i =>
                                i.InventoryItemData?.Candy != null &&
                                i.InventoryItemData.Candy.FamilyId ==
                                deltaItem.InventoryItemData.Candy.FamilyId)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.Item != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            i =>
                                i.InventoryItemData?.Item != null &&
                                i.InventoryItemData.Item.ItemId == deltaItem.InventoryItemData.Item.ItemId)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PokemonData != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            i =>
                                i.InventoryItemData?.PokemonData != null &&
                                i.InventoryItemData.PokemonData.Id == deltaItem.InventoryItemData.PokemonData.Id)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.AppliedItems != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.AppliedItems != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.EggIncubators != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(i => i.InventoryItemData?.EggIncubators != null)
                            .OrderByDescending(i => i.ModifiedTimestampMs)
                            .Skip(1));
                }
                foreach (var oldItem in oldItems)
                {
                    InventoryItems.Remove(oldItem);
                }
            }
        }

        public async Task<ReleasePokemonResponse> TransferPokemon(ulong pokemonId)
        {
            var message = new ReleasePokemonMessage
            {
                PokemonId = pokemonId
            };

            return await PostProtoPayload<Request, ReleasePokemonResponse>(RequestType.ReleasePokemon, message);
        }

        public async Task<EvolvePokemonResponse> EvolvePokemon(ulong pokemonId)
        {
            var message = new EvolvePokemonMessage
            {
                PokemonId = pokemonId
            };

            return await PostProtoPayload<Request, EvolvePokemonResponse>(RequestType.EvolvePokemon, message);
        }

        public async Task<UpgradePokemonResponse> UpgradePokemon(ulong pokemonId)
        {
            var message = new UpgradePokemonMessage
            {
                PokemonId = pokemonId
            };

            return await PostProtoPayload<Request, UpgradePokemonResponse>(RequestType.UpgradePokemon, message);
        }

        public RepeatedField<InventoryItem> GetInventory()
        {
            return InventoryItems;
        }

        public async Task<RecycleInventoryItemResponse> RecycleItem(ItemId itemId, int amount)
        {
            var message = new RecycleInventoryItemMessage
            {
                ItemId = itemId,
                Count = amount
            };

            return
                await PostProtoPayload<Request, RecycleInventoryItemResponse>(RequestType.RecycleInventoryItem, message);
        }

        public async Task<UseItemXpBoostResponse> UseItemXpBoost()
        {
            var message = new UseItemXpBoostMessage
            {
                ItemId = ItemId.ItemLuckyEgg
            };

            return await PostProtoPayload<Request, UseItemXpBoostResponse>(RequestType.UseItemXpBoost, message);
        }

        public async Task<UseItemEggIncubatorResponse> UseItemEggIncubator(string itemId, ulong pokemonId)
        {
            var message = new UseItemEggIncubatorMessage
            {
                ItemId = itemId,
                PokemonId = pokemonId
            };

            return
                await PostProtoPayload<Request, UseItemEggIncubatorResponse>(RequestType.UseItemEggIncubator, message);
        }

        public async Task<GetHatchedEggsResponse> GetHatchedEgg()
        {
            return
                await
                    PostProtoPayload<Request, GetHatchedEggsResponse>(RequestType.GetHatchedEggs,
                        new GetHatchedEggsMessage());
        }

        public async Task<UseItemPotionResponse> UseItemPotion(ItemId itemId, ulong pokemonId)
        {
            var message = new UseItemPotionMessage
            {
                ItemId = itemId,
                PokemonId = pokemonId
            };

            return await PostProtoPayload<Request, UseItemPotionResponse>(RequestType.UseItemPotion, message);
        }

        public async Task<UseItemReviveResponse> UseItemRevive(ItemId itemId, ulong pokemonId)
        {
            var message = new UseItemReviveMessage
            {
                ItemId = itemId,
                PokemonId = pokemonId
            };

            return await PostProtoPayload<Request, UseItemReviveResponse>(RequestType.UseItemRevive, message);
        }

        public async Task<UseIncenseResponse> UseIncense(ItemId incenseType)
        {
            var message = new UseIncenseMessage
            {
                IncenseType = incenseType
            };

            return await PostProtoPayload<Request, UseIncenseResponse>(RequestType.UseIncense, message);
        }

        public async Task<UseItemGymResponse> UseItemInGym(string gymId, ItemId itemId)
        {
            var message = new UseItemGymMessage
            {
                ItemId = itemId,
                GymId = gymId,
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude
            };

            return await PostProtoPayload<Request, UseItemGymResponse>(RequestType.UseItemGym, message);
        }

        public async Task<NicknamePokemonResponse> NicknamePokemon(ulong pokemonId, string nickName)
        {
            var message = new NicknamePokemonMessage
            {
                PokemonId = pokemonId,
                Nickname = nickName
            };

            return await PostProtoPayload<Request, NicknamePokemonResponse>(RequestType.NicknamePokemon, message);
        }

        public async Task<SetFavoritePokemonResponse> SetFavoritePokemon(ulong pokemonId, bool isFavorite)
        {
            var message = new SetFavoritePokemonMessage
            {
                PokemonId = (long) pokemonId,
                IsFavorite = isFavorite
            };

            return await PostProtoPayload<Request, SetFavoritePokemonResponse>(RequestType.SetFavoritePokemon, message);
        }
    }
}