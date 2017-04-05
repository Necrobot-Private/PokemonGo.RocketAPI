#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using System.Linq;
using GeoCoordinatePortable;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Map : BaseRpc
    {
        public GetMapObjectsResponse LastGetMapObjectResponse;
        internal long LastRpcMapObjectsRequestMs { get; private set; }
        internal GeoCoordinate LastGeoCoordinateMapObjectsRequest { get; private set; }

        public Map(Client client) : base(client)
        {
        }

        private int GetMilisSecondUntilRefreshMapAvail()
        {
            var minSeconds =  Client.GlobalSettings.MapSettings.GetMapObjectsMinRefreshSeconds;
            var lastGeoCoordinate = LastGeoCoordinateMapObjectsRequest;
            var secondsSinceLast = Util.TimeUtil.GetCurrentTimestampInMilliseconds() - LastRpcMapObjectsRequestMs;

            //if (lastGeoCoordinate == null)
            //{
            //    return 0;
            //}
            if (secondsSinceLast > minSeconds * 1000) return 0;

            int waitTime = (int)Math.Max(minSeconds*1000- secondsSinceLast, 0);

            return waitTime;
        }
        private bool CanRefreshMap()
        {
            var minSeconds = Client.GlobalSettings.MapSettings.GetMapObjectsMinRefreshSeconds;
            var maxSeconds = Client.GlobalSettings.MapSettings.GetMapObjectsMaxRefreshSeconds;
            var minDistance = Client.GlobalSettings.MapSettings.GetMapObjectsMinDistanceMeters;
            var lastGeoCoordinate = LastGeoCoordinateMapObjectsRequest;
            var secondsSinceLast = (Util.TimeUtil.GetCurrentTimestampInMilliseconds() - LastRpcMapObjectsRequestMs) * 1000;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force">For thread wait until next api call available to use</param>
        /// <param name="updateCache">Allow update cache, in some case we don't want update cache, snipe pokemon is an example</param>
        /// <returns></returns>
        public async Task<GetMapObjectsResponse> GetMapObjects(bool force = false, bool updateCache=true)
        {
            if (force)
            {
                var t = GetMilisSecondUntilRefreshMapAvail();
                //wait until get map available
                if (t > 0)
                {
                    await Task.Delay(t).ConfigureAwait(false);
                }
            }
            if (!CanRefreshMap())
            {
                return force ? await GetMapObjects(force, updateCache).ConfigureAwait(false) : LastGetMapObjectResponse;
                // If we cannot refresh the map, return the cached response.
            }

            var lat = Client.CurrentLatitude;
            var lon = Client.CurrentLongitude;

            var getMapObjectsMessage = new GetMapObjectsMessage
            {
                CellId = { S2Helper.GetNearbyCellIds(lon, lat) },
                SinceTimestampMs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                Latitude = lat,
                Longitude = lon
            };

            var getMapObjectsRequest = new Request
            {
                RequestType = RequestType.GetMapObjects,
                RequestMessage = getMapObjectsMessage.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getMapObjectsRequest, Client)).ConfigureAwait(false);

            Tuple<GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            LastRpcMapObjectsRequestMs = Util.TimeUtil.GetCurrentTimestampInMilliseconds();

            var numCells = response.Item1.MapCells.Count;
            var numCellsWithForts = response.Item1.MapCells.Count(x => x.Forts.Count > 0);
            var numCellsWithNearbyPokemon = response.Item1.MapCells.Count(x => x.NearbyPokemons.Count > 0);
            var numCellsWithWildPokemon = response.Item1.MapCells.Count(x => x.WildPokemons.Count > 0);

            // Only cache good responses
            if (updateCache &&
                numCells > 0 &&
                (numCellsWithForts > 0 ||
                numCellsWithNearbyPokemon > 0 ||
                numCellsWithWildPokemon > 0))
            {
                // Good map response since we got at least a fort or pokemon in our cells.
                LastGetMapObjectResponse = response.Item1;
                LastGeoCoordinateMapObjectsRequest = new GeoCoordinate(lat, lon);
            }

            if (updateCache && LastGetMapObjectResponse == null)
            {
                LastGetMapObjectResponse = response.Item1;
                
            }

            return response.Item1;
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

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getIncensePokemonsRequest, Client)).ConfigureAwait(false);

            Tuple<GetIncensePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetIncensePokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
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