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
using System.Collections.Generic;
using System.Collections.Concurrent;
using POGOProtos.Data;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using PokemonGo.RocketAPI.Util;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void OnMapUpdateHandler();

    public class Map : BaseRpc
    {
        public event OnMapUpdateHandler OnMapUpdated;

        public GetMapObjectsResponse LastGetMapObjectResponse;
        internal long LastRpcMapObjectsRequestMs { get; private set; }
        internal GeoCoordinate LastGeoCoordinateMapObjectsRequest { get; private set; }
        public ConcurrentDictionary<ulong, NearbyPokemon> NearbyPokemonCache = new ConcurrentDictionary<ulong, NearbyPokemon>();
        public ConcurrentDictionary<ulong, WildPokemon> WildPokemonCache = new ConcurrentDictionary<ulong, WildPokemon>();
        public ConcurrentDictionary<ulong, MapPokemon> CatchablePokemonsCache = new ConcurrentDictionary<ulong, MapPokemon>();
        public ConcurrentDictionary<string, FortData> FortsCache = new ConcurrentDictionary<string, FortData>();

        public Map(Client client) : base(client)
        {
        }

        public void RemovePokemonFromCache(ulong encounterId)
        {
            var nearbyPokemon = NearbyPokemonCache.FirstOrDefault(p => p.Value.EncounterId == encounterId).Value;
            NearbyPokemon toRemoveNearby;
            NearbyPokemonCache.TryRemove(encounterId, out toRemoveNearby);

            var wildPokemon = WildPokemonCache.FirstOrDefault(p => p.Value.EncounterId == encounterId).Value;
            WildPokemon toRemoveWild;
            WildPokemonCache.TryRemove(encounterId, out toRemoveWild);
            
            var catchablePokemon = CatchablePokemonsCache.FirstOrDefault(p => p.Value.EncounterId == encounterId).Value;
            MapPokemon toRemoveCatchable;
            CatchablePokemonsCache.TryRemove(encounterId, out toRemoveCatchable);
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

        public async Task GetMapObjects(bool force = false)
        {
            if (force)
            {
                var t = GetMilisSecondUntilRefreshMapAvail();
                //wait until get map available
                if (t > 0)
                {
                    await Task.Delay(t);
                }
            }
            if (!CanRefreshMap())
            {
                if (force)
                {
                    await GetMapObjects(force);
                    return;
                }
                // If we cannot refresh the map, return the cached response.
                return;
            }

            var lat = Client.CurrentLatitude;
            var lon = Client.CurrentLongitude;

            var cellIds = S2Helper.GetNearbyCellIds(lon, lat);
            var sinceTimeMs = new List<long>(cellIds.Count);
            foreach (var cellId in cellIds)
            {
                var cell = LastGetMapObjectResponse?.MapCells.FirstOrDefault(x => x.S2CellId == cellId);

                sinceTimeMs.Add(cell?.CurrentTimestampMs ?? 0);
            }

            var getMapObjectsMessage = new GetMapObjectsMessage
            {
                CellId = { cellIds },
                SinceTimestampMs = { sinceTimeMs.ToArray() },
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

            LastRpcMapObjectsRequestMs = Util.TimeUtil.GetCurrentTimestampInMilliseconds();

            GetMapObjectsResponse mapObjects = response.Item1;
            IEnumerable<WildPokemon> newWildPokemon = mapObjects.MapCells.SelectMany(c => c.WildPokemons);
            if (newWildPokemon.Count() > 0)
            {
                foreach (var p in newWildPokemon)
                {
                    WildPokemonCache.AddOrUpdate(p.PokemonData.Id, p, (k, v) =>
                    {
                        v.EncounterId = p.EncounterId;
                        v.Latitude = p.Latitude;
                        v.Longitude = p.Longitude;
                        v.LastModifiedTimestampMs = p.LastModifiedTimestampMs;
                        v.PokemonData = p.PokemonData;
                        v.SpawnPointId = p.SpawnPointId;
                        v.TimeTillHiddenMs = p.TimeTillHiddenMs;
                        return v;
                    });
                }

                // Remove pokemon not in most recent map objects
                /*
                foreach (var p in WildPokemonCache.Select(kvp => kvp.Value))
                {
                    if (newWildPokemon.Any(x => x.PokemonData.Id == p.PokemonData.Id))
                        continue;

                    WildPokemon toRemove;
                    WildPokemonCache.TryRemove(p.PokemonData.Id, out toRemove);
                }
                */
            }

            IEnumerable<NearbyPokemon> newNearbyPokemon = mapObjects.MapCells.SelectMany(c => c.NearbyPokemons);
            if (newNearbyPokemon.Count() > 0)
            {
                foreach (var p in newNearbyPokemon)
                {
                    NearbyPokemonCache.AddOrUpdate(p.EncounterId, p, (k, v) =>
                    {
                        v.DistanceInMeters = p.DistanceInMeters;
                        v.FortId = p.FortId;
                        v.FortImageUrl = p.FortImageUrl;
                        v.PokemonDisplay = p.PokemonDisplay;
                        v.PokemonId = p.PokemonId;
                        return v;
                    });
                }

                /*
                // Remove pokemon not in most recent map objects
                foreach (var p in NearbyPokemonCache.Select(kvp => kvp.Value))
                {
                    if (newNearbyPokemon.Any(x => x.EncounterId == p.EncounterId))
                        continue;

                    NearbyPokemon toRemove;
                    NearbyPokemonCache.TryRemove(p.EncounterId, out toRemove);
                }
                */
            }
            
            IEnumerable<MapPokemon> newCatchablePokemon = mapObjects.MapCells.SelectMany(c => c.CatchablePokemons);
            if (newCatchablePokemon.Count() > 0)
            {
                foreach (var p in newCatchablePokemon)
                {
                    CatchablePokemonsCache.AddOrUpdate(p.EncounterId, p, (k, v) =>
                    {
                        v.ExpirationTimestampMs = p.ExpirationTimestampMs;
                        v.Latitude = p.Latitude;
                        v.Longitude = p.Longitude;
                        v.PokemonDisplay = p.PokemonDisplay;
                        v.PokemonId = p.PokemonId;
                        v.SpawnPointId = p.SpawnPointId;
                        return v;
                    });
                }

                // Remove pokemon not in most recent map objects
                /*
                foreach (var p in CatchablePokemonsCache.Select(kvp => kvp.Value))
                {
                    if (newCatchablePokemon.Any(x => x.EncounterId == p.EncounterId))
                        continue;

                    MapPokemon toRemove;
                    CatchablePokemonsCache.TryRemove(p.EncounterId, out toRemove);
                }
                */
            }
            
            // Remove expired wild pokemon
            var expiredWildPokemon = WildPokemonCache.Where(kvp => (kvp.Value.TimeTillHiddenMs > 0 && kvp.Value.TimeTillHiddenMs <= 90000) && (kvp.Value.LastModifiedTimestampMs + kvp.Value.TimeTillHiddenMs) < TimeUtil.GetCurrentTimestampInMilliseconds());
            foreach (var kvp in expiredWildPokemon)
            {
                //WildPokemon removedPokemon;
                //WildPokemonCache.TryRemove(kvp.Key, out removedPokemon);
                RemovePokemonFromCache(kvp.Key);
            }

            // Remove expired catchable pokemon
            var expiredCatchablePokemon = CatchablePokemonsCache.Where(kvp => kvp.Value.ExpirationTimestampMs > 0 && kvp.Value.ExpirationTimestampMs < TimeUtil.GetCurrentTimestampInMilliseconds());
            foreach (var kvp in expiredCatchablePokemon)
            {
                //MapPokemon removedPokemon;
                //CatchablePokemonsCache.TryRemove(kvp.Key, out removedPokemon);
                RemovePokemonFromCache(kvp.Key);
            }

            IEnumerable<FortData> newForts = mapObjects.MapCells.SelectMany(c => c.Forts);
            foreach (var f in newForts)
            {
                if (f.Enabled)
                {
                    FortsCache.AddOrUpdate(f.Id, f, (k, v) =>
                    {
                        v.CooldownCompleteTimestampMs = f.CooldownCompleteTimestampMs;
                        v.DeployLockoutEndMs = f.DeployLockoutEndMs;
                        v.Enabled = f.Enabled;
                        v.GuardPokemonCp = f.GuardPokemonCp;
                        v.GuardPokemonDisplay = f.GuardPokemonDisplay;
                        v.GuardPokemonId = f.GuardPokemonId;
                        v.GymPoints = f.GymPoints;
                        v.IsInBattle = f.IsInBattle;
                        v.LastModifiedTimestampMs = f.LastModifiedTimestampMs;
                        v.Latitude = f.Latitude;
                        v.Longitude = f.Longitude;
                        v.LureInfo = f.LureInfo;
                        v.OwnedByTeam = f.OwnedByTeam;
                        v.RenderingType = f.RenderingType;
                        v.Sponsor = f.Sponsor;
                        v.Type = f.Type;
                        return v;
                    });
                }
            }

            // Only cache good responses
            if (mapObjects.MapCells.Count > 0 &&
                (mapObjects.MapCells.Where(x => x.Forts.Count > 0).Count() > 0 ||
                 mapObjects.MapCells.Where(x => x.NearbyPokemons.Count > 0).Count() > 0 ||
                 mapObjects.MapCells.Where(x => x.WildPokemons.Count > 0).Count() > 0))
            {
                // Good map response since we got at least a fort or pokemon in our cells.
                LastGetMapObjectResponse = mapObjects;
                LastGeoCoordinateMapObjectsRequest = new GeoCoordinate(lat, lon);
            }

            if (LastGetMapObjectResponse == null)
            {
                LastGetMapObjectResponse = mapObjects;
            }

            OnMapUpdated?.Invoke();
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