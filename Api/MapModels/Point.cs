using POGOProtos.Map;
using PokemonGo.RocketAPI.Api.Util;

namespace PokemonGo.RocketAPI.Api.MapModels
{
    public class Point : MapPoint
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Point(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Point(SpawnPoint spawnpoint)
        {
            this.Latitude = spawnpoint.Latitude;
            this.Longitude = spawnpoint.Longitude;
        }
    }
}
