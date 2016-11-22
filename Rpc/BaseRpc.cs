#region using directives

using System;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.Helpers;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;

#endregion

namespace PokemonGo.RocketAPI.Rpc
{
    public class BaseRpc
    {
        protected Client Client;

        protected BaseRpc(Client client)
        {
            Client = client;
        }

        protected RequestBuilder GetRequestBuilder()
        {
            return new RequestBuilder(Client, Client.AuthType, Client.CurrentLatitude, Client.CurrentLongitude,
                    Client.CurrentAltitude, Client.CurrentSpeed, Client.Settings);
        }

        protected async Task<TResponsePayload> PostProtoPayload<TRequest, TResponsePayload>(RequestType type,
            IMessage message, bool addCommonRequests = true, bool addGetBuddyWalked = false) where TRequest : IMessage<TRequest>
            where TResponsePayload : IMessage<TResponsePayload>, new()
        {
            var requestEnvelope = await GetRequestBuilder().GetRequestEnvelope(
                new Request[] { new Request
                    {
                        RequestType = type,
                        RequestMessage = message.ToByteString()
                    }
                },
                addCommonRequests,
                addGetBuddyWalked
            );

            return await Client.PokemonHttpClient.PostProtoPayload<TRequest, TResponsePayload>(Client, requestEnvelope);
        }
    }
}