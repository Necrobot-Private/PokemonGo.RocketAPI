#region using directives

using Google.Protobuf;
using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Helpers;
using System;
using System.Threading.Tasks;
using static POGOProtos.Networking.Requests.Messages.RegisterPushNotificationMessage.Types;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Misc : BaseRpc
    {
        public Misc(Client client) : base(client)
        {
        }

        public async Task<ClaimCodenameResponse> ClaimCodename(string codename)
        {
            var claimCodenameRequest = new Request
            {
                RequestType = RequestType.ClaimCodename,
                RequestMessage = ((IMessage)new ClaimCodenameMessage
                {
                    Codename = codename
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(claimCodenameRequest, Client)).ConfigureAwait(false);

            Tuple<ClaimCodenameResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, ClaimCodenameResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
        
        public async Task<EchoResponse> SendEcho()
        {
            var sendEchoRequest = new Request
            {
                RequestType = RequestType.Echo,
                RequestMessage = ((IMessage)new EchoMessage()).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(sendEchoRequest, Client)).ConfigureAwait(false);

            Tuple<EchoResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EchoResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<EncounterTutorialCompleteResponse> MarkTutorialComplete(
            RepeatedField<TutorialState> toComplete)
        {
            var markTutorialCompleteRequest = new Request
            {
                RequestType = RequestType.MarkTutorialComplete,
                RequestMessage = ((IMessage)new MarkTutorialCompleteMessage
                {
                    SendMarketingEmails = false,
                    SendPushNotifications = false,
                    TutorialsCompleted = { toComplete }
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(markTutorialCompleteRequest, Client)).ConfigureAwait(false);

            Tuple<EncounterTutorialCompleteResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EncounterTutorialCompleteResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaRegistrationResponse> SfidaRegistration(string sfidaId)
        {
            var SfidaRegistrationRequest = new Request
            {
                RequestType = RequestType.SfidaRegistration,
                RequestMessage = ((IMessage)new SfidaRegistrationMessage
                {
                    SfidaId = sfidaId
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaRegistrationRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaRegistrationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaRegistrationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaActionLog()
        {
            var SfidaActionLogRequest = new Request
            {
                RequestType = RequestType.SfidaActionLog,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaActionLogRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaAction()
        {
            var SfidaActionRequest = new Request
            {
                RequestType = RequestType.SfidaAction,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaActionRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaCapture()
        {
            var SfidaCaptureRequest = new Request
            {
                RequestType = RequestType.SfidaCapture,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaCaptureRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaCertification()
        {
            var SfidaCertificationRequest = new Request
            {
                RequestType = RequestType.SfidaCertification,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaCertificationRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaDowser()
        {
            var SfidaDowserRequest = new Request
            {
                RequestType = RequestType.SfidaDowser,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaDowserRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SfidaActionLogResponse> SfidaUpdate()
        {
            var SfidaUpdateRequest = new Request
            {
                RequestType = RequestType.SfidaUpdate,
                RequestMessage = ((IMessage)new SfidaActionLogMessage
                {
                    //
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(SfidaUpdateRequest, Client)).ConfigureAwait(false);

            Tuple<SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SfidaActionLogResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<GetInboxResponse> GetInbox(bool isHistory, bool isReverse, long notBeforeMs)
        {
            var GetInboxRequest = new Request
            {
                RequestType = RequestType.GetInbox,
                RequestMessage = ((IMessage)new GetInboxMessage
                {
                    IsHistory = isHistory,
                    IsReverse = isReverse,
                    NotBeforeMs = notBeforeMs
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(GetInboxRequest, Client)).ConfigureAwait(false);

            Tuple<GetInboxResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetInboxResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<UpdateNotificationResponse> UpdateNotification(RepeatedField<string> Notification_Ids, RepeatedField<Int64> TimeStampsMS, NotificationState state)
        {
            var UpdateNotificationRequest = new Request
            {
                RequestType = RequestType.UpdateNotificationStatus,
                RequestMessage = ((IMessage)new UpdateNotificationMessage
                {
                    NotificationIds = { Notification_Ids },
                    CreateTimestampMs = { TimeStampsMS },
                    State = state
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(UpdateNotificationRequest, Client)).ConfigureAwait(false);

            Tuple<UpdateNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, UpdateNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<RegisterPushNotificationResponse> RegisterPushNotification(ApnToken apnToken, GcmToken gcmToken)
        {
            var RegisterPushNotificationRequest = new Request
            {
                RequestType = RequestType.RegisterPushNotification,
                RequestMessage = ((IMessage)new RegisterPushNotificationMessage
                {
                    ApnToken = apnToken,
                    GcmToken = gcmToken
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(RegisterPushNotificationRequest, Client)).ConfigureAwait(false);

            Tuple<RegisterPushNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, RegisterPushNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<RegisterPushNotificationResponse> UnregisterPushNotification(ApnToken apnToken, GcmToken gcmToken)
        {
            var UnregisterPushNotificationRequest = new Request
            {
                RequestType = RequestType.UnregisterPushNotification,
                RequestMessage = ((IMessage)new RegisterPushNotificationMessage
                {
                    ApnToken = apnToken,
                    GcmToken = gcmToken,
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(UnregisterPushNotificationRequest, Client)).ConfigureAwait(false);

            Tuple<RegisterPushNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, RegisterPushNotificationResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<OptOutPushNotificationCategoryResponse> OptOutPushNotificationCategory(RepeatedField<string> categories)
        {
            var OptOutPushNotificationCategoryRequest = new Request
            {
                RequestType = RequestType.OptOutPushNotificationCategory,
                RequestMessage = ((IMessage)new OptOutPushNotificationCategoryMessage
                {
                    Categories = { categories}
                }).ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(OptOutPushNotificationCategoryRequest, Client)).ConfigureAwait(false);

            Tuple<OptOutPushNotificationCategoryResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, OptOutPushNotificationCategoryResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            /*
             * not needed
             * 
            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);
            */

            return response.Item1;
        }

        private Random randomizer = new Random();
        public async Task RandomAPICall()
        {

            var apiIndex = randomizer.Next(0, 3);

            switch (apiIndex)
            {
                case 1:
                    await Client.Inventory.GetInventory().ConfigureAwait(false);
                    break;

                case 2:
                    await Client.Player.GetNewlyAwardedBadges().ConfigureAwait(false);
                    break;
                case 3:
                    await Client.Player.GetPlayerProfile().ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }
    }
}