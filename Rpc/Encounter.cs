#region using directives

using System.Threading.Tasks;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using System;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Encounter : BaseRpc
    {
        public Encounter(Client client) : base(client)
        {
        }

        public async Task<EncounterResponse> EncounterPokemon(ulong encounterId, string spawnPointGuid)
        {
            var encounterPokemonRequest = new Request
            {
                RequestType = RequestType.Encounter,
                RequestMessage = ((IMessage)new EncounterMessage
                {
                    EncounterId = encounterId,
                    SpawnPointId = spawnPointGuid,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(encounterPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<EncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UseItemCaptureResponse> UseCaptureItem(ulong encounterId, ItemId itemId, string spawnPointId)
        {
            var useCaptureItemRequest = new Request
            {
                RequestType = RequestType.UseItemCapture,
                RequestMessage = ((IMessage)new UseItemCaptureMessage
                {
                    EncounterId = encounterId,
                    ItemId = itemId,
                    SpawnPointId = spawnPointId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useCaptureItemRequest, Client)).ConfigureAwait(false);

            Tuple<UseItemCaptureResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemCaptureResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }


        public async Task<UseItemEncounterResponse> UseItemEncounter(ulong encounterId, ItemId itemId, string spawnPointId)
        {
            var useCaptureItemRequest = new Request
            {
                RequestType = RequestType.UseItemEncounter,
                RequestMessage = ((IMessage)new UseItemEncounterMessage
                {
                    EncounterId = encounterId,
                    Item = itemId,
                    SpawnPointGuid = spawnPointId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(useCaptureItemRequest, Client)).ConfigureAwait(false);

            Tuple<UseItemEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UseItemEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<CatchPokemonResponse> CatchPokemon(ulong encounterId, string spawnPointGuid,
            ItemId pokeballItemId, double normalizedRecticleSize = 1.950, double spinModifier = 1,
            bool hitPokemon = true, double normalizedHitPos = 1)
        {
            var catchPokemonRequest = new Request
            {
                RequestType = RequestType.CatchPokemon,
                RequestMessage = ((IMessage)new CatchPokemonMessage
                {
                    EncounterId = encounterId,
                    Pokeball = pokeballItemId,
                    SpawnPointId = spawnPointGuid,
                    HitPokemon = hitPokemon,
                    NormalizedReticleSize = normalizedRecticleSize,
                    SpinModifier = spinModifier,
                    NormalizedHitPosition = normalizedHitPos
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(catchPokemonRequest, Client)).ConfigureAwait(false);

            Tuple<CatchPokemonResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CatchPokemonResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<IncenseEncounterResponse> EncounterIncensePokemon(ulong encounterId, string encounterLocation)
        {
            var encounterIncensePokemonRequest = new Request
            {
                RequestType = RequestType.IncenseEncounter,
                RequestMessage = ((IMessage)new IncenseEncounterMessage
                {
                    EncounterId = encounterId,
                    EncounterLocation = encounterLocation
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(encounterIncensePokemonRequest, Client)).ConfigureAwait(false);

            Tuple<IncenseEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, IncenseEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<DiskEncounterResponse> EncounterLurePokemon(ulong encounterId, string fortId)
        {
            var encounterLurePokemonRequest = new Request
            {
                RequestType = RequestType.DiskEncounter,
                RequestMessage = ((IMessage)new DiskEncounterMessage
                {
                    EncounterId = encounterId,
                    FortId = fortId,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(encounterLurePokemonRequest, Client)).ConfigureAwait(false);

            Tuple<DiskEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, DiskEncounterResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<EncounterTutorialCompleteResponse> EncounterTutorialComplete(PokemonId pokemonId)
        {
            var encounterTutorialCompleteRequest = new Request
            {
                RequestType = RequestType.EncounterTutorialComplete,
                RequestMessage = ((IMessage)new EncounterTutorialCompleteMessage
                {
                    PokemonId = pokemonId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(encounterTutorialCompleteRequest, Client)).ConfigureAwait(false);

            Tuple<EncounterTutorialCompleteResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EncounterTutorialCompleteResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}