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
        public static async Task<IMessage[]> PostProtoPayload<TRequest>(this System.Net.Http.HttpClient client,
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

            ResponseEnvelope response = await PerformThrottledRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);

            if (response.Returns.Count != requestEnvelope.Requests.Count)
                throw new InvalidResponseException();

            for (var i = 0; i < responseTypes.Length; i++)
            {
                var payload = response.Returns[i];
                result[i].MergeFrom(payload);
            }
            return result;
        }

        public static async Task<TResponsePayload> PostProtoPayload<TRequest, TResponsePayload>(
            this System.Net.Http.HttpClient client,
            Client apiClient, RequestEnvelope requestEnvelope)
            where TRequest : IMessage<TRequest>
            where TResponsePayload : IMessage<TResponsePayload>, new()
        {
            ResponseEnvelope response = await PerformThrottledRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);

            if (response.Returns.Count != requestEnvelope.Requests.Count)
                throw new InvalidResponseException();

            //Decode payload
            //todo: multi-payload support
            var payload = response.Returns[0];
            var parsedPayload = new TResponsePayload();
            parsedPayload.MergeFrom(payload);

            return parsedPayload;
        }

        private static async Task<ResponseEnvelope> PerformRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client,
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
            var result = await client.PostAsync(apiClient.ApiUrl, new ByteArrayContent(data.ToByteArray()));

            //Decode message
            var responseData = await result.Content.ReadAsByteArrayAsync();
            var codedStream = new CodedInputStream(responseData);
            ResponseEnvelope serverResponse = new ResponseEnvelope();
            serverResponse.MergeFrom(codedStream);
            
            if (!string.IsNullOrEmpty(serverResponse.ApiUrl))
                apiClient.ApiUrl = "https://" + serverResponse.ApiUrl + "/rpc";

            if (serverResponse.AuthTicket != null)
                apiClient.AccessToken.AuthTicket = serverResponse.AuthTicket;

            switch (serverResponse.StatusCode)
            {
                case ResponseEnvelope.Types.StatusCode.InvalidAuthToken:
                    apiClient.AccessToken.Expire();
                    await Rpc.Login.Reauthenticate(apiClient);
                    Rpc.Login.SaveAccessToken(apiClient.AccessToken);
                    throw new AccessTokenExpiredException();
                case ResponseEnvelope.Types.StatusCode.Redirect:
                    // 53 means that the api_endpoint was not correctly set, should be at this point, though, so redo the request
                    return await PerformRemoteProcedureCall<TRequest>(client, apiClient, requestEnvelope);
                case ResponseEnvelope.Types.StatusCode.BadRequest:
                    // Your account may be banned! please try from the official client.
                    throw new LoginFailedException("Your account may be banned! please try from the official client.");
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
                    break;
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

        public static async Task<ResponseEnvelope> PerformThrottledRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client, Client apiClient, RequestEnvelope requestEnvelope) where TRequest : IMessage<TRequest>
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
                        await Task.Delay((int)(delay));
                    }
                    lastRpc = DateTime.Now.Millisecond;
                    ResponseEnvelope response = await PerformRemoteProcedureCall<TRequest>(client, apiClient, r);
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