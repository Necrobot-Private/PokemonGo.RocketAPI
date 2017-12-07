#region using directives

using Google.Protobuf;
using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Platform.Responses;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class CommonRequest
    {
        public async static void ProcessGetInboxResponse(Client client, GetInboxResponse getInboxResponse)
        {
            var notifcation_count = getInboxResponse.Inbox.Notifications.Count();

            switch (getInboxResponse.Result)
            {
                case GetInboxResponse.Types.Result.Unset:
                    break;
                case GetInboxResponse.Types.Result.Failure:
                    APIConfiguration.Logger.InboxStatusUpdate($"There was an error, viewing your notifications!", ConsoleColor.Red);
                    break;
                case GetInboxResponse.Types.Result.Success:
                    if (getInboxResponse.Inbox.Notifications.Count > 0)
                    {
                        APIConfiguration.Logger.InboxStatusUpdate($"We got {notifcation_count} new notification(s).", ConsoleColor.Magenta);
                        RepeatedField<string> notificationIDs = new RepeatedField<string>();
                        RepeatedField<string> categorieIDs = new RepeatedField<string>();
                        RepeatedField<Int64> createTimestampMsIDs = new RepeatedField<Int64>();

                        foreach (var notification in getInboxResponse.Inbox.Notifications)
                        {
                            notificationIDs.Add(notification.NotificationId);
                            createTimestampMsIDs.Add(notification.CreateTimestampMs);
                            categorieIDs.Add(notification.Category);
                        }

                        NotificationState notificationState = NotificationState.Viewed;
                        //await client.Misc.OptOutPushNotificationCategory(categorieIDs).ConfigureAwait(false);
                        UpdateNotificationResponse updateNotificationResponse = await client.Misc.UpdateNotification(notificationIDs, createTimestampMsIDs, notificationState).ConfigureAwait(false);
                        APIConfiguration.Logger.InboxStatusUpdate($"Notifications {updateNotificationResponse.State}.", ConsoleColor.Magenta);
                    }
                    break;
            }
        }

        public static Request GetDownloadRemoteConfigVersionMessageRequest(Client client)
        {
            var downloadRemoteConfigVersionMessage = new DownloadRemoteConfigVersionMessage
            {
                Platform = client.Platform,
                AppVersion = client.AppVersion
            };
            return new Request
            {
                RequestType = RequestType.DownloadRemoteConfigVersion,
                RequestMessage = downloadRemoteConfigVersionMessage.ToByteString()
            };
        }

        public static Request GetGetAssetDigestMessageRequest(Client client)
        {
            var getAssetDigestMessage = new GetAssetDigestMessage
            {
                Platform = client.Platform,
                AppVersion = client.AppVersion
            };
            return new Request
            {
                RequestType = RequestType.GetAssetDigest,
                RequestMessage = getAssetDigestMessage.ToByteString()
            };
        }

        public static Request GetDownloadSettingsMessageRequest(Client client)
        {
            var downloadSettingsMessage = new DownloadSettingsMessage
            {
                Hash = client.SettingsHash
            };
            return new Request
            {
                RequestType = RequestType.DownloadSettings,
                RequestMessage = downloadSettingsMessage.ToByteString()
            };
        }

        public static Request GetDefaultGetHoloInventoryMessage(Client client)
        {
            var getHoloInventoryMessage = new GetHoloInventoryMessage
            {
                LastTimestampMs = client.InventoryLastUpdateTimestamp
            };
            return new Request
            {
                RequestType = RequestType.GetHoloInventory,
                RequestMessage = getHoloInventoryMessage.ToByteString()
            };
        }

        public static Request[] AppendCheckChallenge(Request request)
        {
            return new[]
            {
                request,
                new Request
                {
                    RequestType = RequestType.CheckChallenge,
                    RequestMessage = new CheckChallengeMessage().ToByteString()
                }
            };
        }

        public static Request GetVerifyChallenge(string token)
        {
            return new Request
            {
                RequestType = RequestType.VerifyChallenge,
                RequestMessage = new VerifyChallengeMessage()
                {
                    Token = token,

                }.ToByteString()
            };
        }

        public static List<Request> FillRequest(Request request, Client client, params RequestType[] excludes)
        {
            var requests = GetCommonRequests(client, excludes);
            requests.Insert(0, request);

            return requests;
        }

        public static List<Request> GetCommonRequests(Client client, params RequestType[] excludes)
        {
            List<Request> commonRequestsList = new List<Request>
            {
                new Request
                {
                    RequestType = RequestType.CheckChallenge,
                    RequestMessage = new CheckChallengeMessage().ToByteString()
                },
                new Request
                {
                    RequestType = RequestType.GetHatchedEggs,
                    RequestMessage = new GetHatchedEggsMessage().ToByteString()
                },
                GetDefaultGetHoloInventoryMessage(client),
                new Request
                {
                    RequestType = RequestType.CheckAwardedBadges,
                    RequestMessage = new CheckAwardedBadgesMessage().ToByteString()
                },
                GetDownloadSettingsMessageRequest(client),
                new Request
                {
                    RequestType = RequestType.GetBuddyWalked,
                    RequestMessage = new GetBuddyWalkedMessage().ToByteString()
                }
            };
            return commonRequestsList.Where(r => !excludes.Contains(r.RequestType)).ToList();
        }

        public static void ProcessGetHoloInventoryResponse(Client client, GetHoloInventoryResponse getHoloInventoryResponse)
        {
            if (getHoloInventoryResponse == null)
                return;

            if (getHoloInventoryResponse.Success)
            {
                if (getHoloInventoryResponse.InventoryDelta == null)
                    return;

                if (getHoloInventoryResponse.InventoryDelta.NewTimestampMs >= client.InventoryLastUpdateTimestamp)
                {
                    client.InventoryLastUpdateTimestamp = getHoloInventoryResponse.InventoryDelta.NewTimestampMs;
                }

                client.Inventory.MergeWith(getHoloInventoryResponse);
            }
        }

        public static void ProcessDownloadSettingsResponse(Client client, DownloadSettingsResponse downloadSettingsResponse)
        {
            if (downloadSettingsResponse == null)
                return;

            if (string.IsNullOrEmpty(downloadSettingsResponse.Error))
            {
                if (downloadSettingsResponse.Settings == null)
                    return;

                client.SettingsHash = downloadSettingsResponse.Hash;
                client.GlobalSettings = downloadSettingsResponse.Settings;
                if (!string.IsNullOrEmpty(downloadSettingsResponse.Settings.MinimumClientVersion))
                {
                    client.MinimumClientVersion = new Version(downloadSettingsResponse.Settings.MinimumClientVersion);
                    // ranamed to UseCustomAPI if (!client.Settings.UseLegacyAPI)
                    //{
                        if (client.CheckCurrentVersionOutdated())
                            throw new MinimumClientVersionException(client.CurrentApiEmulationVersion, client.MinimumClientVersion);
                    //}
                }
            }
        }

        public static void ProcessCheckChallengeResponse(Client client, CheckChallengeResponse checkChallengeResponse)
        {
            if (checkChallengeResponse == null)
                return;

            if (checkChallengeResponse.ShowChallenge)
                throw new CaptchaException(checkChallengeResponse.ChallengeUrl);
        }

        public static void ProcessGetPlayerResponse(Client client, GetPlayerResponse getPlayerResponse)
        {
            if (getPlayerResponse == null)
                return;

            if (getPlayerResponse.Banned)
                APIConfiguration.Logger.LogError("Error: This account seems be banned");

            if (getPlayerResponse.Warn)
                APIConfiguration.Logger.LogInfo("Warning: This account seems be flagged, it's recommended to not bot on this account for now!");

            if (getPlayerResponse.PlayerData != null)
                client.Player.PlayerData = getPlayerResponse.PlayerData;
        }

        public static void ProcessPlatform8Response(Client client, ResponseEnvelope responseEnvelope)
        {
            foreach (var platformReturn in responseEnvelope.PlatformReturns)
            {
                if (platformReturn.Type == POGOProtos.Networking.Platform.PlatformRequestType.UnknownPtr8)
                {
                    var ptr8Response = new UnknownPtr8Response();
                    ptr8Response.MergeFrom(platformReturn.Response);
                    client.UnknownPlat8Field = ptr8Response.Message;
                }
            }
        }
    }
}