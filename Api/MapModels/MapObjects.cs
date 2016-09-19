using POGOProtos.Map;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using PokemonGo.RocketAPI.Api.MapModels.FortModels;
using PokemonGo.RocketAPI.Rpc;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.MapModels
{
    public class MapObjects : BaseRpc
    {

        public List<NearbyPokemon> NearbyPokemons = new List<NearbyPokemon>();
        public List<MapPokemon> CatchablePokemons = new List<MapPokemon>();
        public List<WildPokemon> WildPokemons = new List<WildPokemon>();
        public List<SpawnPoint> DecimatedSpawnPoints = new List<SpawnPoint>();
        public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();
        public List<FortData> Gyms = new List<FortData>();
        public List<Pokestop> Pokestops = new List<Pokestop>();
        bool complete = false;


	    /**
	     * Instantiates a new Map objects.
	     *
	     * @param api the api
	     */
	    public MapObjects(Client client) : base(client)
        {
        }

        /**
         * Add nearby pokemons.
         *
         * @param NearbyPokemons the nearby pokemons
         */
        public void AddNearbyPokemons(List<NearbyPokemon> NearbyPokemons)
        {
            if (NearbyPokemons == null || NearbyPokemons.Count == 0)
            {
                return;
            }
            complete = true;
            this.NearbyPokemons.AddRange(NearbyPokemons);
        }

        /**
         * Add catchable pokemons.
         *
         * @param CatchablePokemons the catchable pokemons
         */
        public void AddCatchablePokemons(List<MapPokemon> CatchablePokemons)
        {
            if (CatchablePokemons == null || CatchablePokemons.Count == 0)
            {
                return;
            }
            complete = true;
            this.CatchablePokemons.AddRange(CatchablePokemons);
        }

        /**
         * Add wild pokemons.
         *
         * @param WildPokemons the wild pokemons
         */
        public void AddWildPokemons(List<WildPokemon> WildPokemons)
        {
            if (WildPokemons == null || WildPokemons.Count == 0)
            {
                return;
            }
            complete = true;
            this.WildPokemons.AddRange(WildPokemons);
        }

        /**
         * Add decimated spawn points.
         *
         * @param DecimatedSpawnPoints the decimated spawn points
         */
        public void AddDecimatedSpawnPoints(List<SpawnPoint> DecimatedSpawnPoints)
        {
            if (DecimatedSpawnPoints == null || DecimatedSpawnPoints.Count == 0)
            {
                return;
            }
            complete = true;
            this.DecimatedSpawnPoints.AddRange(DecimatedSpawnPoints);
        }

        /**
         * Add spawn points.
         *
         * @param SpawnPoints the spawn points
         */
        public void AddSpawnPoints(List<SpawnPoint> SpawnPoints)
        {
            if (SpawnPoints == null || SpawnPoints.Count == 0)
            {
                return;
            }
            complete = true;
            this.SpawnPoints.AddRange(SpawnPoints);
        }

        /**
         * Add gyms.
         *
         * @param gyms the gyms
         */
        public void AddGyms(List<FortData> gyms)
        {
            if (gyms == null || gyms.Count == 0)
            {
                return;
            }
            complete = true;
            this.Gyms.AddRange(gyms);
        }

        /**
         * Add pokestops.
         *
         * @param pokestops the pokestops
         */
        public void AddPokestops(List<FortData> pokestops)
        {
            if (pokestops == null || pokestops.Count == 0)
            {
                return;
            }
            complete = true;
            foreach (FortData pokestop in pokestops)
            {
                this.Pokestops.Add(new Pokestop(Client, pokestop));
            }
        }

        /**
         * Returns whether any data was returned. When a user requests too many cells/wrong cell level/cells too far away
         * from the users location, the server returns empty MapCells.
         *
         * @return whether or not the return returned any data at all;
         */
        public bool isComplete()
        {
            return complete;
        }


        /**
         * updates the object.
         *
         * @param other Update this {@link MapObjects} data with the provided data.
         */
        //@Deprecated
        public void Update(MapObjects other)
        {

            NearbyPokemons.Clear();
            AddNearbyPokemons(other.NearbyPokemons);

            CatchablePokemons.Clear();
            AddCatchablePokemons(other.CatchablePokemons);

            WildPokemons.Clear();
            AddWildPokemons(other.WildPokemons);

            DecimatedSpawnPoints.Clear();
            AddDecimatedSpawnPoints(other.DecimatedSpawnPoints);

            SpawnPoints.Clear();
            AddSpawnPoints(other.SpawnPoints);


            /* for (FortData otherGym: other.GetGyms()) {
                Iterator<FortData> iterator = gyms.iterator();
                while (iterator.hasNext()) {
                    FortData gym = iterator.next();
                    if (otherGym.GetId().equals(gym.GetId())) {
                        gyms.remove(gym);
                        break;
                    }
                }
                gyms.add(otherGym);
            }

            /*for (Pokestop otherPokestop: other.GetPokestops()) {
                Iterator<Pokestop> iterator = pokestops.iterator();
                while (iterator.hasNext()) {
                    Pokestop pokestop = iterator.next();
                    if (otherPokestop.GetId().equals(pokestop.GetId())) {
                        pokestops.remove(pokestop);
                        break;
                    }
                }
                pokestops.add(otherPokestop);
            }*/
        }
    }
}
