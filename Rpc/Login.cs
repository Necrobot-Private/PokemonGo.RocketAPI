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

            await FireRequestBlock(CommonRequest.GetDownloadRemoteConfigVersionMessageRequest(_client)).ConfigureAwait(false);
            await FireRequestBlockTwo().ConfigureAwait(false);
        }
        
        private async Task FireRequestBlock(Request request)
        {
            Request[] requests = CommonRequest.FillRequest(request, _client);

            var serverRequest = RequestBuilder.GetInitialRequestEnvelope(requests);
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
                await FireRequestBlock(request);
                return;
            }
            else if (serverResponse.StatusCode == 3)
            {
                // Your account may be banned! please try from the official client.
                throw new LoginFailedException("Your account may be banned! please try from the official client.");
            }

            var responses = serverResponse.Returns;
            if (responses != null)
            {
                var getInventoryResponse = new GetInventoryResponse();
                if (4 <= responses.Count)
                {
                    getInventoryResponse.MergeFrom(responses[3]);

                    _client.InventoryLastUpdateTimestamp = Client.GetCurrentTimeMillis();
                }

                var downloadSettingsResponse = new DownloadSettingsResponse();
                if (6 <= responses.Count)
                {
                    downloadSettingsResponse.MergeFrom(responses[5]);

                    _client.SettingsHash = downloadSettingsResponse.Hash;
                }
            }
        }

        public async Task FireRequestBlockTwo()
        {
            await FireRequestBlock(CommonRequest.GetGetAssetDigestMessageRequest(_client));
        }

    }
}
