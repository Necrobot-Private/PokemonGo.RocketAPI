using Google.Common.Geometry;
using POGOProtos.Map;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.GymModels;
using PokemonGo.RocketAPI.Api.MapModels.FortModels;
using PokemonGo.RocketAPI.Api.MapModels.PokemonModels;
using PokemonGo.RocketAPI.Api.Util;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.MapModels
{
    public class Map : BaseRpc
    {
    	private MapObjects cachedMapObjects;
        private List<CatchablePokemon> cachedCatchable = new List<CatchablePokemon>();
        private int cellWidth = 3;
        private long lastMapUpdate;

        /**
         * Instantiates a new Map.
         *
         * @param api the api
         */
        public Map(Client client) : base(client)
        {
            cachedMapObjects = new MapObjects(client);
            lastMapUpdate = 0;
        }

        /**
         * Returns a list of catchable pokemon around the current location.
         *
         * @return a List of CatchablePokemon at your current location
         */
        public async Task<List<CatchablePokemon>> GetCatchablePokemon()
        {

            if (UseCache() && cachedCatchable.Count > 0)
            {
                return cachedCatchable;
            }


            List<ulong> cellIds = GetDefaultCells();
            MapObjects mapObjects = await GetMapObjects(cellIds);
            HashSet<CatchablePokemon> catchablePokemons = new HashSet<CatchablePokemon>();
            foreach (MapPokemon mapPokemon in mapObjects.CatchablePokemons)
            {
                catchablePokemons.Add(new CatchablePokemon(Client, mapPokemon));
            }

            foreach (WildPokemon wildPokemon in mapObjects.WildPokemons)
            {
                catchablePokemons.Add(new CatchablePokemon(Client, wildPokemon));
            }

            foreach (Pokestop pokestop in mapObjects.Pokestops)
            {
                if (pokestop.InRangeForLuredPokemon() && pokestop.FortData.LureInfo != null)
                {
                    catchablePokemons.Add(new CatchablePokemon(Client, pokestop.FortData));
                }
            }
            cachedCatchable.Clear();
            cachedCatchable.AddRange(catchablePokemons);
            return cachedCatchable;
        }
	

        /**
         * Remove a catchable pokemon from the cache
         *
         * @param pokemon the catchable pokemon
         */
        public void RemoveCatchable(CatchablePokemon pokemon)
        {
            if (cachedCatchable.Count > 0)
            {
                cachedCatchable.Remove(pokemon);
            }
        }
        
	    /**
	     * Gets catchable pokemon sort by distance.
	     *
	     * @return the catchable pokemon sort
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
	    public async Task<Dictionary<double, CatchablePokemon>> GetCatchablePokemonSort()
        {
		    MapUtil<CatchablePokemon> util = new MapUtil<CatchablePokemon>();
		    return util.sortItems((await GetCatchablePokemon()), Client);
	    }

	    /**
	     * Returns a list of nearby pokemon (non-catchable).
	     *
	     * @return a List of NearbyPokemon at your current location
	     */
	    public async Task<List<PokemonModels.NearbyPokemon>> GetNearbyPokemon()
        {
            MapObjects result = await GetMapObjects(GetDefaultCells());

            List<PokemonModels.NearbyPokemon> pokemons = new List<PokemonModels.NearbyPokemon>();
            foreach (POGOProtos.Map.Pokemon.NearbyPokemon pokemon in result.NearbyPokemons)
            {
                pokemons.Add(new PokemonModels.NearbyPokemon(pokemon));
            }

            return pokemons;
        }
        
	    /**
	     * Returns a list of spawn points.
	     *
	     * @return list of spawn points
	     */
	    public async Task<List<Point>> GetSpawnPoints()
        {
            MapObjects result = await GetMapObjects(GetDefaultCells());
            List<Point> points = new List<Point>();

            foreach (POGOProtos.Map.SpawnPoint point in result.SpawnPoints)
            {
                points.Add(new Point(point));
            }

            return points;
    	}
        

	    /**
	     * Get a list of gyms near the current location.
	     *
	     * @return List of gyms
	     */
	    public async Task<List<Gym>> GetGyms()
        {
            MapObjects result = await GetMapObjects(GetDefaultCells());
            
            List<Gym> gyms = new List<Gym>();

            foreach (FortData fortdata in result.Gyms)
            {
                gyms.Add(new Gym(Client, fortdata));
            }

            return gyms;
	    }
        

	    /**
	     * Gets gym sort by distance.
	     *
	     * @return the gym sort
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
	    public async Task<Dictionary<double, Gym>> GetGymSort()
        {
		    MapUtil<Gym> util = new MapUtil<Gym>();
		    return util.sortItems(await GetGyms(), Client);
	    }

	    /**
	     * Returns a list of decimated spawn points at current location.
	     *
	     * @return list of spawn points
	     */
	    public async Task<List<Point>> GetDecimatedSpawnPoints()
        {
            MapObjects result = await GetMapObjects(GetDefaultCells());
            
            List<Point> points = new List<Point>();
            foreach (POGOProtos.Map.SpawnPoint point in result.DecimatedSpawnPoints)
            {
                points.Add(new Point(point));
            }

            return points;
	    }

	    /**
	     * Gets decimated spawn points sort by distance.
	     *
	     * @return the decimated spawn points sort
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
	    public async Task<Dictionary<double, Point>> GetDecimatedSpawnPointsSort()
        {
		    MapUtil<Point> util = new MapUtil<Point>();
		    return util.sortItems(await GetDecimatedSpawnPoints(), Client);
	    }

	    /**
	     * Returns MapObjects around your current location.
	     *
	     * @return MapObjects at your current location
	     */
	    public async Task<MapObjects> GetMapObjects()
        {
            return await GetMapObjects(GetDefaultCells());
        }

        /**
         * Returns MapObjects around your current location within a given width.
         *
         * @param width width
         * @return MapObjects at your current location
         */
        public async Task<MapObjects> GetMapObjects(int width)
        {
            return await GetMapObjects(GetCellIds(Client.CurrentLatitude, Client.CurrentLongitude, width));
        }

        /**
         * Returns the cells requested.
         *
         * @param cellIds List of cellId
         * @return MapObjects in the given cells
         */
        public async Task<MapObjects> GetMapObjects(List<ulong> cellIds)
        {
            if (UseCache() && cachedCatchable.Count > 0)
            {
                return cachedMapObjects;
            }

            lastMapUpdate = Utils.GetTime(true);
            var getMapObjectsMessage = new GetMapObjectsMessage
            {
                Latitude = Client.CurrentLatitude,
                Longitude = Client.CurrentLongitude
            };

            int index = 0;
            foreach (ulong cellId in cellIds)
            {
                getMapObjectsMessage.CellId.Add(cellId);
                getMapObjectsMessage.SinceTimestampMs.Add(0);
                index++;
            }

            GetMapObjectsResponse response = await PostProtoPayload<Request, GetMapObjectsResponse>(RequestType.GetMapObjects, getMapObjectsMessage);
            
            MapObjects result = new MapObjects(Client);
            cachedMapObjects = result;
            foreach (MapCell mapCell in response.MapCells)
            {
                result.AddNearbyPokemons(mapCell.NearbyPokemons);
                result.AddCatchablePokemons(mapCell.CatchablePokemons);
                result.AddWildPokemons(mapCell.WildPokemons);
                result.AddDecimatedSpawnPoints(mapCell.DecimatedSpawnPoints);
                result.AddSpawnPoints(mapCell.SpawnPoints);

                Dictionary<FortType, List<FortData>> groupedForts = new Dictionary<FortType, List<FortData>>();
                foreach (FortData fort in mapCell.Forts)
                {
                    var fortType = fort.Type;
                    if (groupedForts[fortType] == null)
                        groupedForts[fortType] = new List<FortData>();

                    groupedForts[fortType].Add(fort);
                }
                
                result.AddGyms(groupedForts[FortType.Gym]);
                result.AddPokestops(groupedForts[FortType.Checkpoint]);
            }

            cachedCatchable.Clear();
            return result;
        }
	
	    /**
	     * Get a list of all the Cell Ids.
	     *
	     * @param latitude  latitude
	     * @param longitude longitude
	     * @param width     width
	     * @return List of Cells
	     */
	    public List<ulong> GetCellIds(double latitude, double longitude, int width)
        {
            S2LatLng latLng = S2LatLng.FromDegrees(latitude, longitude);
            S2CellId cellId = S2CellId.FromLatLng(latLng).ParentForLevel(15);

            int index = 0;
            int jindex = 0;
            int? orientation = null;

            int level = cellId.Level;
            int maxLevel = 30; // S2CellId.MaxLevel is internal.
            int size = 1 << (maxLevel - level);
            int face = cellId.ToFaceIjOrientation(ref index, ref jindex, ref orientation);

            List<ulong> cells = new List<ulong>();

            int halfWidth = (int)Math.Floor((double)width / 2);
            for (int x = -halfWidth; x <= halfWidth; x++)
            {
                for (int y = -halfWidth; y <= halfWidth; y++)
                {
                    cells.Add(S2CellId.FromFaceIj(face, index + x * size, jindex + y * size).ParentForLevel(15).Id);
                }
            }
            return cells;
        }
        
        public void SetDefaultWidth(int width)
        {
            cellWidth = width;
        }

        /**
         * Wether or not to Get a fresh copy or use cache;
         *
         * @return true if enough time has elapsed since the last request, false otherwise
         */
        private bool UseCache()
        {
            return (Utils.GetTime(true) - lastMapUpdate) < Client.ApiSettings.MapSettings.MinRefresh;
        }

        /**
         * Clear map objects cache
         *
         */
        public void ClearCache()
        {
            cachedCatchable.Clear();
            cachedMapObjects.NearbyPokemons.Clear();
            cachedMapObjects.CatchablePokemons.Clear();
            cachedMapObjects.WildPokemons.Clear();
            cachedMapObjects.DecimatedSpawnPoints.Clear();
            cachedMapObjects.SpawnPoints.Clear();
        }

        private List<ulong> GetDefaultCells()
        {
            return GetCellIds(Client.CurrentLatitude, Client.CurrentLongitude, cellWidth);
        }
    }

}
