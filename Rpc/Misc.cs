﻿#region using directives

using System.Threading.Tasks;
using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

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
            return
                await
                    PostProtoPayload<Request, ClaimCodenameResponse>(RequestType.ClaimCodename,
                        new ClaimCodenameMessage
                        {
                            Codename = codename
                        });
        }
        //TODO: Removed from POGOProtos.NetStandard1
        /* 
                public async Task<CheckCodenameAvailableResponse> CheckCodenameAvailable(string codename)
                {
                    return
                        await
                            PostProtoPayload<Request, CheckCodenameAvailableResponse>(RequestType.CheckCodenameAvailable,
                                new CheckCodenameAvailableMessage
                                {
                                    Codename = codename
                                });
                }

                public async Task<GetSuggestedCodenamesResponse> GetSuggestedCodenames()
                {
                    return
                        await
                            PostProtoPayload<Request, GetSuggestedCodenamesResponse>(RequestType.GetSuggestedCodenames,
                                new GetSuggestedCodenamesMessage());
                }

      Add or moved in to AeonLucid/POGOProtos.NetStandard1 2.3.0:
         * AvatarCustomization
         * AvatarItem
         * PlayerAvatarType
         * PokemonDisplay
         * Filter
         * Slot
         * ListAvatarCustomizationsMessage
         * ListAvatarCustomizationsResponse
         * AvatarCustomizationSettings
         */
        public async Task<EchoResponse> SendEcho()
        {
            return await PostProtoPayload<Request, EchoResponse>(RequestType.Echo, new EchoMessage());
        }

        public async Task<EncounterTutorialCompleteResponse> MarkTutorialComplete(
            RepeatedField<TutorialState> toComplete)
        {
            return
                await
                    PostProtoPayload<Request, EncounterTutorialCompleteResponse>(RequestType.MarkTutorialComplete,
                        new MarkTutorialCompleteMessage
                        {
                            SendMarketingEmails = false,
                            SendPushNotifications = false,
                            TutorialsCompleted = {toComplete}
                        });
        }
    }
}