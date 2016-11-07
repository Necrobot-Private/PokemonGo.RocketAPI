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

    public interface IApiFailureStrategy
    {
        Task<ApiOperation> HandleApiFailure(RequestEnvelope request, ResponseEnvelope response);
        void HandleApiSuccess(RequestEnvelope request, ResponseEnvelope response);

        void HandleCaptcha(string challengeUrl, ICaptchaResponseHandler captchaResponseHandler);
    }

    public interface ICaptchaResponseHandler
    {
        void SetCaptchaToken(string captchaToken);
    }

    public static class HttpClientExtensions
    {
        public static async Task<IMessage[]> PostProtoPayload<TRequest>(this System.Net.Http.HttpClient client,
            string url, RequestEnvelope requestEnvelope,
            IApiFailureStrategy strategy,
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

            ResponseEnvelope response;
            while ((response = await PerformThrottledRemoteProcedureCall<TRequest>(client, url, requestEnvelope)).Returns.Count !=
                   responseTypes.Length)
            {
                var operation = await strategy.HandleApiFailure(requestEnvelope, response);
                if (operation == ApiOperation.Abort)
                {
                    throw new InvalidResponseException(
                        $"Expected {responseTypes.Length} responses, but got {response.Returns.Count} responses");
                }
            }

            strategy.HandleApiSuccess(requestEnvelope, response);

            for (var i = 0; i < responseTypes.Length; i++)
            {
                var payload = response.Returns[i];
                result[i].MergeFrom(payload);
            }
            return result;
        }

        public static async Task<TResponsePayload> PostProtoPayload<TRequest, TResponsePayload>(
            this System.Net.Http.HttpClient client,
            string url, RequestEnvelope requestEnvelope, IApiFailureStrategy strategy)
            where TRequest : IMessage<TRequest>
            where TResponsePayload : IMessage<TResponsePayload>, new()
        {
            Debug.WriteLine($"Requesting {typeof(TResponsePayload).Name}");
            var response = await PerformThrottledRemoteProcedureCall<TRequest>(client, url, requestEnvelope);

            while (response.Returns.Count == 0)
            {
                var operation = await strategy.HandleApiFailure(requestEnvelope, response);
                if (operation == ApiOperation.Abort)
                {
                    break;
                }

                response = await PerformThrottledRemoteProcedureCall<TRequest>(client, url, requestEnvelope);
            }

            if (response.Returns.Count == 0)
                throw new InvalidResponseException();

            strategy.HandleApiSuccess(requestEnvelope, response);

            //Decode payload
            //todo: multi-payload support
            var payload = response.Returns[0];
            var parsedPayload = new TResponsePayload();
            parsedPayload.MergeFrom(payload);

            return parsedPayload;
        }

        private static async Task<ResponseEnvelope> PerformRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client,
            string url,
            RequestEnvelope requestEnvelope) where TRequest : IMessage<TRequest>
        {
            //Encode payload and put in envelop, then send
            var data = requestEnvelope.ToByteString();
            var result = await client.PostAsync(url, new ByteArrayContent(data.ToByteArray()));

            //Decode message
            var responseData = await result.Content.ReadAsByteArrayAsync();
            var codedStream = new CodedInputStream(responseData);
            var decodedResponse = new ResponseEnvelope();
            decodedResponse.MergeFrom(codedStream);

            return decodedResponse;
        }

        // RPC Calls need to be throttled 
        private static long lastRpc = 0;    // Starting at 0 to allow first RPC call to be done immediately
        private const int minDiff = 1000;   // Derived by trial-and-error. Up to 900 can cause server to complain.
        private static ConcurrentQueue<RequestEnvelope> rpcQueue = new ConcurrentQueue<RequestEnvelope>();
        private static ConcurrentDictionary<RequestEnvelope, ResponseEnvelope> responses = new ConcurrentDictionary<RequestEnvelope, ResponseEnvelope>();
        private static Semaphore mutex = new Semaphore(1, 1);

        public static async Task<ResponseEnvelope> PerformThrottledRemoteProcedureCall<TRequest>(this System.Net.Http.HttpClient client, string url, RequestEnvelope requestEnvelope) where TRequest : IMessage<TRequest>
        {
            rpcQueue.Enqueue(requestEnvelope);
            var count = rpcQueue.Count;
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
                ResponseEnvelope response = await PerformRemoteProcedureCall<TRequest>(client, url, requestEnvelope);
                responses.GetOrAdd(r, response);
            }
            ResponseEnvelope ret;
            responses.TryRemove(requestEnvelope, out ret);
            mutex.Release();
            return ret;
        }
    }
}