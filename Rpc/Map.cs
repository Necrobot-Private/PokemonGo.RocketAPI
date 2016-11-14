#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Map : BaseRpc
    {
        public Map(Client client) : base(client)
        {
        }

        public async
            Task
                <
                    Tuple
                        <GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse,
                            DownloadSettingsResponse, GetBuddyWalkedResponse>> GetMapObjects()
        {
            var getMapObjectsMessage = new GetMapObjectsMessage
            {
                CellId = {S2Helper.GetNearbyCellIds(Client.CurrentLongitude, Client.CurrentLatitude)},
                SinceTimestampMs = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                Latitude = Client.CurrentLatitude,
                Longitude = Client.CurrentLongitude
            };
            
            var getMapObjectsRequest = new Request
            {
                RequestType = RequestType.GetMapObjects,
                RequestMessage = getMapObjectsMessage.ToByteString()
            };
            
            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getMapObjectsRequest, Client));

            Tuple<GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);
            
            return response;
        }

        public async Task<GetIncensePokemonResponse> GetIncensePokemons()
        {
            var message = new GetIncensePokemonMessage
            {
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude
            };

            return await PostProtoPayload<Request, GetIncensePokemonResponse>(RequestType.GetIncensePokemon, message);
        }
    }
}