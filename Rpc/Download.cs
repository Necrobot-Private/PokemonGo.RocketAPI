#region using directives

using System.Collections.Generic;
using System.Threading.Tasks;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using System;
using PokemonGo.RocketAPI.Helpers;
using Google.Protobuf;
using Google.Protobuf.Collections;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Download : BaseRpc
    {
        public Download(Client client) : base(client)
        {
        }

        public RepeatedField<DownloadItemTemplatesResponse.Types.ItemTemplate> ItemTemplates { get; set; }

        public async Task<DownloadItemTemplatesResponse> GetItemTemplates()
        {
            var downloadItemTemplatesRequest = new Request
            {
                RequestType = RequestType.DownloadItemTemplates,
                RequestMessage = ((IMessage)new DownloadItemTemplatesMessage()
                {
                    // To be implemented
                    //PageTimestamp = 0,
                    //Paginate = true,
                    //PageOffset = Client.PageOffset
                }).ToByteString()
            };

            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(downloadItemTemplatesRequest, Client)).ConfigureAwait(false);

            Tuple<DownloadItemTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, DownloadItemTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope).ConfigureAwait(false);

            DownloadItemTemplatesResponse downloadItemTemplatesResponse = response.Item1;
            ItemTemplates = downloadItemTemplatesResponse.ItemTemplates;
            PokemonMeta.Update(downloadItemTemplatesResponse);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<DownloadRemoteConfigVersionResponse> GetRemoteConfigVersion()
        {
            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(CommonRequest.GetDownloadRemoteConfigVersionMessageRequest(Client), Client, RequestType.GetBuddyWalked)).ConfigureAwait(false);

            Tuple<DownloadRemoteConfigVersionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, DownloadRemoteConfigVersionResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetAssetDigestResponse> GetAssetDigest()
        {
            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(CommonRequest.GetGetAssetDigestMessageRequest(Client), Client, RequestType.GetBuddyWalked)).ConfigureAwait(false);

            Tuple<GetAssetDigestResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse> response =
                await
                    PostProtoPayload
                        <Request, GetAssetDigestResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse>(requestEnvelope).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

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

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getDownloadUrlsRequest, Client)).ConfigureAwait(false);

            Tuple<GetDownloadUrlsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetDownloadUrlsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<DownloadGmTemplatesResponse> DownloadGmTemplates(long basisBatchId, long batchId, int pageOffset)
        {
            var DownloadGmTemplatesRequest = new Request
            {
                RequestType = RequestType.DownloadGameMasterTemplates,
                RequestMessage = ((IMessage)new DownloadGmTemplatesMessage
                {
                    BasisBatchId = basisBatchId,
                    BatchId = batchId,
                    PageOffset = pageOffset
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(DownloadGmTemplatesRequest, Client)).ConfigureAwait(false);

            Tuple<DownloadGmTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, DownloadGmTemplatesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            /*
             * maybe not needed
             * 
            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);
            */

            return response.Item1;
        }
    }
}