#region using directives

using System.Threading.Tasks;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using System.Collections.Generic;
using Google.Protobuf;
using System;
using PokemonGo.RocketAPI.Helpers;
using Google.Protobuf.Collections;
using POGOProtos.Inventory;
using System.Linq;
using POGOProtos.Data;
using System.Collections.Concurrent;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void OnInventoryUpdateHandler();

    public class Inventory : BaseRpc
    {
        public Inventory(Client client) : base(client)
        {
        }

        public event OnInventoryUpdateHandler OnInventoryUpdated;
        public ConcurrentDictionary<int, InventoryItem> InventoryItems = new ConcurrentDictionary<int, InventoryItem>();

        private bool RemoveInventoryItem(InventoryItem item)
        {
            try
            {
                InventoryItem toRemove;
                return InventoryItems.TryRemove(item.GetHashCode(), out toRemove);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        private void AddInventoryItem(InventoryItem item)
        {
            InventoryItems[item.GetHashCode()] = item;
        }

        public void MergeWith(GetInventoryResponse update)
        {
            var delta = update.InventoryDelta;

            if (delta?.InventoryItems == null || delta.InventoryItems.All(i => i == null))
            {
                return;
            }

            foreach(var item in delta.InventoryItems.Where(x => x != null))
            {
                AddInventoryItem(item);
            }

            //remove delete items
            foreach (var deletedItem in delta.InventoryItems.Where(p => p != null && p.DeletedItem != null))
            {
                var pokemon = InventoryItems.FirstOrDefault(kvp => kvp.Value.InventoryItemData?.PokemonData?.Id == deletedItem.DeletedItem.PokemonId).Value;
                RemoveInventoryItem(pokemon);
                RemoveInventoryItem(deletedItem);
            }

            // Only keep the newest ones
            foreach (var deltaItem in delta.InventoryItems.Where(d => d?.InventoryItemData != null))
            {
                var oldItems = new List<KeyValuePair<int, InventoryItem>>();

                if (deltaItem.InventoryItemData.PlayerStats != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.PlayerStats != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PlayerCurrency != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.PlayerCurrency != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PlayerCamera != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.PlayerCamera != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.InventoryUpgrades != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.InventoryUpgrades != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.PokedexEntry != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            kvp =>
                                kvp.Value.InventoryItemData?.PokedexEntry != null &&
                                kvp.Value.InventoryItemData.PokedexEntry.PokemonId ==
                                deltaItem.InventoryItemData.PokedexEntry.PokemonId)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.Candy != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            kvp =>
                                kvp.Value.InventoryItemData?.Candy != null &&
                                kvp.Value.InventoryItemData.Candy.FamilyId ==
                                deltaItem.InventoryItemData.Candy.FamilyId)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.Item != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            kvp =>
                                kvp.Value.InventoryItemData?.Item != null &&
                                kvp.Value.InventoryItemData.Item.ItemId == deltaItem.InventoryItemData.Item.ItemId)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }

                if (deltaItem.InventoryItemData.PokemonData != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(
                            kvp =>
                                kvp.Value.InventoryItemData?.PokemonData != null &&
                                kvp.Value.InventoryItemData.PokemonData.Id == deltaItem.InventoryItemData.PokemonData.Id)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }

                if (deltaItem.InventoryItemData.AppliedItems != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.AppliedItems != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }
                if (deltaItem.InventoryItemData.EggIncubators != null)
                {
                    oldItems.AddRange(
                        InventoryItems.Where(kvp => kvp.Value.InventoryItemData?.EggIncubators != null)
                            .OrderByDescending(kvp => kvp.Value.ModifiedTimestampMs)
                            .Skip(1));
                }

                foreach (var oldItem in oldItems)
                {
                    RemoveInventoryItem(oldItem.Value);
                }
            }

            OnInventoryUpdated?.Invoke();
        }

        internal void RemoveInventoryItems(IEnumerable<KeyValuePair<int, InventoryItem>> items)
        {   
            foreach (var item in items)
            {
                RemoveInventoryItem(item.Value);
            }
        }

        public IEnumerable<PokemonData> GetPokemons()
        {
            return InventoryItems
                .Select(kvp => kvp.Value.InventoryItemData?.PokemonData)
                .Where(p => p != null && p.PokemonId > 0);
        }

        public PokemonData GetPokemon(ulong pokemonId)
        {
            return GetPokemons().Where(p => p.Id == pokemonId).FirstOrDefault();
        }
        
        public async Task<ReleasePokemonResponse> TransferPokemon(ulong pokemonId)
        {
            if (GetPokemon(pokemonId) == null)
                return new ReleasePokemonResponse() { Result = ReleasePokemonResponse.Types.Result.Success };

            var transferPokemonRequest = new Request
            {
                RequestType = RequestType.ReleasePokemon,
                RequestMessage = ((IMessage)new ReleasePokemonMessage
                {
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(transferPokemonRequest, Client));

            Tuple<ReleasePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, ReleasePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            ReleasePokemonResponse releaseResponse = response.Item1;
            if (releaseResponse.Result == ReleasePokemonResponse.Types.Result.Success)
            {
                var pokemons = InventoryItems.Where(
                    kvp =>
                        kvp.Value != null &&
                        kvp.Value.InventoryItemData != null &&
                        kvp.Value.InventoryItemData.PokemonData != null &&
                        kvp.Value.InventoryItemData.PokemonData.Id == pokemonId);
                RemoveInventoryItems(pokemons);
            }

            return response.Item1;
        }

        public async Task<ReleasePokemonResponse> TransferPokemons(List<ulong> pokemonIds)
        {
            // Filter out all pokemons that don't exist and duplicates.
            pokemonIds = GetPokemons().Where(p => pokemonIds.Contains(p.Id)).Select(p => p.Id).Distinct().ToList();

            var message = new ReleasePokemonMessage();
            message.PokemonIds.AddRange(pokemonIds);

            var transferPokemonRequest = new Request
            {
                RequestType = RequestType.ReleasePokemon,
                RequestMessage = message.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(transferPokemonRequest, Client));

            Tuple<ReleasePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, ReleasePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            ReleasePokemonResponse releaseResponse = response.Item1;
            if (releaseResponse.Result == ReleasePokemonResponse.Types.Result.Success)
            {
                var pokemons = InventoryItems.Where(
                    kvp =>
                        kvp.Value != null &&
                        kvp.Value.InventoryItemData != null &&
                        kvp.Value.InventoryItemData.PokemonData != null &&
                        pokemonIds.Contains(kvp.Value.InventoryItemData.PokemonData.Id));
                RemoveInventoryItems(pokemons);
            }

            return response.Item1;
        }
        public async Task<EvolvePokemonResponse> EvolvePokemon(ulong pokemonId)
        {
            var evolvePokemonRequest = new Request
            {
                RequestType = RequestType.EvolvePokemon,
                RequestMessage = ((IMessage)new EvolvePokemonMessage
                {
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(evolvePokemonRequest, Client));

            Tuple<EvolvePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EvolvePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            EvolvePokemonResponse evolveResponse = response.Item1;
            if (evolveResponse.Result == EvolvePokemonResponse.Types.Result.Success)
            {
                var pokemons = InventoryItems.Where(
                    kvp =>
                        kvp.Value != null &&
                        kvp.Value.InventoryItemData != null &&
                        kvp.Value.InventoryItemData.PokemonData != null &&
                        kvp.Value.InventoryItemData.PokemonData.Id == pokemonId);
                RemoveInventoryItems(pokemons);
            }
            return response.Item1;
        }

        public async Task<UpgradePokemonResponse> UpgradePokemon(ulong pokemonId)
        {
            var upgradePokemonRequest = new Request
            {
                RequestType = RequestType.UpgradePokemon,
                RequestMessage = ((IMessage)new UpgradePokemonMessage
                {
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(upgradePokemonRequest, Client));

            Tuple<UpgradePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UpgradePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetInventoryResponse> GetInventory()
        {
            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.GetCommonRequests(Client));

            Tuple<CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item1;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item3;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return getInventoryResponse;
        }

        public async Task<RecycleInventoryItemResponse> RecycleItem(ItemId itemId, int amount)
        {
            var recycleItemRequest = new Request
            {
                RequestType = RequestType.RecycleInventoryItem,
                RequestMessage = ((IMessage)new RecycleInventoryItemMessage
                {
                    ItemId = itemId,
                    Count = amount
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(recycleItemRequest, Client));

            Tuple<RecycleInventoryItemResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, RecycleInventoryItemResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemXpBoostResponse> UseItemXpBoost()
        {
            var useItemXpBoostRequest = new Request
            {
                RequestType = RequestType.UseItemXpBoost,
                RequestMessage = ((IMessage)new UseItemXpBoostMessage
                {
                    ItemId = ItemId.ItemLuckyEgg
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemXpBoostRequest, Client));

            Tuple<UseItemXpBoostResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemXpBoostResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemEggIncubatorResponse> UseItemEggIncubator(string itemId, ulong pokemonId)
        {
            var useItemEggIncubatorRequest = new Request
            {
                RequestType = RequestType.UseItemEggIncubator,
                RequestMessage = ((IMessage)new UseItemEggIncubatorMessage
                {
                    ItemId = itemId,
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemEggIncubatorRequest, Client));

            Tuple<UseItemEggIncubatorResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemEggIncubatorResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetHatchedEggsResponse> GetHatchedEgg()
        {
            var getHatchedEggRequest = new Request
            {
                RequestType = RequestType.GetHatchedEggs,
                RequestMessage = ((IMessage)new GetHatchedEggsMessage()).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getHatchedEggRequest, Client));

            Tuple<GetHatchedEggsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetHatchedEggsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemPotionResponse> UseItemPotion(ItemId itemId, ulong pokemonId)
        {
            var useItemPotionRequest = new Request
            {
                RequestType = RequestType.UseItemPotion,
                RequestMessage = ((IMessage)new UseItemPotionMessage
                {
                    ItemId = itemId,
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemPotionRequest, Client));

            Tuple<UseItemPotionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemPotionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemReviveResponse> UseItemRevive(ItemId itemId, ulong pokemonId)
        {
            var useItemReviveRequest = new Request
            {
                RequestType = RequestType.UseItemRevive,
                RequestMessage = ((IMessage)new UseItemReviveMessage
                {
                    ItemId = itemId,
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemReviveRequest, Client));

            Tuple<UseItemReviveResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemReviveResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseIncenseResponse> UseIncense(ItemId incenseType)
        {
            var useIncenseRequest = new Request
            {
                RequestType = RequestType.UseIncense,
                RequestMessage = ((IMessage)new UseIncenseMessage
                {
                    IncenseType = incenseType
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useIncenseRequest, Client));

            Tuple<UseIncenseResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseIncenseResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemGymResponse> UseItemInGym(string gymId, ItemId itemId)
        {
            var useItemInGymRequest = new Request
            {
                RequestType = RequestType.UseItemGym,
                RequestMessage = ((IMessage)new UseItemGymMessage
                {
                    ItemId = itemId,
                    GymId = gymId,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemInGymRequest, Client));

            Tuple<UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<NicknamePokemonResponse> NicknamePokemon(ulong pokemonId, string nickName)
        {
            var nicknamePokemonRequest = new Request
            {
                RequestType = RequestType.NicknamePokemon,
                RequestMessage = ((IMessage)new NicknamePokemonMessage
                {
                    PokemonId = pokemonId,
                    Nickname = nickName
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(nicknamePokemonRequest, Client));

            Tuple<NicknamePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, NicknamePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetFavoritePokemonResponse> SetFavoritePokemon(ulong pokemonId, bool isFavorite)
        {
            var setFavoritePokemonRequest = new Request
            {
                RequestType = RequestType.SetFavoritePokemon,
                RequestMessage = ((IMessage)new SetFavoritePokemonMessage
                {
                    PokemonId = (long)pokemonId,
                    IsFavorite = isFavorite
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setFavoritePokemonRequest, Client));

            Tuple<SetFavoritePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetFavoritePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}