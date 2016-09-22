#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Login;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Responses;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public delegate void GoogleDeviceCodeDelegate(string code, string uri);

    public class Login : BaseRpc
    {
        //public event GoogleDeviceCodeDelegate GoogleDeviceCodeEvent;
        private readonly ILoginType _login;

        public Login(Client client) : base(client)
        {
            _login = SetLoginType(client.Settings);
            Client.ApiUrl = Resources.RpcUrl;
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
            // Always use auth token over auth ticket when logging in.
            Client.AuthToken = await _login.GetAccessToken().ConfigureAwait(false);
            Client.AuthTicket = null;
            Client.StartTime = Utils.GetTime(true);
            
            await
                FireRequestBlock(CommonRequest.GetDownloadRemoteConfigVersionMessageRequest(Client), typeof(DownloadRemoteConfigVersionResponse))
                    .ConfigureAwait(false);
            await FireRequestBlockTwo().ConfigureAwait(false);
        }

        private async Task FireRequestBlock(Request request, Type requestType)
        {
            var requests = CommonRequest.FillRequest(request, Client);

            var serverRequest = GetRequestBuilder().GetRequestEnvelope(requests);
            IMessage[] responses = await PostProtoPayload<Request>(serverRequest, requestType, typeof(CheckChallengeResponse), typeof(GetHatchedEggsResponse), typeof(GetInventoryResponse), typeof(CheckAwardedBadgesResponse), typeof(DownloadSettingsResponse));
            
            if (responses != null)
            {
                if (4 <= responses.Length)
                {
                    GetInventoryResponse getInventoryResponse = responses[3] as GetInventoryResponse;

                    Client.InventoryLastUpdateTimestamp = Utils.GetTime(true);
                }
                
                if (6 <= responses.Length)
                {
                    DownloadSettingsResponse downloadSettingsResponse = responses[5] as DownloadSettingsResponse;

                    Client.SettingsHash = downloadSettingsResponse.Hash;
                }
            }
        }

        public async Task FireRequestBlockTwo()
        {
            await FireRequestBlock(CommonRequest.GetGetAssetDigestMessageRequest(Client), typeof(GetAssetDigestResponse));
        }
    }
}