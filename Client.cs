﻿#region using directives

using System;
using System.Net;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.HttpClient;
using PokemonGo.RocketAPI.Rpc;
using POGOProtos.Enums;
using POGOProtos.Networking.Envelopes;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Hash;
using PokemonGo.RocketAPI.Encrypt;
using PokemonGo.RocketAPI.Exceptions;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Authentication.Data;
using PokemonGo.RocketAPI.LoginProviders;
using POGOProtos.Settings;

#endregion

namespace PokemonGo.RocketAPI
{
    public class Client : ICaptchaResponseHandler
    {
        public static string API_VERSION = "0.57.3";

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
        internal RequestBuilder RequestBuilder;

        public Client(ISettings settings)
        {
            if (settings.UsePogoDevHashServer )
            {
                if (string.IsNullOrEmpty(settings.AuthAPIKey)) throw new AuthConfigException("You selected Pogodev API but not provide proper API Key");

                Cryptor = new Crypt();

                // This value will determine which version of hashing you receive.
                // Currently supported versions:
                // v119   -> Pogo iOS 1.19
                // v121   -> Pogo iOS 1.21
                // v121_2 -> Pogo iOS 1.22
                // v125   -> Pogo iOS 1.25
                // v127_2 -> Pogo iOS 1.27.2
                // v127_3 -> Pogo iOS 1.27.3
                ApiEndPoint = "api/v127_3/hash";
                Hasher = new PokefamerHasher(settings.AuthAPIKey, settings.DisplayVerboseLog, ApiEndPoint);

                // These 4 constants below need to change if we update the hashing server API version that is used.
                Unknown25 = -816976800928766045;
                AppVersion = 5703;
                CurrentApiEmulationVersion = new Version(API_VERSION); // Make sure to update the constant above.
                UnknownPlat8Field = "90f6a704505bccac73cec99b07794993e6fd5a12";
            }
            else
            if (settings.UseLegacyAPI)
            {
                Hasher = new LegacyHashser();
                Cryptor = new LegacyCrypt();

                Unknown25 = -816976800928766045;// - 816976800928766045;// - 1553869577012279119;
                AppVersion = 4500;
                CurrentApiEmulationVersion = new Version("0.45.0");
            }
            else
            {
                throw new AuthConfigException("No API method being select in your auth.json");
            }
            
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
        public GlobalSettings GlobalSettings { get; set; }
        public long InventoryLastUpdateTimestamp { get; set; }
        internal Platform Platform { get; set; }
        internal uint AppVersion { get; set; }
        internal string UnknownPlat8Field { get; set; }
        internal long Unknown25 { get; set; }
        internal string ApiEndPoint { get; set; }
        public long StartTime { get; set; }
        public Version CurrentApiEmulationVersion { get; set; }
        public Version MinimumClientVersion { get; set; }        // This is version from DownloadSettings, but after login is updated from https://pgorelease.nianticlabs.com/plfe/version

        //public POGOLib.Net.Session AuthSession { get; set; }
        public ILoginProvider LoginProvider { get; set; }
        public AccessToken AccessToken { get; set; }
        
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
