#region using directives

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Exceptions;
using POGOProtos.Networking.Envelopes;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using PokemonGo.RocketAPI.Helpers;

#endregion

namespace PokemonGo.RocketAPI.Extensions
{
    public enum ApiOperation
    {
        Retry,
        Abort
    }

    public interface ICaptchaResponseHandler
    {
        void SetCaptchaToken(string captchaToken);
    }

    public static class HttpClientExtensions
    {
        public static  IMessage[] PostProtoPayload<TRequest>(this System.Net.Http.HttpClient client,
            Client apiClient, RequestEnvelope requestEnvelope,
            params Type[] responseTypes) where TRequest : IMessage<TRequest>
        {
            var result = new IMessage[responseTypes.Length];
            for (var i = 0; i < responseTypes.Length; i++)
            {
                result[i] = Activator.CreateInstance(responseTypes[i]) as IMessage;
                if (result[i] == null)
                {
                    throw new ArgumentException($"ResponseType {i} is not an IMessage");
                }
            }
            //ResponseEnvelope response = PerformThrottledRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);
            ResponseEnvelope response = PerformRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);

            if (response== null || (response.Returns.Count != requestEnvelope.Requests.Count))
                throw new InvalidResponseException($"Error with API request type: {requestEnvelope.Requests[0].RequestType}");

            for (var i = 0; i < responseTypes.Length; i++)
            {
                var payload = response.Returns[i];
                result[i].MergeFrom(payload);
            }
            return result;
        }

        public static  TResponsePayload PostProtoPayload<TRequest, TResponsePayload>(
            this System.Net.Http.HttpClient client,
            Client apiClient, RequestEnvelope requestEnvelope)
            where TRequest : IMessage<TRequest>
            where TResponsePayload : IMessage<TResponsePayload>, new()
        {
            // PerformRemoteProcedureCall
            //ResponseEnvelope response = PerformThrottledRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);
            ResponseEnvelope response = PerformRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);

            if (response.Returns.Count != requestEnvelope.Requests.Count)
                throw new InvalidResponseException($"Error with API request type: {requestEnvelope.Requests[0].RequestType}");

            //Decode payload
            //todo: multi-payload support
            var payload = response.Returns[0];
            var parsedPayload = new TResponsePayload();
            parsedPayload.MergeFrom(payload);

            return parsedPayload;
        }

        public static  ResponseEnvelope PerformRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client,
            Client apiClient,
            RequestEnvelope requestEnvelope) where TRequest : IMessage<TRequest>
        {
            // Check killswitch from url before making API calls.
            if (!apiClient.Settings.UseLegacyAPI)
            {
                if (apiClient.CheckCurrentVersionOutdated())
                    throw new MinimumClientVersionException(apiClient.CurrentApiEmulationVersion, apiClient.MinimumClientVersion);
            }

            //Encode payload and put in envelop, then send
            var data = requestEnvelope.ToByteString();
            var result =  client.PostAsync(apiClient.ApiUrl, new ByteArrayContent(data.ToByteArray())).Result;

            //Decode message
            var responseData = result.Content.ReadAsByteArrayAsync().Result;
            var codedStream = new CodedInputStream(responseData);
            var serverResponse = new ResponseEnvelope();
            serverResponse.MergeFrom(codedStream);

            // Process Platform8Response
            CommonRequest.ProcessPlatform8Response(apiClient, serverResponse);

            if (!string.IsNullOrEmpty(serverResponse.ApiUrl))
                apiClient.ApiUrl = "https://" + serverResponse.ApiUrl + "/rpc";

            if (serverResponse.AuthTicket != null)
            {
                apiClient.AuthTicket = serverResponse.AuthTicket.ExpireTimestampMs > (ulong)Utils.GetTime(true) ? serverResponse.AuthTicket : null;
            }

            switch (serverResponse.StatusCode)
            {
                case ResponseEnvelope.Types.StatusCode.InvalidAuthToken:
                    apiClient.RequestBuilder.RegenerateRequestEnvelopeWithNewAccessToken(requestEnvelope);
                    return PerformRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);
                case ResponseEnvelope.Types.StatusCode.Redirect:
                    // 53 means that the api_endpoint was not correctly set, should be at this point, though, so redo the request
                    return PerformRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);
                case ResponseEnvelope.Types.StatusCode.BadRequest:
                    // Your account may be banned! please try from the official client.
                    throw new APIBadRequestException("BAD REQUEST \r\n" + JsonConvert.SerializeObject(requestEnvelope));
                case ResponseEnvelope.Types.StatusCode.Unknown:
                    break;
                case ResponseEnvelope.Types.StatusCode.Ok:
                    break;
                case ResponseEnvelope.Types.StatusCode.OkRpcUrlInResponse:
                    break;
                case ResponseEnvelope.Types.StatusCode.InvalidRequest:
                    break;
                case ResponseEnvelope.Types.StatusCode.InvalidPlatformRequest:
                    break;
                case ResponseEnvelope.Types.StatusCode.SessionInvalidated:
                    throw new SessionInvalidatedException("SESSION INVALIDATED EXCEPTION");
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return serverResponse;
        }

        // RPC Calls need to be throttled 
        private static long lastRpc = 0;    // Starting at 0 to allow first RPC call to be done immediately
        private const int minDiff = 1000;   // Derived by trial-and-error. Up to 900 can cause server to complain.
        private static ConcurrentQueue<RequestEnvelope> rpcQueue = new ConcurrentQueue<RequestEnvelope>();
        private static ConcurrentDictionary<RequestEnvelope, ResponseEnvelope> responses = new ConcurrentDictionary<RequestEnvelope, ResponseEnvelope>();
        private static Semaphore mutex = new Semaphore(1, 1);

        public static ResponseEnvelope PerformThrottledRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client, Client apiClient, RequestEnvelope requestEnvelope) where TRequest : IMessage<TRequest>
        {
            rpcQueue.Enqueue(requestEnvelope);
            var count = rpcQueue.Count;
            ResponseEnvelope ret;

            try
            {
                mutex.WaitOne();
                RequestEnvelope r;
                while (rpcQueue.TryDequeue(out r))
                {
                    var diff = Math.Max(0, DateTime.Now.Millisecond - lastRpc);
                    if (diff < minDiff)
                    {
                        var delay = (minDiff - diff) + (int)(new Random().NextDouble() * 0); // Add some randomness
                        Task.Delay((int)(delay)).Wait();
                    }
                    lastRpc = DateTime.Now.Millisecond;
                    ResponseEnvelope response = PerformRemoteProcedureCall<TRequest>(client, apiClient, r);
                    responses.GetOrAdd(r, response);
                }
                responses.TryRemove(requestEnvelope, out ret);
            }
            finally
            {
                mutex.Release();
            }
            return ret;
        }
    }
}