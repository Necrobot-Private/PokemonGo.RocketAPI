#region using directives

using System.Threading.Tasks;
using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using System;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class Misc : BaseRpc
    {
        public Misc(Client client) : base(client)
        {
        }


        public  ClaimCodenameResponse ClaimCodename(string codename)
        {
            var claimCodenameRequest = new Request
            {
                RequestType = RequestType.ClaimCodename,
                RequestMessage = ((IMessage)new ClaimCodenameMessage
                {
                    Codename = codename
                }).ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(claimCodenameRequest, Client));

            Tuple<ClaimCodenameResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, ClaimCodenameResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }
        
        public  EchoResponse SendEcho()
        {
            var sendEchoRequest = new Request
            {
                RequestType = RequestType.Echo,
                RequestMessage = ((IMessage)new EchoMessage()).ToByteString()
            };

            var request =  GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(sendEchoRequest, Client));

            Tuple<EchoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, EchoResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }

        public  EncounterTutorialCompleteResponse MarkTutorialComplete(
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

            var request = GetRequestBuilder().GetRequestEnvelope(CommonRequest.FillRequest(markTutorialCompleteRequest, Client));

            Tuple<EncounterTutorialCompleteResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse> response =
                
                    PostProtoPayload
                        <Request, EncounterTutorialCompleteResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse,
                            CheckAwardedBadgesResponse, DownloadSettingsResponse, GetBuddyWalkedResponse>(request);

            CheckChallengeResponse checkChallengeResponse = response.Item2;
            CommonRequest.ProcessCheckChallengeResponse(Client, checkChallengeResponse);

            GetInventoryResponse getInventoryResponse = response.Item4;
            CommonRequest.ProcessGetInventoryResponse(Client, getInventoryResponse);

            DownloadSettingsResponse downloadSettingsResponse = response.Item6;
            CommonRequest.ProcessDownloadSettingsResponse(Client, downloadSettingsResponse);

            return response.Item1;
        }


        private readonly Random randomizer = new Random();
        public  void RandomAPICall()
        {

            var apiIndex = randomizer.Next(0, 3);

            switch (apiIndex)
            {
                case 1:
                     Client.Inventory.GetInventory();
                    break;

                case 2:
                     Client.Player.CheckChallenge();
                    break;

                case 3:
                     Client.Player.GetNewlyAwardedBadges();
                    break;
                case 4:
                     Client.Player.GetPlayerProfile();
                    break;
            }

        }
    }
}