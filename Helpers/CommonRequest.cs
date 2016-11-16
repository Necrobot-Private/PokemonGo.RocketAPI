#region using directives

using Google.Protobuf;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Exceptions;
using System;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class CommonRequest
    {
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

        public static Request GetDefaultGetInventoryMessage(Client client)
        {
            var getInventoryMessage = new GetInventoryMessage
            {
                LastTimestampMs = client.InventoryLastUpdateTimestamp
            };
            return new Request
            {
                RequestType = RequestType.GetInventory,
                RequestMessage = getInventoryMessage.ToByteString()
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
                    Token = token
                }.ToByteString()
            };
        }

        public static Request[] FillRequest(Request request, Client client)
        {
            return new[]
            {
                request,
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
                GetDefaultGetInventoryMessage(client),
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
        }

        public static Request[] GetCommonRequests(Client client)
        {
            return new[]
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
                GetDefaultGetInventoryMessage(client),
                new Request
                {
                    RequestType = RequestType.CheckAwardedBadges,
                    RequestMessage = new CheckAwardedBadgesMessage().ToByteString()
                },
                GetDownloadSettingsMessageRequest(client)
            };
        }

        public static void ProcessGetInventoryResponse(Client client, GetInventoryResponse getInventoryResponse)
        {
            if (getInventoryResponse == null)
                return;

            if (getInventoryResponse.Success)
            {
                if (getInventoryResponse.InventoryDelta == null)
                    return;

                if (getInventoryResponse.InventoryDelta.NewTimestampMs >= client.InventoryLastUpdateTimestamp)
                {
                    client.InventoryLastUpdateTimestamp = getInventoryResponse.InventoryDelta.NewTimestampMs;
                }
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

                if (!string.IsNullOrEmpty(downloadSettingsResponse.Settings.MinimumClientVersion))
                {
                    client.MinimumClientVersion = new Version(downloadSettingsResponse.Settings.MinimumClientVersion);
                    if (client.CheckCurrentVersionOutdated())
                        throw new MinimumClientVersionException(client.CurrentApiEmulationVersion, client.MinimumClientVersion);
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

        public static void Parse(Client client, RequestType requestType, ByteString data)
        {
            try
            {
                switch (requestType)
                {
                    case RequestType.GetInventory:
                        //TODO Update inventory
                        //client..getInventories().updateInventories(GetInventoryResponse.parseFrom(data));

                        //var getInventoryResponse = new GetInventoryResponse();
                        //getInventoryResponse.MergeFrom(data);

                        // Update inventory timestamp
                        client.InventoryLastUpdateTimestamp = Utils.GetTime(true);

                        break;
                    case RequestType.DownloadSettings:
                        //TODO Update settings
                        //api.getSettings().updateSettings(DownloadSettingsResponse.parseFrom(data));

                        // Update settings hash
                        var downloadSettingsResponse = new DownloadSettingsResponse();
                        downloadSettingsResponse.MergeFrom(data);
                        client.SettingsHash = downloadSettingsResponse.Hash;

                        break;
                    default:
                        break;
                }
            }
            catch (InvalidProtocolBufferException e)
            {
                throw e;
            }
        }
    }
}