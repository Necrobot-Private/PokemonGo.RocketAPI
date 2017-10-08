#region using directives

using System.Threading.Tasks;
using Google.Protobuf;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using System;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Data;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    /// <summary>
    /// Description of LocaleInfo.
    /// </summary>
    public static class LocaleInfo
    {
        public static string Country = "US";
        public static string Language = "en";
        public static string TimeZone = "America/New_York";
        public static string POSIX = "en-us";

        public static void SetValues(string country, string language, string timezone, string posix)
        {
            Country = country;
            Language = language;
            TimeZone = timezone;
            POSIX = posix;
        }
    }

    public class Player : BaseRpc
    {
        public Player(Client client) : base(client)
        {
            LocaleInfo.SetValues(client.Settings.Country, client.Settings.Language, client.Settings.TimeZone, client.Settings.POSIX);
        }

        public PlayerData PlayerData { get; set; }

        public void UpdatePlayerLocation(double latitude, double longitude, double altitude, float speed)
        {
            SetCoordinates(latitude, longitude, altitude);
            SetSpeed(speed);

            return;

            //var updatePlayerLocationRequest = new Request
            //{
            //    RequestType = RequestType.PlayerUpdate,
            //    RequestMessage = new PlayerUpdateMessage
            //    {
            //        Latitude = Client.CurrentLatitude,
            //        Longitude = Client.CurrentLongitude
            //    }.ToByteString()
            //};

            //var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(updatePlayerLocationRequest, Client)).ConfigureAwait(false);

            //Tuple<PlayerUpdateResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
            //    await
            //        PostProtoPayload
            //            <Request, PlayerUpdateResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
            //                CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            //CheckChallengeResponse checkChallengeResponse = response.Item2;
            //CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            //GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            //CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            //DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            //CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            //return response.Item1;
        }

        public async Task<SetBuddyPokemonResponse> SelectBuddy(ulong pokemonId)
        {
            var selectBuddyRequest = new Request
            {
                RequestType = RequestType.SetBuddyPokemon,
                RequestMessage = new SetBuddyPokemonMessage
                {
                    PokemonId = pokemonId
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(selectBuddyRequest, Client)).ConfigureAwait(false);

            Tuple<SetBuddyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetBuddyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public void SetCoordinates(double lat, double lng, double altitude)
        {
            Client.CurrentLatitude = lat;
            Client.CurrentLongitude = lng;
            Client.CurrentAltitude = altitude;
        }

        internal void SetSpeed(float speed)
        {
            Client.CurrentSpeed = speed;
        }

        public async Task<GetPlayerResponse> GetPlayer(bool addCommonRequests = true, bool addChallengeRequests = false)
        {
            var locale = new GetPlayerMessage.Types.PlayerLocale
            {
                Country =  LocaleInfo.Country,
                Language = LocaleInfo.Language,
                Timezone = LocaleInfo.TimeZone
            };

            var getPlayerRequest = new Request
            {
                RequestType = RequestType.GetPlayer,
                RequestMessage = new GetPlayerMessage()
                {
                    PlayerLocale = locale
                }.ToByteString()
            };
            
            if (addCommonRequests)
            {
                var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getPlayerRequest, Client)).ConfigureAwait(false);

                Tuple<GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                    await
                        PostProtoPayload
                            <Request, GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                                CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(requestEnvelope).ConfigureAwait(false);

                GetPlayerResponse getPlayerResponse = response.Item1;
                CommonRequest.ProcessGetPlayerResponse(Client, getPlayerResponse);

                CheckChallengeResponse checkChallengeResponse = response.Item2;
                CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

                GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
                CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

                DownloadSettingsResponse downloadSettingsResponse = response.Item6;
                CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);
                
                return response.Item1;
            }
            if (addChallengeRequests)
            {
                var challengeRequest = new Request
                {
                    RequestType = RequestType.CheckChallenge,
                    RequestMessage = new CheckChallengeMessage().ToByteString()
                };
                var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(new Request[] { getPlayerRequest,challengeRequest }).ConfigureAwait(false);
                Tuple<GetPlayerResponse,CheckChallengeResponse> response = await PostProtoPayload<Request, GetPlayerResponse, CheckChallengeResponse>(requestEnvelope).ConfigureAwait(false);
                CommonRequest.ProcessGetPlayerResponse(Client, response.Item1);

                return response.Item1;
            }
			else
            {
                var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(new Request[] { getPlayerRequest }).ConfigureAwait(false);
                GetPlayerResponse getPlayerResponse = await PostProtoPayload<Request, GetPlayerResponse>(requestEnvelope).ConfigureAwait(false);
                CommonRequest.ProcessGetPlayerResponse(Client, getPlayerResponse);

                return getPlayerResponse;
            }
        }

        public async Task<GetPlayerProfileResponse> GetPlayerProfile()
        {
            var getPlayerProfileRequest = new Request
            {
                RequestType = RequestType.GetPlayerProfile,
                RequestMessage = new GetPlayerProfileMessage
                {
                    PlayerName = PlayerData.Username
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getPlayerProfileRequest, Client)).ConfigureAwait(false);

            Tuple<GetPlayerProfileResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetPlayerProfileResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L32
        public async Task<CheckAwardedBadgesResponse> GetNewlyAwardedBadges()
        {
            var getNewlyAwardedBadgesRequest = new Request
            {
                RequestType = RequestType.CheckAwardedBadges,
                RequestMessage = new CheckAwardedBadgesMessage().ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getNewlyAwardedBadgesRequest, Client)).ConfigureAwait(false);

            Tuple<CheckAwardedBadgesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CheckAwardedBadgesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L38
        public async Task<CollectDailyBonusResponse> CollectDailyBonus()
        {
            var collectDailyBonusRequest = new Request
            {
                RequestType = RequestType.CollectDailyBonus,
                RequestMessage = new CollectDailyBonusMessage().ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(collectDailyBonusRequest, Client)).ConfigureAwait(false);

            Tuple<CollectDailyBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CollectDailyBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //TODO: revise https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L46
        public async Task<CollectDailyDefenderBonusResponse> CollectDailyDefenderBonus()
        {
            var collectDailyDefenderBonusRequest = new Request
            {
                RequestType = RequestType.CollectDailyDefenderBonus,
                RequestMessage = new CollectDailyDefenderBonusMessage().ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(collectDailyDefenderBonusRequest, Client)).ConfigureAwait(false);

            Tuple<CollectDailyDefenderBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CollectDailyDefenderBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<EquipBadgeResponse> EquipBadge(BadgeType type)
        {
            var equipBadgeRequest = new Request
            {
                RequestType = RequestType.EquipBadge,
                RequestMessage = new EquipBadgeMessage
                {
                    BadgeType = type
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(equipBadgeRequest, Client)).ConfigureAwait(false);

            Tuple<EquipBadgeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, EquipBadgeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<LevelUpRewardsResponse> GetLevelUpRewards(int level)
        {
            var levelUpRewardsRequest = new Request
            {
                RequestType = RequestType.LevelUpRewards,
                RequestMessage = new LevelUpRewardsMessage
                {
                    Level = level
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(levelUpRewardsRequest, Client)).ConfigureAwait(false);

            Tuple<LevelUpRewardsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, LevelUpRewardsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetAvatarResponse> SetAvatar(PlayerAvatar playerAvatar)
        {
            var setAvatarRequest = new Request
            {
                RequestType = RequestType.SetAvatar,
                RequestMessage = new SetAvatarMessage
                {
                    PlayerAvatar = playerAvatar
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setAvatarRequest, Client)).ConfigureAwait(false);

            Tuple<SetAvatarResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetAvatarResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetContactSettingsResponse> SetContactSetting(ContactSettings contactSettings)
        {
            var setContactSettingRequest = new Request
            {
                RequestType = RequestType.SetContactSettings,
                RequestMessage = new SetContactSettingsMessage
                {
                    ContactSettings = contactSettings
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setContactSettingRequest, Client)).ConfigureAwait(false);

            Tuple<SetContactSettingsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetContactSettingsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<SetPlayerTeamResponse> SetPlayerTeam(TeamColor teamColor)
        {
            var setPlayerTeamRequest = new Request
            {
                RequestType = RequestType.SetPlayerTeam,
                RequestMessage = new SetPlayerTeamMessage
                {
                    Team = teamColor
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setPlayerTeamRequest, Client)).ConfigureAwait(false);

            Tuple<SetPlayerTeamResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, SetPlayerTeamResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<CheckChallengeResponse> CheckChallenge()
        {
            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.GetCommonRequests(Client)).ConfigureAwait(false);

            Tuple<CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            // This is commented out because the assumption is that you are calling CheckChallenge() directly 
            // to get a new challenge url. So don't throw the exception below.
            // CheckChallengeResponse checkChallengeResponse = response.Item1;
            // CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item3;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public async Task<VerifyChallengeResponse> VerifyChallenge(string token)
        {
            var verifyChallengeRequest = new Request
            {
                RequestType = RequestType.VerifyChallenge,
                RequestMessage = new VerifyChallengeMessage
                {
                    Token = token
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(verifyChallengeRequest, Client)).ConfigureAwait(false);

            Tuple<VerifyChallengeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, VerifyChallengeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            // This is commented out because the assumption is that you are trying to verify the captcha,
            // so don't throw any exceptions.
            // CheckChallengeResponse checkChallengeResponse = response.Item2;
            // CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        //https://github.com/AeonLucid/POGOProtos/blob/master/src/POGOProtos/Networking/Requests/RequestType.proto#L83
        public async Task<ListAvatarCustomizationsResponse> ListAvatarCustomizations(PlayerAvatarType avatarType, int limit, int start)
        {
            var setPlayerTeamRequest = new Request
            {
                RequestType = RequestType.ListAvatarCustomizations,
                RequestMessage = new ListAvatarCustomizationsMessage
                {
                    AvatarType = avatarType,
                    //Filters = xxx,
                    Limit = limit,
                    //Slot = xxx,
                    Start = start
                }.ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setPlayerTeamRequest, Client)).ConfigureAwait(false);

            Tuple<ListAvatarCustomizationsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, ListAvatarCustomizationsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetHoloInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request).ConfigureAwait(false);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetHoloInventoryResponse getHoloInventoryResponse = response.Item4;
            CommonRequest.ProcessGetHoloInventoryResponse(Client, getHoloInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}