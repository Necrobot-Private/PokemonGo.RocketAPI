#region using directives

using System;
using System.Net;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.HttpClient;
using PokemonGo.RocketAPI.Rpc;
using POGOProtos.Enums;
using POGOProtos.Networking.Envelopes;
using PokemonGo.RocketAPI.Helpers;
using POGOLib.Official.Util.Hash;
using PokemonGo.RocketAPI.Hash;
using PokemonGo.RocketAPI.Encrypt;
using PokemonGo.RocketAPI.Exceptions;
using POGOProtos.Networking.Responses;

#endregion

namespace PokemonGo.RocketAPI
{

    public delegate void OnInventoryUpdateHandler(GetInventoryResponse response);

    public class Client : ICaptchaResponseHandler
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
        string CaptchaToken;
        public KillSwitchTask KillswitchTask;
        public Hash.IHasher Hasher;
        public ICrypt Cryptor;
        public event OnInventoryUpdateHandler OnInventoryUpdated;
        public Client(ISettings settings)
        {
            if (settings.UsePogoDevHashServer )
            {
                if (string.IsNullOrEmpty(settings.AuthAPIKey)) throw new AuthConfigException("You selected Pogodev API but not provide proper API Key");
                Hasher = new PokefamerHasher(settings.AuthAPIKey);
                Cryptor = new Crypt();

                // These constants need to change if we update the hashing server API version that is used.
                AppVersion = 5120;
                CurrentApiEmulationVersion = new Version("0.51.2"); 
            }
            else
            if (settings.UseLegacyAPI)
            {
                Hasher = new LegacyHashser();
                Cryptor = new LegacyCrypt();
                AppVersion = 4500;
                CurrentApiEmulationVersion = new Version("0.45.0");

            }
            else
            {
                throw new AuthConfigException("No API method being select in your auth.json");
            }

            //Hasher = new LegacyHashser();


            Settings = settings;
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
            KillswitchTask = new KillSwitchTask(this);

            Player.SetCoordinates(Settings.DefaultLatitude, Settings.DefaultLongitude, Settings.DefaultAltitude);

            InventoryLastUpdateTimestamp = 0;

            Platform = settings.DevicePlatform.Equals("ios", StringComparison.Ordinal) ? Platform.Ios : Platform.Android;

            // We can no longer emulate Android so for now just overwrite settings with randomly generated iOS device info.
            if (Platform == Platform.Android)
            {
                DeviceInfo iosInfo = DeviceInfoHelper.GetRandomIosDevice();
                settings.DeviceId = iosInfo.DeviceId;
                settings.DeviceBrand = iosInfo.DeviceBrand;
                settings.DeviceModel = iosInfo.DeviceModel;
                settings.DeviceModelBoot = iosInfo.DeviceModelBoot;
                settings.HardwareManufacturer = iosInfo.HardwareManufacturer;
                settings.HardwareModel = iosInfo.HardwareModel;
                settings.FirmwareBrand = iosInfo.FirmwareBrand;
                settings.FirmwareType = iosInfo.FirmwareType;

                // Clear out the android fields.
                settings.AndroidBoardName = "";
                settings.AndroidBootloader = "";
                settings.DeviceModelIdentifier = "";
                settings.FirmwareTags = "";
                settings.FirmwareFingerprint = "";

                // Now set the client platform to ios
                Platform = Platform.Ios;
            }
            SettingsHash = "";
        }
        
        public void SetCaptchaToken(string token)
        {
            CaptchaToken = token;
        }
        
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
        public long StartTime { get; set; }
        internal GetInventoryResponse inventory;
        public Version CurrentApiEmulationVersion { get; set; }
        public Version MinimumClientVersion { get; set; }        // This is version from DownloadSettings, but after login is updated from https://pgorelease.nianticlabs.com/plfe/version

        //public POGOLib.Net.Session AuthSession { get; set; }
        public POGOLib.Official.LoginProviders.ILoginProvider LoginProvider { get; set; }
        public POGOLib.Official.Net.Authentication.Data.AccessToken AccessToken { get; set; }
        public GetInventoryResponse LastGetInvenrotyResponse { get { return inventory; }
            set {
                if (inventory == null)
                {
                    inventory = value;
                }
                else {
                    //Console.WriteLine($"{ value.InventoryDelta }");
                    inventory.MergeWith(value);
                }
                if (OnInventoryUpdated!= null)
                OnInventoryUpdated ?.Invoke( inventory);
            }
        }
        private WebProxy InitProxy()
        {
            if (!Settings.UseProxy) return null;

            var prox = new WebProxy(new Uri($"http://{Settings.UseProxyHost}:{Settings.UseProxyPort}"), false, null);

            if (Settings.UseProxyAuthentication)
                prox.Credentials = new NetworkCredential(Settings.UseProxyUsername, Settings.UseProxyPassword);

            return prox;
        }

        public bool CheckCurrentVersionOutdated()
        {
            if (MinimumClientVersion == null)
                return false;

            return CurrentApiEmulationVersion < MinimumClientVersion;
        }

        public static Version GetMinimumRequiredVersionFromUrl()
        {
            try
            {
                var client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                var version = client.DownloadString("https://pgorelease.nianticlabs.com/plfe/version").Replace("\u0006", "").Replace("\n", "");
                return new Version(version);
            }
            catch(Exception)
            {
            }
            return null;
        }
    }
}