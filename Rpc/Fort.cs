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
using Google.Protobuf.Collections;

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

            Tuple<FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GymBattleAttackResponse> GymBattleAttak(string fortId, string battleId, IEnumerable<BattleAction> battleActions,
            BattleAction lastRetrievedAction, long timestampMs)
        {
            var message = new GymBattleAttackMessage
            {
                BattleId = battleId,
                GymId = fortId,
                LastRetrievedAction = lastRetrievedAction,
                PlayerLatDegrees = Client.CurrentLatitude,
                PlayerLngDegrees = Client.CurrentLongitude,
                TimestampMs = timestampMs,
                AttackerActions = { } // { battleActions }
            };

            message.AttackerActions.AddRange(battleActions);

            var gymBattleAttackGymRequest = new Request
            {
                RequestType = RequestType.GymBattleAttack,
                RequestMessage = message.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(gymBattleAttackGymRequest, Client)).ConfigureAwait(false);

            Tuple<GymBattleAttackResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymBattleAttackResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<GymGetInfoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymGetInfoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GymStartSessionResponse> GymStartSession(string gymId, ulong defendingPokemonId,
            IEnumerable<ulong> attackingPokemonIds)
        {
            var startGymBattleRequest = new Request
            {
                RequestType = RequestType.GymStartSession,
                RequestMessage = ((IMessage)new GymStartSessionMessage
                {
                    GymId = gymId,
                    DefendingPokemonId = defendingPokemonId,
                    AttackingPokemonId = { attackingPokemonIds },
                    PlayerLatDegrees = Client.CurrentLatitude,
                    PlayerLngDegrees = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(startGymBattleRequest, Client)).ConfigureAwait(false);

            Tuple<GymStartSessionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymStartSessionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L64
        public async Task<GymFeedPokemonResponse> GymFeedPokemon(string gymId, ItemId item, ulong pokemonId, int startingQuantity = 1)
        {
            var GymFeedPokemonRequest = new Request
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

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(GymFeedPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<GymFeedPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymFeedPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            Tuple<UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L87
        public async Task<GetGymBadgeDetailsResponse> GetGymBadgeDetails(string fortId, double latitude, double longitude)
        {
            var GetGymBadgeDetailsRequest = new Request
            {
                RequestType = RequestType.GetGymBadgeDetails,
                RequestMessage = ((IMessage)new GetGymBadgeDetailsMessage
                {
                    FortId = fortId,
                    Latitude = latitude,
                    Longitude = longitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(GetGymBadgeDetailsRequest, Client)).ConfigureAwait(false);

            Tuple<GetGymBadgeDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetGymBadgeDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L55
        public async Task<GymDeployResponse> GymDeploy(string fortId, ulong pokemonId)
        {
            var GymDeploynRequest = new Request
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

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(GymDeploynRequest, Client)).ConfigureAwait(false);

            Tuple<GymDeployResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GymDeployResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetRaidDetailsResponse> GetRaidDetails(string gymId, long raidSeed)
        {
            var setGetRaidDetailsRequest = new Request
            {
                RequestType = RequestType.GetRaidDetails,
                RequestMessage = ((IMessage)new GetRaidDetailsMessage
                {
                    GymId = gymId,
                    PlayerLatDegrees = Client.CurrentLatitude,
                    PlayerLngDegrees = Client.CurrentLongitude,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setGetRaidDetailsRequest, Client)).ConfigureAwait(false);

            Tuple<GetRaidDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetRaidDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<StartRaidBattleResponse> StartRaidBattle(string gymId, long raidSeed)
        {
            var StartRaidBattleRequest = new Request
            {
                RequestType = RequestType.StartRaidBattle,
                RequestMessage = ((IMessage)new StartRaidBattleMessage
                {
                    GymId = gymId,
                    PlayerLatDegrees = Client.CurrentLatitude,
                    PlayerLngDegrees = Client.CurrentLongitude,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(StartRaidBattleRequest, Client)).ConfigureAwait(false);

            Tuple<StartRaidBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, StartRaidBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<AttackRaidBattleResponse> AttackRaidBattle(string gymId, RepeatedField<BattleAction> attackeractions, string battleid, BattleAction lastretrievedaction, long timestampms)
        {
            var AttackRaidBattleRequest = new Request
            {
                RequestType = RequestType.AttackRaid,
                RequestMessage = ((IMessage)new AttackRaidBattleMessage
                {
                    GymId = gymId,
                    AttackerActions = { attackeractions },
                    BattleId = battleid,
                    LastRetrievedAction = lastretrievedaction,
                    TimestampMs = timestampms
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(AttackRaidBattleRequest, Client)).ConfigureAwait(false);

            Tuple<AttackRaidBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, AttackRaidBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<JoinLobbyResponse> JoinLobby(string gymId, long raidSeed, bool _private)
        {
            var AttackRaidBattleRequest = new Request
            {
                RequestType = RequestType.JoinLobby,
                RequestMessage = ((IMessage)new JoinLobbyMessage
                {
                    GymId = gymId,
                    PlayerLatDegrees = Client.CurrentLatitude,
                    PlayerLngDegrees = Client.CurrentLongitude,
                    Private = _private,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(AttackRaidBattleRequest, Client)).ConfigureAwait(false);

            Tuple<JoinLobbyResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, JoinLobbyResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<LeaveLobbyResponse> LeaveLobby(string gymId, long raidSeed)
        {
            var LeaveLobbyRequest = new Request
            {
                RequestType = RequestType.LeaveLobby,
                RequestMessage = ((IMessage)new LeaveLobbyMessage
                {
                    GymId = gymId,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(LeaveLobbyRequest, Client)).ConfigureAwait(false);

            Tuple<LeaveLobbyResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, LeaveLobbyResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetLobbyPokemonResponse> SetLobbyPokemon(string gymId, long raidSeed)
        {
            var SetLobbyPokemonRequest = new Request
            {
                RequestType = RequestType.SetLobbyPokemon,
                RequestMessage = ((IMessage)new SetLobbyPokemonMessage
                {
                    GymId = gymId,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SetLobbyPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<SetLobbyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetLobbyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetLobbyVisibilityResponse> SetLobbyVisibility(string gymId, long raidSeed)
        {
            var SetLobbyVisibilityRequest = new Request
            {
                RequestType = RequestType.SetLobbyVisibility,
                RequestMessage = ((IMessage)new SetLobbyVisibilityMessage
                {
                    GymId = gymId,
                    RaidSeed = raidSeed
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SetLobbyVisibilityRequest, Client)).ConfigureAwait(false);

            Tuple<SetLobbyVisibilityResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetLobbyVisibilityResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}
