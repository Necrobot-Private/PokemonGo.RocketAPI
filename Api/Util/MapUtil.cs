using PokemonGo.RocketAPI.Api.MapModels;
using PokemonGo.RocketAPI.Extensions;
using System;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.Util
{
    public class MapUtil<K> where K : MapPoint
    {
        /**
         * Random step to a coordinate object
         *
         * @param point the coordinate
         * @return the coordinate
         */
        public static Point RandomStep(Point point)
        {
            point.Longitude = (point.Longitude + RandomStep());
            point.Latitude = (point.Latitude + RandomStep());

            return point;
        }

        /**
         * Random step double.
         *
         * @return the double
         */
        public static double RandomStep()
        {
            Random random = new Random();
            return random.NextDouble() / 100000.0;
        }

        /**
         * Dist between coordinates
         *
         * @param start the start coordinate
         * @param end   the end coordinate
         * @return the double
         */
        public static double DistFrom(Point start, Point end)
        {
            return DistFrom(start.Latitude, start.Longitude, end.Latitude, end.Longitude);
        }

        /**
         * Dist between coordinates
         *
         * @param lat1 the start latitude coordinate
         * @param lng1 the start longitude coordinate
         * @param lat2 the end latitude coordinate
         * @param lng2 the end longitude coordinate
         * @return the double
         */
        public static double DistFrom(double lat1, double lng1, double lat2, double lng2)
        {
            double earthRadius = 6371000;
            double lat = (lat2 - lat1).ToRadians();
            double lng = (lng2 - lng1).ToRadians();
            double haversine = Math.Sin(lat / 2) * Math.Sin(lat / 2)
                    + Math.Cos(lat.ToRadians()) * Math.Cos(lat2.ToRadians())
                    * Math.Sin(lng / 2) * Math.Sin(lng / 2);


            return earthRadius * (2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine)));
        }

        /**
         * Sort items map by distance
         *
         * @param items the items
         * @param api   the api
         * @return the map
         */
        public Dictionary<double, K> sortItems(List<K> items, Client client)
        {
            Dictionary<Double, K> result = new Dictionary<Double, K>();
            foreach (K point in items)
            {
                result[DistFrom(client.CurrentLatitude, client.CurrentLongitude, point.Latitude, point.Longitude)] = point;
            }
            return result;
        }
    }
}
