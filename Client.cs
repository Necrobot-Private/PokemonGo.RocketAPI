#region using directives

using System;
using System.Net;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.HttpClient;
using PokemonGo.RocketAPI.Rpc;
using POGOProtos.Enums;
using PokemonGo.RocketAPI.Api.PlayerModels;
using POGOProtos.Networking.Envelopes;

#endregion

namespace PokemonGo.RocketAPI
{
    public class Client
    {
        public static WebProxy Proxy;

        internal readonly PokemonHttpClient PokemonHttpClient;
        
        public Download Download;
        public Encounter Encounter;
        public Fort Fort;
        public Inventory Inventory;
        public Rpc.Login Login;
        public Map Map;
        public Misc Misc;
        public Player Player;

        public Api.InventoryModels.Inventories Inventories;
        public Api.SettingsModels.ApiSettings ApiSettings;
        public Api.MapModels.Map ApiMap;
        public PlayerProfile PlayerProfile;

        public Client(ISettings settings, IApiFailureStrategy apiFailureStrategy)
        {
            Settings = settings;
            ApiFailure = apiFailureStrategy;
            Proxy = InitProxy();
            PokemonHttpClient = new PokemonHttpClient();
            Login = new Rpc.Login(this);
            Player = new Player(this);
            Download = new Download(this);
            Inventory = new Inventory(this);
            Map = new Map(this);
            Fort = new Fort(this);
            Encounter = new Encounter(this);
            Misc = new Misc(this);

            Player.SetCoordinates(Settings.DefaultLatitude, Settings.DefaultLongitude, Settings.DefaultAltitude);

            Platform = settings.DevicePlatform.Equals("ios", StringComparison.Ordinal) ? Platform.Ios : Platform.Android;

            AppVersion = 3500;

            Inventories = new Api.InventoryModels.Inventories(this);
            ApiSettings = new Api.SettingsModels.ApiSettings(this);
            PlayerProfile = new PlayerProfile(this);
        }

        public IApiFailureStrategy ApiFailure { get; set; }
        public ISettings Settings { get; }
        public string AuthToken { get; set; }

        public double CurrentLatitude { get; internal set; }
        public double CurrentLongitude { get; internal set; }
        public double CurrentAltitude { get; internal set; }
        public double CurrentAccuracy { get; internal set; }
        public float CurrentSpeed { get; internal set; }

        public AuthType AuthType => Settings.AuthType;
        internal string ApiUrl { get; set; }
        internal AuthTicket AuthTicket { get; set; }

        internal string SettingsHash { get; set; }
        internal long InventoryLastUpdateTimestamp { get; set; }
        internal Platform Platform { get; set; }
        internal uint AppVersion { get; set; }
        internal long StartTime { get; set; }
        
        private WebProxy InitProxy()
        {
            if (!Settings.UseProxy) return null;

            var prox = new WebProxy(new Uri($"http://{Settings.UseProxyHost}:{Settings.UseProxyPort}"), false, null);

            if (Settings.UseProxyAuthentication)
                prox.Credentials = new NetworkCredential(Settings.UseProxyUsername, Settings.UseProxyPassword);

            return prox;
        }
    }
}