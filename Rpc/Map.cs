#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using GeoCoordinatePortable;
using System.Linq;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Map : BaseRpc
    {
        internal GetMapObjectsResponse LastGetMapObjectResponse;
        internal DateTime LastRpcMapObjectsRequest { get; private set; }
        internal GeoCoordinate LastGeoCoordinateMapObjectsRequest { get; private set; }
        
        public Map(Client client) : base(client)
        {
        }

        private bool CanRefreshMap()
        {
            var minSeconds = Client.GlobalSettings.MapSettings.GetMapObjectsMinRefreshSeconds;
            var maxSeconds = Client.GlobalSettings.MapSettings.GetMapObjectsMaxRefreshSeconds;
            var minDistance = Client.GlobalSettings.MapSettings.GetMapObjectsMinDistanceMeters;
            var lastGeoCoordinate = LastGeoCoordinateMapObjectsRequest;
            var secondsSinceLast = DateTime.UtcNow.Subtract(LastRpcMapObjectsRequest).Seconds;

            if (lastGeoCoordinate == null)
            {
                return true;
            }
            else if (secondsSinceLast >= minSeconds)
            {
                var metersMoved = new GeoCoordinate(Client.CurrentLatitude, Client.CurrentLongitude).GetDistanceTo(lastGeoCoordinate);
                if (secondsSinceLast >= maxSeconds)
                {
                    return true;
                }
                else if (metersMoved >= minDistance)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<GetMapObjectsResponse> GetMapObjects()
        {
            if (!CanRefreshMap())
            {
                // If we cannot refresh the map, return the cached response.
                return LastGetMapObjectResponse;
            }

            var lat = Client.CurrentLatitude;
            var lon = Client.CurrentLongitude;

            var getMapObjectsMessage = new GetMapObjectsMessage
            {
                CellId = {S2Helper.GetNearbyCellIds(lon, lat)},
                SinceTimestampMs = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                Latitude = lat,
                Longitude = lon
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

            // Only cache good responses
            if (response.Item1.MapCells.Count > 0 && 
                (response.Item1.MapCells.Where(x => x.Forts.Count > 0).Count() > 0 ||
                 response.Item1.MapCells.Where(x => x.NearbyPokemons.Count > 0).Count() > 0 ||
                 response.Item1.MapCells.Where(x => x.WildPokemons.Count > 0).Count() > 0))
            {
                // Good map response since we got at least a fort or pokemon in our cells.
                LastGetMapObjectResponse = response.Item1;
                LastGeoCoordinateMapObjectsRequest = new GeoCoordinate(lat, lon);
                LastRpcMapObjectsRequest = new DateTime();
            }
            
            return LastGetMapObjectResponse;
        }

        public async Task<GetIncensePokemonResponse> GetIncensePokemons()
        {
            var getIncensePokemonsRequest = new Request
            {
                RequestType = RequestType.GetIncensePokemon,
                RequestMessage = ((IMessage)new GetIncensePokemonMessage
                {
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getIncensePokemonsRequest, Client));

            Tuple<GetIncensePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetIncensePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
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