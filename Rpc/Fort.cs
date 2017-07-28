#region using directives

using System.Collections.Generic;
using System.Threading.Tasks;
using POGOProtos.Data.Battle;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using System;
using System.Linq;
using POGOProtos.Map;
using PokemonGo.RocketAPI.Extensions;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Fort : BaseRpc
    {
        public Fort(Client client) : base(client)
        {
        }

        public async Task<FortDetailsResponse> GetFort(string fortId, double fortLatitude, double fortLongitude)
        {
            var getFortRequest = new Request
            {
                RequestType = RequestType.FortDetails,
                RequestMessage = ((IMessage)new FortDetailsMessage
                {
                    FortId = fortId,
                    Latitude = fortLatitude,
                    Longitude = fortLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getFortRequest, Client)).ConfigureAwait(false);

            Tuple<FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<FortSearchResponse> SearchFort(string fortId, double fortLat, double fortLng)
        {
            var searchFortRequest = new Request
            {
                RequestType = RequestType.FortSearch,
                RequestMessage = ((IMessage)new FortSearchMessage
                {
                    FortId = fortId,
                    FortLatitude = fortLat,
                    FortLongitude = fortLng,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(searchFortRequest, Client)).ConfigureAwait(false);

            Tuple<FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            // Update LastMapObject to mark fort as being used
            foreach (MapCell mapCell in Client.Map.LastGetMapObjectResponse.MapCells)
            {
                var mapFort = mapCell.Forts.Where(x => x.Id == fortId).FirstOrDefault();
                if (mapFort != null && mapFort.Type == POGOProtos.Map.Fort.FortType.Checkpoint)
                {
                    mapFort.CooldownCompleteTimestampMs = DateTime.UtcNow.ToUnixTime() + 5 * 60 * 1000; // Cooldown is 5 minutes.
                    break;
                }
            }
            return response.Item1;
        }

        public async Task<AddFortModifierResponse> AddFortModifier(string fortId, ItemId modifierType)
        {
            var addFortModifierRequest = new Request
            {
                RequestType = RequestType.AddFortModifier,
                RequestMessage = ((IMessage)new AddFortModifierMessage
                {
                    FortId = fortId,
                    ModifierType = modifierType,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(addFortModifierRequest, Client)).ConfigureAwait(false);

            Tuple<AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //TODO: revise https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L36
        public async Task<AttackGymResponse> AttackGym(string fortId, string battleId, List<BattleAction> battleActions,
            BattleAction lastRetrievedAction)
        {
            var message = new AttackGymMessage
            {
                BattleId = battleId,
                GymId = fortId,
                LastRetrievedAction = lastRetrievedAction,
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                
                AttackActions = { } // {battleActions}    ,
            };

            message.AttackActions.AddRange(battleActions);
            
            var attackGymRequest = new Request
            {
                RequestType = RequestType.AttackGym,
                RequestMessage = message.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(attackGymRequest, Client)).ConfigureAwait(false);

            Tuple<AttackGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, AttackGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L20
        public async Task<FortDeployPokemonResponse> FortDeployPokemon(string fortId, ulong pokemonId)
        {
            var fortDeployPokemonRequest = new Request
            {
                RequestType = RequestType.FortDeployPokemon,
                RequestMessage = ((IMessage)new FortDeployPokemonMessage
                {
                    PokemonId = pokemonId,
                    FortId = fortId,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(fortDeployPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<FortRecallPokemonResponse> FortRecallPokemon(string fortId, ulong pokemonId)
        {
            var fortRecallPokemonRequest = new Request
            {
                RequestType = RequestType.FortRecallPokemon,
                RequestMessage = ((IMessage)new FortRecallPokemonMessage
                {
                    PokemonId = pokemonId,
                    FortId = fortId,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(fortRecallPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //Renamed to GymGetInfo https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L34
        public async Task<GymGetInfoResponse> GymGetInfo(string gymId, double gymLat, double gymLng)
        {
            var getGymDetailsRequest = new Request
            {
                RequestType = RequestType.GymGetInfo,
                RequestMessage = ((IMessage)new GymGetInfoMessage
                {
                    GymId = gymId,
                    PlayerLngDegrees = Client.CurrentLongitude,
                    PlayerLatDegrees = Client.CurrentLatitude,
                    GymLatDegrees = gymLat,
                    GymLngDegrees = gymLng,
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getGymDetailsRequest, Client)).ConfigureAwait(false);

            Tuple<GymGetInfoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymGetInfoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //TODO: revise https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L35
        public async Task<StartGymBattleResponse> StartGymBattle(string gymId, ulong defendingPokemonId,
            IEnumerable<ulong> attackingPokemonIds)
        {
            var startGymBattleRequest = new Request
            {
                RequestType = RequestType.StartGymBattle,
                RequestMessage = ((IMessage)new StartGymBattleMessage
                {
                    GymId = gymId,
                    DefendingPokemonId = defendingPokemonId,
                    AttackingPokemonIds = { attackingPokemonIds },
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(startGymBattleRequest, Client)).ConfigureAwait(false);

            Tuple<StartGymBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, StartGymBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L64
        public async Task<GymFeedPokemonResponse> GymFeedPokemon(string gymId, ItemId item, ulong pokemonId, int startingQuantity = 1)
        {
            var setFavoritePokemonRequest = new Request
            {
                RequestType = RequestType.GymFeedPokemon,
                RequestMessage = ((IMessage)new GymFeedPokemonMessage
                {
                    GymId = gymId,
                    Item = item,
                    PokemonId = pokemonId,
                    PlayerLatDegrees = Client.CurrentLatitude,
                    PlayerLngDegrees = Client.CurrentLongitude,
                    StartingQuantity = startingQuantity
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setFavoritePokemonRequest, Client)).ConfigureAwait(false);

            Tuple<GymFeedPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymFeedPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //TODO: revise https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L33
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

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useItemInGymRequest, Client)).ConfigureAwait(false);

            Tuple<UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L87
        public async Task<GetGymBadgeDetailsResponse> GetGymBadgeDetails(string fortId, double latitude, double longitude)
        {
            var setFavoritePokemonRequest = new Request
            {
                RequestType = RequestType.GetGymBadgeDetails,
                RequestMessage = ((IMessage)new GetGymBadgeDetailsMessage
                {
                    FortId = fortId,
                    Latitude = latitude,
                    Longitude = longitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setFavoritePokemonRequest, Client)).ConfigureAwait(false);

            Tuple<GetGymBadgeDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetGymBadgeDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L55
        public async Task<GymDeployResponse> GymDeploy(string fortId, ulong pokemonId)
        {
            var setFavoritePokemonRequest = new Request
            {
                RequestType = RequestType.GymDeploy,
                RequestMessage = ((IMessage)new GymDeployMessage
                {
                    FortId = fortId,
                    PokemonId = pokemonId,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setFavoritePokemonRequest, Client)).ConfigureAwait(false);

            Tuple<GymDeployResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymDeployResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

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