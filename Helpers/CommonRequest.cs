#region using directives

using Google.Protobuf;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

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
                Hash = client.ApiSettings.Hash
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
                LastTimestampMs = client.Inventories.LastInventoryUpdate
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
                GetDownloadSettingsMessageRequest(client)
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

        public static void Parse(Client client, RequestType requestType, ByteString data)
        {
            try
            {
                switch (requestType)
                {
                    case RequestType.GetInventory:
                        var getInventoryResponse = new GetInventoryResponse();
                        getInventoryResponse.MergeFrom(data);

                        client.Inventories.UpdateInventories(getInventoryResponse);
                        
                        break;
                    case RequestType.DownloadSettings:
                        var downloadSettingsResponse = new DownloadSettingsResponse();
                        downloadSettingsResponse.MergeFrom(data);

                        client.ApiSettings.UpdateSettings(downloadSettingsResponse);

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
