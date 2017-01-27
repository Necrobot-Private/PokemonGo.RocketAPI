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

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Inventory : BaseRpc
    {
        public Inventory(Client client) : base(client)
        {
        }

        public async Task<ReleasePokemonResponse> TransferPokemon(ulong pokemonId)
        {
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

            return response.Item1;

        }
        public async Task<ReleasePokemonResponse> TransferPokemons(List<ulong> pokemonIds)
        {
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