using System;

namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class MapSettings
    {
        /**
	     * Google api key used for display map
	     *
	     * @return String.
	     */
        public String GoogleApiKey;

        /**
	     * Minimum distance between getMapObjects requests
	     *
	     * @return distance in meters.
	     */
        public float MinMapObjectDistance;

        /**
	     * Max refresh betweewn getMapObjecs requests
	     *
	     * @return value in milliseconds.
	     */
        public float MaxRefresh;

        /**
	     * Min refresh betweewn getMapObjecs requests
	     *
	     * @return value in milliseconds.
	     */
        public float MinRefresh;

        /**
	     * NOT SURE: the max distance for encounter pokemon?
	     *
	     * @return distance in meters.
	     */
        public double EncounterRange;

        /**
	     * NOT SURE: the max distance before show pokemon on map?
	     *
	     * @return distance in meters.
	     */
        public double PokemonVisibilityRange;

        /**
	     * NO IDEA
	     *
	     * @return distance in meters.
	     */
        public double PokeNavRange;

        public void Update(POGOProtos.Settings.MapSettings mapSettings)
        {
            GoogleApiKey = mapSettings.GoogleMapsApiKey;
            MinMapObjectDistance = mapSettings.GetMapObjectsMinDistanceMeters;
            MaxRefresh = mapSettings.GetMapObjectsMaxRefreshSeconds * 1000;
            MinRefresh = mapSettings.GetMapObjectsMinRefreshSeconds * 1000;
            EncounterRange = mapSettings.EncounterRangeMeters;
            PokemonVisibilityRange = mapSettings.PokemonVisibleRange;
            PokeNavRange = mapSettings.PokeNavRangeMeters;
        }
    }
}
