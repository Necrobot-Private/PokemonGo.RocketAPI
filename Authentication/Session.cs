﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using POGOProtos.Settings;
using static POGOProtos.Networking.Envelopes.Signature.Types;
using PokemonGo.RocketAPI.Rpc;
using PokemonGo.RocketAPI.Authentication.Data;
using PokemonGo.RocketAPI.LoginProviders;
using PokemonGo.RocketAPI.HttpClient;
using PokemonGo.RocketAPI.Util.Device;
using System.Device.Location;

namespace PokemonGo.RocketAPI.Authentication
{
    /// <summary>
    /// This is an authenticated <see cref="Session" /> with PokémonGo that handles everything between the developer and PokémonGo.
    /// </summary>
    public class Session : IDisposable
    {
        
        /// <summary>
        /// This is the <see cref="HeartbeatDispatcher" /> which is responsible for retrieving events and updating gps location.
        /// </summary>
        //private readonly HeartbeatDispatcher _heartbeat;

        /// <summary>
        /// This is the <see cref="RpcClient" /> which is responsible for all communication between us and PokémonGo.
        /// Only use this if you know what you are doing.
        /// </summary>
        //public readonly RpcClient RpcClient;

        private static readonly string[] ValidLoginProviders = { "ptc", "google" };

        /// <summary>
        /// Stores data like assets and item templates. Defaults to an in-memory cache, but can be implemented as writing to disk by the platform
        /// </summary>
        // public IDataCache DataCache { get; set; } = new MemoryDataCache();
        // public Templates Templates { get; private set; }

        internal Session(ILoginProvider loginProvider, AccessToken accessToken, GeoCoordinate geoCoordinate, DeviceInfo deviceInfo = null)
        {
            if (!ValidLoginProviders.Contains(loginProvider.ProviderId))
            {
                throw new ArgumentException($"LoginProvider ID must be one of the following: {string.Join(", ", ValidLoginProviders)}");
            }

            HttpClient = new PokemonHttpClient();
            DeviceInfo = deviceInfo ?? DeviceInfoUtil.GetRandomDevice(this);
            AccessToken = accessToken;
            LoginProvider = loginProvider;
           // Player = new Player(geoCoordinate);
            //Map = new Map(this);
            //RpcClient = new RpcClient(this);
            //_heartbeat = new HeartbeatDispatcher(this);
        }

        /// <summary>
        /// Gets the <see cref="Random"/> of the <see cref="Session"/>.
        /// </summary>
        internal Random Random { get; private set; } = new Random();

        /// <summary>
        /// Gets the <see cref="HttpClient"/> of the <see cref="Session"/>.
        /// </summary>
        internal PokemonHttpClient HttpClient { get; }

        /// <summary>
        /// Gets the <see cref="DeviceInfo"/> used by <see cref="RpcEncryption"/>.
        /// </summary>
        public DeviceInfo DeviceInfo { get; private set; }

        /// <summary>
        /// Gets the <see cref="ILoginProvider"/> used to obtain an <see cref="AccessToken"/>.
        /// </summary>
        private ILoginProvider LoginProvider { get; }

        /// <summary>
        ///  Gets the <see cref="AccessToken"/> of the <see cref="Session" />.
        /// </summary>
        public AccessToken AccessToken { get; private set; }

        /// <summary>
        /// Gets the <see cref="Player"/> of the <see cref="Session" />.
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Gets the <see cref="Map"/> of the <see cref="Session" />.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Gets the <see cref="GlobalSettings"/> of the <see cref="Session" />.
        /// </summary>
        public GlobalSettings GlobalSettings { get; internal set; }
        
        private Semaphore ReauthenticateMutex { get; } = new Semaphore(1, 1);
      
        /// <summary>
        /// Ensures the <see cref="Session" /> gets reauthenticated, no matter how long it takes.
        /// </summary>
        internal void Reauthenticate()
        {
            ReauthenticateMutex.WaitOne();
            if (AccessToken.IsExpired)
            {
                AccessToken accessToken = null;
                var tries = 0;
                while (accessToken == null)
                {
                    try
                    {
                        accessToken = LoginProvider.GetAccessToken();
                    }
                    catch (Exception )
                    {
                       // Logger.Error($"Reauthenticate exception was catched: {exception}");
                    }
                    finally
                    {
                        if (accessToken == null)
                        {
                            var sleepSeconds = Math.Min(60, ++tries*5);
                            Task.Delay(TimeSpan.FromMilliseconds(sleepSeconds * 1000)).Wait();
                        }
                    }
                }
                AccessToken = accessToken;
                OnAccessTokenUpdated();
            }
            ReauthenticateMutex.Release();
        }

        private void OnAccessTokenUpdated()
        {
            AccessTokenUpdated?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> AccessTokenUpdated;
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            ReauthenticateMutex?.Dispose();
            //RpcClient?.Dispose();
            HttpClient?.Dispose();
        }
    }
}