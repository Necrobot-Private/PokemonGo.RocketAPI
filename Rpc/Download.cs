#region using directives

using System.Collections.Generic;
using System.Threading.Tasks;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using System;
using PokemonGo.RocketAPI.Helpers;
using Google.Protobuf;
using Google.Protobuf.Collections;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse.Types;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Download : BaseRpc
    {
        public Download(Client client) : base(client)
        {
        }

        public RepeatedField<ItemTemplate> ItemTemplates { get; set; }

        public async Task<DownloadItemTemplatesResponse> GetItemTemplates()
        {
            IMessage downloadItemTemplatesMessage = new DownloadItemTemplatesMessage();
            var downloadItemTemplatesRequest = new Request
            {
                RequestType = RequestType.DownloadItemTemplates,
                RequestMessage = downloadItemTemplatesMessage.ToByteString()
            };

            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(downloadItemTemplatesRequest, Client));

            Tuple<DownloadItemTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, DownloadItemTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope);

            DownloadItemTemplatesResponse downloadItemTemplatesResponse = response.Item1;
            ItemTemplates = downloadItemTemplatesResponse.ItemTemplates;
            PokemonMeta.Update(downloadItemTemplatesResponse);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<DownloadRemoteConfigVersionResponse> GetRemoteConfigVersion()
        {
            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(CommonRequest.GetDownloadRemoteConfigVersionMessageRequest(Client), Client, RequestType.GetBuddyWalked));

            Tuple<DownloadRemoteConfigVersionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, DownloadRemoteConfigVersionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetAssetDigestResponse> GetAssetDigest()
        {
            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(CommonRequest.GetGetAssetDigestMessageRequest(Client), Client, RequestType.GetBuddyWalked));

            Tuple<GetAssetDigestResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, GetAssetDigestResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetDownloadUrlsResponse> GetDownloadUrls(IEnumerable<string> assetIds)
        {
            var getDownloadUrlsRequest = new Request
            {
                RequestType = RequestType.GetDownloadUrls,
                RequestMessage = new GetDownloadUrlsMessage
                {
                    AssetId = { assetIds }
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getDownloadUrlsRequest, Client));

            Tuple<GetDownloadUrlsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetDownloadUrlsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}