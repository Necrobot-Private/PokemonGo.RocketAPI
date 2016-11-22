#region using directives

using System.Collections.Generic;
using System.Threading.Tasks;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Helpers;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Download : BaseRpc
    {
        public Download(Client client) : base(client)
        {
        }

        public async Task<DownloadSettingsResponse> GetSettings()
        {
            var message = new DownloadSettingsMessage
            {
                Hash = Client.SettingsHash
            };

            return await PostProtoPayload<Request, DownloadSettingsResponse>(RequestType.DownloadSettings, message);
        }

        public async Task<DownloadItemTemplatesResponse> GetItemTemplates()
        {
            return
                await
                    PostProtoPayload<Request, DownloadItemTemplatesResponse>(RequestType.DownloadItemTemplates,
                        new DownloadItemTemplatesMessage());
        }

        public async Task<DownloadRemoteConfigVersionResponse> GetRemoteConfigVersion(uint appVersion, Platform platform)
        {
            return
                await
                    PostProtoPayload<Request, DownloadRemoteConfigVersionResponse>(
                        RequestType.DownloadRemoteConfigVersion, new DownloadRemoteConfigVersionMessage
                        {
                            AppVersion = appVersion,
                            Platform = platform
                        });
        }

        public async Task<GetAssetDigestResponse> GetAssetDigest(uint appVersion, Platform platform)
        {
            return
                await
                    PostProtoPayload<Request, GetAssetDigestResponse>(RequestType.GetAssetDigest,
                        new GetAssetDigestMessage
                        {
                            AppVersion = appVersion,
                            Platform = platform
                        });
        }

        public async Task<GetDownloadUrlsResponse> GetDownloadUrls(IEnumerable<string> assetIds)
        {
            return
                await
                    PostProtoPayload<Request, GetDownloadUrlsResponse>(RequestType.GetDownloadUrls,
                        new GetDownloadUrlsMessage
                        {
                            AssetId = {assetIds}
                        });
        }
    }
}