﻿#region using directives

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

        public FortDetailsResponse GetFort(string fortId, double fortLatitude, double fortLongitude)
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getFortRequest, Client));

            Tuple<FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, FortDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public FortSearchResponse SearchFort(string fortId, double fortLat, double fortLng)
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(searchFortRequest, Client));

            Tuple<FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, FortSearchResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

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

        public AddFortModifierResponse AddFortModifier(string fortId, ItemId modifierType)
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(addFortModifierRequest, Client));

            Tuple<AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, AddFortModifierResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public AttackGymResponse AttackGym(string fortId, string battleId, List<BattleAction> battleActions,
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(attackGymRequest, Client));

            Tuple<AttackGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, AttackGymResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public FortDeployPokemonResponse FortDeployPokemon(string fortId, ulong pokemonId)
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(fortDeployPokemonRequest, Client));

            Tuple<FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, FortDeployPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public FortRecallPokemonResponse FortRecallPokemon(string fortId, ulong pokemonId)
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(fortRecallPokemonRequest, Client));

            Tuple<FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, FortRecallPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public GetGymDetailsResponse GetGymDetails(string gymId, double gymLat, double gymLng)
        {
            var getGymDetailsRequest = new Request
            {
                RequestType = RequestType.GetGymDetails,
                RequestMessage = ((IMessage)new GetGymDetailsMessage
                {
                    GymId = gymId,
                    GymLatitude = gymLat,
                    GymLongitude = gymLng,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getGymDetailsRequest, Client));

            Tuple<GetGymDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, GetGymDetailsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public StartGymBattleResponse StartGymBattle(string gymId, ulong defendingPokemonId,
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(startGymBattleRequest, Client));

            Tuple<StartGymBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, StartGymBattleResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
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