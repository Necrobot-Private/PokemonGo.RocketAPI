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

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Player : BaseRpc
    {
        public Player(Client client) : base(client)
        {
        }

        public async Task<PlayerUpdateResponse> UpdatePlayerLocation(double latitude, double longitude, double altitude, float speed)
        {
            SetCoordinates(latitude, longitude, altitude);
            SetSpeed(speed);

            var message = new PlayerUpdateMessage
            {
                Latitude = Client.CurrentLatitude,
                Longitude = Client.CurrentLongitude    ,
                
            };

            var updatePlayerLocationRequestEnvelope = await GetRequestBuilder().GetRequestEnvelope(new Request[] {
                new Request
                {
                    RequestType = RequestType.PlayerUpdate,
                    RequestMessage = message.ToByteString()
                }
            });

            return await PostProtoPayload<Request, PlayerUpdateResponse>(updatePlayerLocationRequestEnvelope);
        }

        internal void SetCoordinates(double lat, double lng, double altitude)
        {
            Client.CurrentLatitude = lat;
            Client.CurrentLongitude = lng;
            Client.CurrentAltitude = altitude;
        }

        internal void SetSpeed(float speed)
        {
            Client.CurrentSpeed = speed;
        }

        public async Task<GetPlayerResponse> GetPlayer()
        {
            var getPlayerRequest = new Request
            {
                RequestType = RequestType.GetPlayer,
                RequestMessage = new GetPlayerMessage().ToByteString()
            };

            var request = await GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(getPlayerRequest, Client));

            Tuple<GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                await
                    PostProtoPayload
                        <Request, GetPlayerResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            return response.Item1;
        }

        public async Task<GetPlayerProfileResponse> GetPlayerProfile(string playerName)
        {
            return
                await
                    PostProtoPayload<Request, GetPlayerProfileResponse>(RequestType.GetPlayerProfile,
                        new GetPlayerProfileMessage
                        {
                            PlayerName = playerName
                        });
        }

        public async Task<CheckAwardedBadgesResponse> GetNewlyAwardedBadges()
        {
            return
                await
                    PostProtoPayload<Request, CheckAwardedBadgesResponse>(RequestType.CheckAwardedBadges,
                        new CheckAwardedBadgesMessage());
        }

        public async Task<CollectDailyBonusResponse> CollectDailyBonus()
        {
            return
                await
                    PostProtoPayload<Request, CollectDailyBonusResponse>(RequestType.CollectDailyBonus,
                        new CollectDailyBonusMessage());
        }

        public async Task<CollectDailyDefenderBonusResponse> CollectDailyDefenderBonus()
        {
            return
                await
                    PostProtoPayload<Request, CollectDailyDefenderBonusResponse>(RequestType.CollectDailyDefenderBonus,
                        new CollectDailyDefenderBonusMessage());
        }

        public async Task<EquipBadgeResponse> EquipBadge(BadgeType type)
        {
            return
                await
                    PostProtoPayload<Request, EquipBadgeResponse>(RequestType.EquipBadge,
                        new EquipBadgeMessage {BadgeType = type});
        }

        public async Task<LevelUpRewardsResponse> GetLevelUpRewards(int level)
        {
            return
                await
                    PostProtoPayload<Request, LevelUpRewardsResponse>(RequestType.LevelUpRewards,
                        new LevelUpRewardsMessage
                        {
                            Level = level
                        });
        }

        public async Task<SetAvatarResponse> SetAvatar(PlayerAvatar playerAvatar)
        {
            return await PostProtoPayload<Request, SetAvatarResponse>(RequestType.SetAvatar, new SetAvatarMessage
            {
                PlayerAvatar = playerAvatar
            });
        }

        public async Task<SetContactSettingsResponse> SetContactSetting(ContactSettings contactSettings)
        {
            return
                await
                    PostProtoPayload<Request, SetContactSettingsResponse>(RequestType.SetContactSettings,
                        new SetContactSettingsMessage
                        {
                            ContactSettings = contactSettings
                        });
        }

        public async Task<SetPlayerTeamResponse> SetPlayerTeam(TeamColor teamColor)
        {
            return
                await
                    PostProtoPayload<Request, SetPlayerTeamResponse>(RequestType.SetPlayerTeam, new SetPlayerTeamMessage
                    {
                        Team = teamColor
                    });
        }

        public async Task<CheckChallengeResponse> CheckChallenge()
        {
            return await PostProtoPayload<Request, CheckChallengeResponse>(RequestType.CheckChallenge, new CheckChallengeMessage() { });
        }

        public async Task<VerifyChallengeResponse> VerifyChallenge(string token)
        {
            return await PostProtoPayload<Request, VerifyChallengeResponse>(RequestType.VerifyChallenge, new VerifyChallengeMessage() { Token = token });
        }
    }
}