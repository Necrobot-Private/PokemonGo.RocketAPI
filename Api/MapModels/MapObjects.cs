using POGOProtos.Map;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using PokemonGo.RocketAPI.Api.MapModels.FortModels;
using PokemonGo.RocketAPI.Rpc;
using System.Collections.Concurrent;

namespace PokemonGo.RocketAPI.Api.MapModels
{
    public class MapObjects : BaseRpc
    {

        public BlockingCollection<NearbyPokemon> NearbyPokemons = new BlockingCollection<NearbyPokemon>();
        public BlockingCollection<MapPokemon> CatchablePokemons = new BlockingCollection<MapPokemon>();
        public BlockingCollection<WildPokemon> WildPokemons = new BlockingCollection<WildPokemon>();
        public BlockingCollection<SpawnPoint> DecimatedSpawnPoints = new BlockingCollection<SpawnPoint>();
        public BlockingCollection<SpawnPoint> SpawnPoints = new BlockingCollection<SpawnPoint>();
        public BlockingCollection<FortData> Gyms = new BlockingCollection<FortData>();
        public BlockingCollection<Pokestop> Pokestops = new BlockingCollection<Pokestop>();
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
        public void AddNearbyPokemons(BlockingCollection<NearbyPokemon> nearbyPokemons)
        {
            if (nearbyPokemons == null || nearbyPokemons.Count == 0)
            {
                return;
            }
            complete = true;

            foreach (NearbyPokemon pokemon in nearbyPokemons)
                this.NearbyPokemons.Add(pokemon);
        }

        /**
         * Add catchable pokemons.
         *
         * @param CatchablePokemons the catchable pokemons
         */
        public void AddCatchablePokemons(BlockingCollection<MapPokemon> catchablePokemons)
        {
            if (catchablePokemons == null || catchablePokemons.Count == 0)
            {
                return;
            }
            complete = true;

            foreach (MapPokemon pokemon in catchablePokemons)
                this.CatchablePokemons.Add(pokemon);
        }

        /**
         * Add wild pokemons.
         *
         * @param WildPokemons the wild pokemons
         */
        public void AddWildPokemons(BlockingCollection<WildPokemon> wildPokemons)
        {
            if (wildPokemons == null || wildPokemons.Count == 0)
            {
                return;
            }
            complete = true;

            foreach(WildPokemon pokemon in wildPokemons)
                this.WildPokemons.Add(pokemon);
        }

        /**
         * Add decimated spawn points.
         *
         * @param DecimatedSpawnPoints the decimated spawn points
         */
        public void AddDecimatedSpawnPoints(BlockingCollection<SpawnPoint> decimatedSpawnPoints)
        {
            if (decimatedSpawnPoints == null || decimatedSpawnPoints.Count == 0)
            {
                return;
            }
            complete = true;

            foreach (SpawnPoint spawnPoint in decimatedSpawnPoints)
                this.DecimatedSpawnPoints.Add(spawnPoint);
        }

        /**
         * Add spawn points.
         *
         * @param SpawnPoints the spawn points
         */
        public void AddSpawnPoints(BlockingCollection<SpawnPoint> spawnPoints)
        {
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                return;
            }
            complete = true;

            foreach (SpawnPoint spawnPoint in spawnPoints)
                this.SpawnPoints.Add(spawnPoint);
        }

        /**
         * Add gyms.
         *
         * @param gyms the gyms
         */
        public void AddGyms(BlockingCollection<FortData> gyms)
        {
            if (gyms == null || gyms.Count == 0)
            {
                return;
            }
            complete = true;

            foreach (FortData fortData in gyms)
                this.Gyms.Add(fortData);
        }

        /**
         * Add pokestops.
         *
         * @param pokestops the pokestops
         */
        public void AddPokestops(BlockingCollection<FortData> pokestops)
        {
            if (pokestops == null || pokestops.Count == 0)
            {
                return;
            }
            complete = true;
            foreach (FortData fortData in pokestops)
            {
                this.Pokestops.Add(new Pokestop(Client, fortData));
            }
        }

        /**
         * Returns whether any data was returned. When a user requests too many cells/wrong cell level/cells too far away
         * from the users location, the server returns empty MapCells.
         *
         * @return whether or not the return returned any data at all;
         */
        public bool IsComplete()
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

            //NearbyPokemons.Clear();
            while (NearbyPokemons.Count > 0)
            {
                NearbyPokemon item;
                NearbyPokemons.TryTake(out item);
            }

            AddNearbyPokemons(other.NearbyPokemons);

            //CatchablePokemons.Clear();
            while (CatchablePokemons.Count > 0)
            {
                MapPokemon item;
                CatchablePokemons.TryTake(out item);
            }

            AddCatchablePokemons(other.CatchablePokemons);

            //WildPokemons.Clear();
            while (WildPokemons.Count > 0)
            {
                WildPokemon item;
                WildPokemons.TryTake(out item);
            }

            AddWildPokemons(other.WildPokemons);

            //DecimatedSpawnPoints.Clear();
            while (DecimatedSpawnPoints.Count > 0)
            {
                SpawnPoint item;
                DecimatedSpawnPoints.TryTake(out item);
            }

            AddDecimatedSpawnPoints(other.DecimatedSpawnPoints);

            //SpawnPoints.Clear();
            while (SpawnPoints.Count > 0)
            {
                SpawnPoint item;
                SpawnPoints.TryTake(out item);
            }

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
