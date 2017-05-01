#region using directives

using System;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Responses;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using PokemonGo.RocketAPI.LoginProviders;
using PokemonGo.RocketAPI.Authentication.Data;
//using System.Windows.Forms;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void GoogleDeviceCodeDelegate(string code, string uri);

    public class Login : BaseRpc
    {
        private Semaphore ReauthenticateMutex { get; } = new Semaphore(1, 1);
        public Login(Client client) : base(client)
        {
            Client.LoginProvider = SetLoginType(client.Settings);
            Client.ApiUrl = Constants.RpcUrl;
        }

        private ILoginProvider SetLoginType(ISettings settings)
        {
            switch (settings.AuthType)
            {
                case AuthType.Google:
                    return new GoogleLoginProvider(settings.Username, settings.Password);
                case AuthType.Ptc:
                    return new PtcLoginProvider(settings.Username, settings.Password);
                default:
                    throw new ArgumentOutOfRangeException(nameof(settings.AuthType), "Unknown AuthType");
            }
        }

        private bool IsValidAccessToken()
        {
            if (Client.AccessToken == null || string.IsNullOrEmpty(Client.AccessToken.Token) || Client.AccessToken.IsExpired)
                return false;

            return true;
        }

        public async Task<AccessToken> GetValidAccessToken(bool forceRefresh = false, bool isCached = false)
        {
            try
            {
                ReauthenticateMutex.WaitOne();

                if (forceRefresh)
                {
                    Client.AccessToken.Expire();
                    if (isCached)
                        DeleteSavedAccessToken();
                }

                if (IsValidAccessToken())
                    return Client.AccessToken;

                // If we got here then access token is expired or not loaded into memory.
                if (isCached)
                {
                    var loginProvider = Client.LoginProvider;
                    var cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
                    var fileName = Path.Combine(cacheDir, $"{loginProvider.UserId}-{loginProvider.ProviderId}.json");

                    if (!Directory.Exists(cacheDir))
                        Directory.CreateDirectory(cacheDir);

                    if (File.Exists(fileName))
                    {
                        var accessToken = JsonConvert.DeserializeObject<AccessToken>(File.ReadAllText(fileName));

                        if (!accessToken.IsExpired)
                        {
                            Client.AccessToken = accessToken;
                            return accessToken;
                        }
                    }
                }

                await Reauthenticate(isCached).ConfigureAwait(false);
                return Client.AccessToken;
            }
            finally
            {
                ReauthenticateMutex.Release();
            }
        }

        private void SaveAccessToken()
        {
            if (!IsValidAccessToken())
                return;

            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "Cache", $"{Client.AccessToken.Uid}.json");

            File.WriteAllText(fileName, JsonConvert.SerializeObject(Client.AccessToken, Formatting.Indented));
        }

        private void DeleteSavedAccessToken()
        {
            var cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
            var fileName = Path.Combine(cacheDir, $"{Client.AccessToken?.Uid}-{Client.LoginProvider.ProviderId}.json");
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        private async Task Reauthenticate(bool isCached)
        {
            var tries = 0;
            while (!IsValidAccessToken())
            {
                // If expired, then we always delete the saved access token if it exists.
                if (isCached)
                    DeleteSavedAccessToken();

                try
                {
                    Client.AccessToken = await Client.LoginProvider.GetAccessToken().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    APIConfiguration.Logger.LogError(ex.Message);

                    if (ex.Message.Contains("15 minutes")) throw new PtcLoginException(ex.Message);

                    if (ex.Message.Contains("You have to log into a browser")) throw new GoogleTwoFactorException(ex.Message);
                    //Logger.Error($"Reauthenticate exception was catched: {exception}");
                }
                finally
                {
                    if (!IsValidAccessToken())
                    {
                        var sleepSeconds = Math.Min(60, ++tries * 5);
                        //Logger.Error($"Reauthentication failed, trying again in {sleepSeconds} seconds.");
                        await Task.Delay(TimeSpan.FromMilliseconds(sleepSeconds * 1000)).ConfigureAwait(false);
                    }
                    else
                    {
                        // We have successfully refreshed the token so save it.
                        if (isCached)
                            SaveAccessToken();
                    }

                    if (tries == 5)
                    {
                        throw new TokenRefreshException("Error refreshing access token.");
                    }
                }
            }
        }

        public async Task<GetPlayerResponse> DoLogin()
        {
            Client.Reset();

            // Don't wait for background start of killswitch.
            // jjskuld - Ignore CS4014 warning for now.
#pragma warning disable 4014
            Client.KillswitchTask.Start();
#pragma warning restore 4014

            var player = await Client.Player.GetPlayer(false, true).ConfigureAwait(false); // Set false because initial GetPlayer does not use common requests.
            if (player.Warn)
            {
                APIConfiguration.Logger.LogFlaggedInit($"This account {Client.Player.PlayerData.Username} seems to be flagged, it is recommended to not use bot on this account for now!");
            }

            if (player.Banned)
            {
                 APIConfiguration.Logger.LogErrorInit("This account seems to be banned");
            }
            APIConfiguration.Logger.LogDebug("GetPlayer done.");
            await RandomHelper.RandomDelay(10000).ConfigureAwait(false);
            await Client.Download.GetRemoteConfigVersion().ConfigureAwait(false);
            APIConfiguration.Logger.LogDebug("GetRemoteConfigVersion done.");
            await RandomHelper.RandomDelay(300).ConfigureAwait(false);
            await Client.Download.GetAssetDigest().ConfigureAwait(false);
            APIConfiguration.Logger.LogDebug("GetAssetDigest done.");
            await RandomHelper.RandomDelay(300).ConfigureAwait(false);
            await Client.Download.GetItemTemplates().ConfigureAwait(false);
            APIConfiguration.Logger.LogDebug("GetItemTemplates done.");
            await RandomHelper.RandomDelay(300).ConfigureAwait(false);
            await Client.Player.GetPlayerProfile().ConfigureAwait(false);
            APIConfiguration.Logger.LogDebug("GetPlayerProfile done.");
            await RandomHelper.RandomDelay(300).ConfigureAwait(false);

            return player;
        }
    }
}
