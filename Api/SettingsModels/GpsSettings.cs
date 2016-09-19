namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class GpsSettings
    {
        /**
         *
         * @return meters per seconds.
         */
        public double DrivingWarningSpeedMetersPerSecond;

        /**
         *
         * @return minutes.
         */
        public float DrivingWarningCooldownMinutes;

        /**
         *
         * @return seconds.
         */
        public float DrivingSpeedSampleIntervalSeconds;

        /**
         *
         * @return count.
         */
        public double DrivingSpeedSampleCount;

        /**
         * Update the gps settings from the network response.
         *
         * @param gpsSettings the new gps settings
         */
        public void Update(POGOProtos.Settings.GpsSettings gpsSettings)
        {
            DrivingWarningSpeedMetersPerSecond = gpsSettings.DrivingWarningSpeedMetersPerSecond;
            DrivingWarningCooldownMinutes = gpsSettings.DrivingWarningCooldownMinutes;
            DrivingSpeedSampleIntervalSeconds = gpsSettings.DrivingSpeedSampleIntervalSeconds;
            DrivingSpeedSampleCount = gpsSettings.DrivingSpeedSampleCount;
        }
    }

}
