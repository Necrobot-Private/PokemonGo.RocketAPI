namespace PokemonGo.RocketAPI.Logging
{
    public class HashInfo
    {
        public string Version { get; set; }
        public string Url { get; set; }

        public int APICalles { get; set; }

        public double TotalTimes { get; set; }
        public double Slowest { get; set; }
        public double Fastest { get; set; }

        public double Last60MinAPICalles { get; set; }
        public double Last60MinAPIAvgTime { get; set; }
        public string Expired { get; set; }
        public string MaskedAPIKey { get; set; }
        public double HealthyRate { get; set; }
    }
}
