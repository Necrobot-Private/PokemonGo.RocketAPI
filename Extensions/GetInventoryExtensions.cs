using POGOProtos.Inventory;
using POGOProtos.Networking.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Extensions
{
    public static class GetInventoryExtensions
    {
        public static void MergeWith(this GetInventoryResponse me, GetInventoryResponse update)
        {
            var delta = update.InventoryDelta;
            var InventoryItems = me.InventoryDelta.InventoryItems;

            if (delta?.InventoryItems == null || delta.InventoryItems.All(i => i == null))
            {
                return;
            }

            InventoryItems.AddRange(delta.InventoryItems.Where(x => x != null));


            //remove delete items

            foreach (var deletedItem in delta.InventoryItems.Where(p => p != null && p.DeletedItem != null))
            {
                var pokemon = InventoryItems.FirstOrDefault(p => p.InventoryItemData?.PokemonData?.Id == deletedItem.DeletedItem.PokemonId);
                if(pokemon != null)
                {
                    InventoryItems.Remove(pokemon);
                }
                InventoryItems.Remove(deletedItem);
            }
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
    }


}
