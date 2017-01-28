#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Responses;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using PokemonGo.RocketAPI.LoginProviders;
using PokemonGo.RocketAPI.Authentication.Data;
using PokemonGo.RocketAPI.Authentication;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void GoogleDeviceCodeDelegate(string code, string uri);

    public class Login : BaseRpc
    {
        private static Semaphore ReauthenticateMutex { get; } = new Semaphore(1, 1);
        public Login(Client client) : base(client)
        {
            Client.LoginProvider = SetLoginType(client.Settings);
            Client.ApiUrl = Resources.RpcUrl;
        }

        private static ILoginProvider SetLoginType(ISettings settings)
        {
            switch (settings.AuthType)
            {
                case AuthType.Google:
                    return new GoogleLoginProvider(settings.GoogleUsername, settings.GooglePassword);
                case AuthType.Ptc:
                    return new PtcLoginProvider(settings.PtcUsername, settings.PtcPassword);
                default:
                    throw new ArgumentOutOfRangeException(nameof(settings.AuthType), "Unknown AuthType");
            }
        }

        private static async Task<AccessToken> LoadAccessToken(ILoginProvider loginProvider, Client client, bool mayCache = false)
        {
            var cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
            var fileName = Path.Combine(cacheDir, $"{loginProvider.UserId}-{loginProvider.ProviderId}.json");
            
            if (mayCache)
            {
                if (!Directory.Exists(cacheDir))
                    Directory.CreateDirectory(cacheDir);

                if (File.Exists(fileName))
                {
                    var accessToken = JsonConvert.DeserializeObject<AccessToken>(File.ReadAllText(fileName));

                    if (!accessToken.IsExpired)
                    {
                        client.AccessToken = accessToken;
                        return accessToken;
                    }
                }
            }

            await Reauthenticate(client);

            if (mayCache)
                SaveAccessToken(client.AccessToken);

            return client.AccessToken;
        }

        private static void SessionOnAccessTokenUpdated(object sender, EventArgs eventArgs)
        {
            var session = (Session)sender;

            SaveAccessToken(session.AccessToken);
        }

        public static void SaveAccessToken(AccessToken accessToken)
        {
            if (accessToken == null || string.IsNullOrEmpty(accessToken.Uid))
                return;

            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Cache", $"{accessToken.Uid}.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(accessToken, Formatting.Indented));
        }

        public static async Task Reauthenticate(Client client)
        {
            try
            {
                ReauthenticateMutex.WaitOne();

                var tries = 0;
                while (null == client.AccessToken || client.AccessToken.IsExpired || client.AccessToken.Token == null)
                {
                    try
                    {
                        client.AccessToken = await client.LoginProvider.GetAccessToken();
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine(ex.Message);
                        if (ex.Message.Contains("You have to log into an browser")) throw new GoogleTwoFactorException(ex.Message);
                        //Logger.Error($"Reauthenticate exception was catched: {exception}");
                    }
                    finally
                    {
                        if (client.AccessToken == null || client.AccessToken.Token == null)
                        {
                            var sleepSeconds =  Math.Min(60, ++tries * 5);
                            //Logger.Error($"Reauthentication failed, trying again in {sleepSeconds} seconds.");
                            await Task.Delay(TimeSpan.FromMilliseconds(sleepSeconds * 1000));
                        }

                        if (tries == 5)
                        {
                            var cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
                            var fileName = Path.Combine(cacheDir, $"{client.AccessToken?.Uid}-{client.LoginProvider.ProviderId}.json");
                            File.Delete(fileName);

                            throw new TokenRefreshException("Error refreshing access token.");
                        }
                    }
                }
                SaveAccessToken(client.AccessToken);
            }
            finally
            {
                ReauthenticateMutex.Release();
            }
        }

        public async Task<GetPlayerResponse> DoLogin()
        {
            // Don't wait for background start of killswitch.
            Client.KillswitchTask.Start();

            await Login.LoadAccessToken(Client.LoginProvider, Client, true);
            Client.StartTime = Utils.GetTime(true);
            RequestBuilder.Reset();

            var player = await Client.Player.GetPlayer(false); // Set false because initial GetPlayer does not use common requests.

            await Client.Download.GetRemoteConfigVersion();
            await Client.Download.GetAssetDigest();
            await Client.Download.GetItemTemplates();
            
            await Client.Player.GetPlayerProfile();
            
            return player;
        }
    }
}