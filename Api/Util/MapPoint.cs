namespace PokemonGo.RocketAPI.Api.Util
{
    public interface MapPoint
    {
        /**
         * Gets latitude.
         *
         * @return the latitude
         */
        double Latitude { get; set; }

        /**
         * Gets longitude.
         *
         * @return the longitude
         */
        double Longitude { get; set; }
    }
}
