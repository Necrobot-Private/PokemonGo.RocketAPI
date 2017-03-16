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
    public class Player : BaseRpc
    {
        public Player(Client client) : base(client)
        {
        }

        public PlayerData PlayerData { get; set; }

        public void UpdatePlayerLocation(double latitude, double longitude, double altitude, float speed)
        {
            SetCoordinates(latitude, longitude, altitude);
            SetSpeed(speed);

            return;

        }

        public  SetBuddyPokemonResponse SelectBuddy(ulong pokemonId)
        {
            var selectBuddyRequest = new Request
            {
                RequestType = RequestType.SetBuddyPokemon,
                RequestMessage = new SetBuddyPokemonMessage
                {
                    PokemonId = pokemonId
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(selectBuddyRequest, Client));

            Tuple<SetBuddyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                    PostProtoPayload
                        <Request, SetBuddyPokemonResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

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

        public  GetPlayerResponse GetPlayer(bool addCommonRequests = true)
        {
            var getPlayerRequest = new Request
            {
                RequestType = RequestType.GetPlayer,
                RequestMessage = new GetPlayerMessage().ToByteString()
            };
            
            if (addCommonRequests)
            {
                var requestEnvelope =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getPlayerRequest, Client));

                Tuple<GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                    
                        PostProtoPayload
                            <Request, GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                                CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(requestEnvelope);

                GetPlayerResponse getPlayerResponse = response.Item1;
                CommonRequest.ProcessGetPlayerResponse(Client, getPlayerResponse);

                CheckChallengeResponse checkChallengeResponse = response.Item2;
                CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

                GetInventoryResponse getInventoryResponse = response.Item4;
                CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

                DownloadSettingsResponse downloadSettingsResponse = response.Item6;
                CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);
                
                return response.Item1;
            }
            else
            {
                var requestEnvelope =  GetRequestBuilder().GetRequestEnvelope(new Request[] { getPlayerRequest });
                GetPlayerResponse getPlayerResponse =  PostProtoPayload<Request, GetPlayerResponse>(requestEnvelope);
                CommonRequest.ProcessGetPlayerResponse(Client, getPlayerResponse);

                return getPlayerResponse;
            }
        }

        public  GetPlayerProfileResponse GetPlayerProfile()
        {
            var getPlayerProfileRequest = new Request
            {
                RequestType = RequestType.GetPlayerProfile,
                RequestMessage = new GetPlayerProfileMessage
                {
                    PlayerName = PlayerData.Username
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getPlayerProfileRequest, Client));

            Tuple<GetPlayerProfileResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, GetPlayerProfileResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  CheckAwardedBadgesResponse GetNewlyAwardedBadges()
        {
            var getNewlyAwardedBadgesRequest = new Request
            {
                RequestType = RequestType.CheckAwardedBadges,
                RequestMessage = new CheckAwardedBadgesMessage().ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getNewlyAwardedBadgesRequest, Client));

            Tuple<CheckAwardedBadgesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, CheckAwardedBadgesResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  CollectDailyBonusResponse CollectDailyBonus()
        {
            var collectDailyBonusRequest = new Request
            {
                RequestType = RequestType.CollectDailyBonus,
                RequestMessage = new CollectDailyBonusMessage().ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(collectDailyBonusRequest, Client));

            Tuple<CollectDailyBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, CollectDailyBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  CollectDailyDefenderBonusResponse CollectDailyDefenderBonus()
        {
            var collectDailyDefenderBonusRequest = new Request
            {
                RequestType = RequestType.CollectDailyDefenderBonus,
                RequestMessage = new CollectDailyDefenderBonusMessage().ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(collectDailyDefenderBonusRequest, Client));

            Tuple<CollectDailyDefenderBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, CollectDailyDefenderBonusResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  EquipBadgeResponse EquipBadge(BadgeType type)
        {
            var equipBadgeRequest = new Request
            {
                RequestType = RequestType.EquipBadge,
                RequestMessage = new EquipBadgeMessage
                {
                    BadgeType = type
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(equipBadgeRequest, Client));

            Tuple<EquipBadgeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, EquipBadgeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  LevelUpRewardsResponse GetLevelUpRewards(int level)
        {
            var levelUpRewardsRequest = new Request
            {
                RequestType = RequestType.LevelUpRewards,
                RequestMessage = new LevelUpRewardsMessage
                {
                    Level = level
                }.ToByteString()
            };
            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(levelUpRewardsRequest, Client));

            Tuple<LevelUpRewardsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, LevelUpRewardsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  SetAvatarResponse SetAvatar(PlayerAvatar playerAvatar)
        {
            var setAvatarRequest = new Request
            {
                RequestType = RequestType.SetAvatar,
                RequestMessage = new SetAvatarMessage
                {
                    PlayerAvatar = playerAvatar
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setAvatarRequest, Client));

            Tuple<SetAvatarResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, SetAvatarResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  SetContactSettingsResponse SetContactSetting(ContactSettings contactSettings)
        {
            var setContactSettingRequest = new Request
            {
                RequestType = RequestType.SetContactSettings,
                RequestMessage = new SetContactSettingsMessage
                {
                    ContactSettings = contactSettings
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setContactSettingRequest, Client));

            Tuple<SetContactSettingsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, SetContactSettingsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  SetPlayerTeamResponse SetPlayerTeam(TeamColor teamColor)
        {
            var setPlayerTeamRequest = new Request
            {
                RequestType = RequestType.SetPlayerTeam,
                RequestMessage = new SetPlayerTeamMessage
                {
                    Team = teamColor
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(setPlayerTeamRequest, Client));

            Tuple<SetPlayerTeamResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, SetPlayerTeamResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  CheckChallengeResponse CheckChallenge()
        {
            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.GetCommonRequests(Client));

            Tuple<CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            // This is commented out because the assumption is that you are calling CheckChallenge() directly 
            // to get a new challenge url. So don't throw the exception below.
            // CheckChallengeResponse checkChallengeResponse = response.Item1;
            // CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item3;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item5;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  VerifyChallengeResponse VerifyChallenge(string token)
        {
            var verifyChallengeRequest = new Request
            {
                RequestType = RequestType.VerifyChallenge,
                RequestMessage = new VerifyChallengeMessage
                {
                    Token = token
                }.ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(verifyChallengeRequest, Client));

            Tuple<VerifyChallengeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, VerifyChallengeResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            // This is commented out because the assumption is that you are trying to verify the captcha,
            // so don't throw any exceptions.
            // CheckChallengeResponse checkChallengeResponse = response.Item2;
            // CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
    }
}