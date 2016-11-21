#region using directives

using Google.Protobuf;
using POGOProtos.Networking.Envelopes;
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
                LastTimestampMs = client.Inventory.LastInventoryTimestampMs
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

        public static void ProcessGetInventoryResponse(Client client, GetInventoryResponse getInventoryResponse)
        {
            if (getInventoryResponse == null)
                return;

            if (getInventoryResponse.Success)
            {
                if (getInventoryResponse.InventoryDelta.NewTimestampMs >= client.Inventory.LastInventoryTimestampMs)
                {
                    client.Inventory.LastInventoryTimestampMs = getInventoryResponse.InventoryDelta.NewTimestampMs;

                    if (getInventoryResponse.InventoryDelta?.InventoryItems?.Count > 0)
                    {
                        client.Inventory.UpdateInventoryItems(getInventoryResponse.InventoryDelta);
                    }
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

        public static void HandleResponseEnvelope(Client client, RequestEnvelope requestEnvelope, ResponseEnvelope responseEnvelope)
        {
            if (responseEnvelope.Returns.Count != requestEnvelope.Requests.Count)
                throw new InvalidResponseException();

            var responseIndex = 0;
            foreach(var request in requestEnvelope.Requests)
            {
                var bytes = responseEnvelope.Returns[responseIndex];
                switch (request.RequestType)
                {
                    case RequestType.GetHatchedEggs: // Get_Hatched_Eggs
                        var hatchedEggs = GetHatchedEggsResponse.Parser.ParseFrom(bytes);
                        if (hatchedEggs.Success)
                        {
                            // TODO: Throw event, wrap in an object.
                        }
                        break;

                    case RequestType.GetInventory: // Get_Inventory
                        GetInventoryResponse inventoryResponse = GetInventoryResponse.Parser.ParseFrom(bytes);
                        ProcessGetInventoryResponse(client, inventoryResponse);
                        break;

                    case RequestType.ReleasePokemon:
                        var releaseResponse = ReleasePokemonResponse.Parser.ParseFrom(bytes);
                        if (releaseResponse.Result == ReleasePokemonResponse.Types.Result.Success ||
                            releaseResponse.Result == ReleasePokemonResponse.Types.Result.Failed)
                        {
                            var releaseMessage = ReleasePokemonMessage.Parser.ParseFrom(request.RequestMessage);
                            var pokemonId = releaseMessage.PokemonId;
                            var pokemons = client.Inventory.InventoryItems.Where(i => i?.InventoryItemData?.PokemonData != null && i.InventoryItemData.PokemonData.Id.Equals(pokemonId));
                            client.Inventory.RemoveInventoryItems(pokemons);
                        }
                        break;

                    case RequestType.EvolvePokemon:
                        var evolveResponse = EvolvePokemonResponse.Parser.ParseFrom(bytes);
                        if (evolveResponse.Result == EvolvePokemonResponse.Types.Result.Success ||
                            evolveResponse.Result == EvolvePokemonResponse.Types.Result.FailedPokemonMissing)
                        {
                            var releaseMessage = ReleasePokemonMessage.Parser.ParseFrom(request.RequestMessage);
                            var pokemonId = releaseMessage.PokemonId;
                            var pokemons = client.Inventory.InventoryItems.Where(i => i?.InventoryItemData?.PokemonData != null && i.InventoryItemData.PokemonData.Id.Equals(pokemonId));
                            client.Inventory.RemoveInventoryItems(pokemons);
                        }
                        break;

                    case RequestType.CheckAwardedBadges: // Check_Awarded_Badges
                        var awardedBadges = CheckAwardedBadgesResponse.Parser.ParseFrom(bytes);
                        if (awardedBadges.Success)
                        {
                            // TODO: Throw event, wrap in an object.
                        }
                        break;

                    case RequestType.DownloadSettings: // Download_Settings
                        DownloadSettingsResponse downloadSettings = DownloadSettingsResponse.Parser.ParseFrom(bytes);
                        ProcessDownloadSettingsResponse(client, downloadSettings);
                        break;

                    case RequestType.CheckChallenge:
                        CheckChallengeResponse checkChallenge = CheckChallengeResponse.Parser.ParseFrom(bytes);
                        CommonRequest.ProcessCheckChallengeResponse(client, checkChallenge);
                        break;
                }
                responseIndex++;
            }
        }
        

        public static void HandleInventoryResponses(Client client, Request request, ByteString requestResponse)
        {
            ulong pokemonId = 0;
            switch (request.RequestType)
            {
                case RequestType.ReleasePokemon:
                    var releaseResponse = ReleasePokemonResponse.Parser.ParseFrom(requestResponse);
                    if (releaseResponse.Result == ReleasePokemonResponse.Types.Result.Success ||
                        releaseResponse.Result == ReleasePokemonResponse.Types.Result.Failed)
                    {
                        var releaseMessage = ReleasePokemonMessage.Parser.ParseFrom(request.RequestMessage);
                        pokemonId = releaseMessage.PokemonId;
                    }
                    break;

                case RequestType.EvolvePokemon:
                    var evolveResponse = EvolvePokemonResponse.Parser.ParseFrom(requestResponse);
                    if (evolveResponse.Result == EvolvePokemonResponse.Types.Result.Success ||
                        evolveResponse.Result == EvolvePokemonResponse.Types.Result.FailedPokemonMissing)
                    {
                        var releaseMessage = ReleasePokemonMessage.Parser.ParseFrom(request.RequestMessage);
                        pokemonId = releaseMessage.PokemonId;
                    }
                    break;

                default:
                    // If not one of the above cases, just return.
                    return;
            }

            if (pokemonId > 0)
            {
                var pokemons = client.Inventory.InventoryItems.Where(
                    i =>
                        i?.InventoryItemData?.PokemonData != null &&
                        i.InventoryItemData.PokemonData.Id.Equals(pokemonId));
                client.Inventory.RemoveInventoryItems(pokemons);
            }
        }
    }
}