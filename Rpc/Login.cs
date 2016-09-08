using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Login;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void GoogleDeviceCodeDelegate(string code, string uri);
    public class Login : BaseRpc
    {
        //public event GoogleDeviceCodeDelegate GoogleDeviceCodeEvent;
        private ILoginType login;

        public Login(Client client) : base(client)
        {
            login = SetLoginType(client.Settings);
            _client.ApiUrl = Resources.RpcUrl;
        }

        private static ILoginType SetLoginType(ISettings settings)
        {
            switch (settings.AuthType)
            {
                case AuthType.Google:
                    return new GoogleLogin(settings.GoogleUsername, settings.GooglePassword);
                case AuthType.Ptc:
                    return new PtcLogin(settings.PtcUsername, settings.PtcPassword, settings);
                default:
                    throw new ArgumentOutOfRangeException(nameof(settings.AuthType), "Unknown AuthType");
            }
        }

        public async Task DoLogin()
        {
            _client.AuthToken = await login.GetAccessToken().ConfigureAwait(false);
            await SetServer().ConfigureAwait(false);
        }

        public static Request GetDownloadSettingsMessageRequest(Client client)
        {
            DownloadSettingsMessage downloadSettingsMessage = new DownloadSettingsMessage();
            if (client.SettingsHash != null)
                downloadSettingsMessage.Hash = client.SettingsHash;

            return new Request
            {
                RequestType = RequestType.DownloadSettingsMessage,
                RequestMessage = downloadSettingsMessage.ToByteString()
            };
        }

        private async Task SetServer()
        {
            #region Standard intial request messages in right Order

            var getPlayerMessage = new GetPlayerMessage();
            var getHatchedEggsMessage = new GetHatchedEggsMessage();
            var getInventoryMessage = new GetInventoryMessage
            {
                LastTimestampMs = DateTime.UtcNow.ToUnixTime()
            };
            var checkAwardedBadgesMessage = new CheckAwardedBadgesMessage();
            
            #endregion

            var serverRequest = RequestBuilder.GetInitialRequestEnvelope(
                new Request
                {
                    RequestType = RequestType.GetPlayer,
                    RequestMessage = getPlayerMessage.ToByteString()
                }, new Request
                {
                    RequestType = RequestType.GetHatchedEggs,
                    RequestMessage = getHatchedEggsMessage.ToByteString()
                }, new Request
                {
                    RequestType = RequestType.GetInventory,
                    RequestMessage = getInventoryMessage.ToByteString()
                }, new Request
                {
                    RequestType = RequestType.CheckAwardedBadges,
                    RequestMessage = checkAwardedBadgesMessage.ToByteString()
                }, new Request
                {
                    RequestType = RequestType.DownloadSettings,
                    RequestMessage = GetDownloadSettingsMessageRequest(_client).ToByteString()
                });

            var serverResponse = await PostProto<Request>(serverRequest);

            if (!String.IsNullOrEmpty(serverResponse.ApiUrl))
                _client.ApiUrl = "https://" + serverResponse.ApiUrl + "/rpc";

            if (serverResponse.AuthTicket != null)
                _client.AuthTicket = serverResponse.AuthTicket;

            if (serverResponse.StatusCode == 102)
            {
                _client.AuthToken = null;
                throw new AccessTokenExpiredException();
            }
            else if (serverResponse.StatusCode == 53)
            {
                // 53 means that the api_endpoint was not correctly set, should be at this point, though, so redo the request
                await SetServer();
                return;
            }
            else if (serverResponse.StatusCode == 3)
            {
                // Your account may be banned! please try from the official client.
                throw new LoginFailedException("Your account may be banned! please try from the official client.");
            }
            
            var downloadSettingsResponse = new DownloadSettingsResponse();
            downloadSettingsResponse.MergeFrom(serverResponse.Returns[4]);

            _client.SettingsHash = downloadSettingsResponse.Hash;
        }

    }
}
