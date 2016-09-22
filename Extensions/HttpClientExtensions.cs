#region using directives

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Protobuf;
using PokemonGo.RocketAPI.Exceptions;
using POGOProtos.Networking.Envelopes;
using PokemonGo.RocketAPI.Helpers;

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
        Task<ApiOperation> HandleApiFailure(RequestEnvelope request, ResponseEnvelope response, Exception exception);
        void HandleApiSuccess(RequestEnvelope request, ResponseEnvelope response);
    }

    public static class HttpClientExtensions
    {
        public static DateTime LastApiRequestTime = DateTime.Now;
        public static double MaxThrottleMs = 3000;
        public static double BaseThrottleMs = 100;
        const int MaxThrottleRetries = 10;
        
        public static async Task<IMessage[]> PostProtoPayload<TRequest>(this System.Net.Http.HttpClient client,
            string url, RequestEnvelope requestEnvelope,
            IApiFailureStrategy strategy, Client apiClient,
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

            ResponseEnvelope response = null;
            do
            {
                try
                {
                    response = await PostProto<TRequest>(client, apiClient, url, requestEnvelope);
                }
                catch (Exception e)
                {
                    var operation = await strategy.HandleApiFailure(requestEnvelope, response, e);
                    if (operation == ApiOperation.Abort)
                    {
                        throw e;
                    }
                }
            } while (response == null || response.Returns.Count != responseTypes.Length);

            if (response == null || response.Returns.Count != responseTypes.Length)
            {
                throw new InvalidResponseException(
                       $"Expected {responseTypes.Length} responses, but got {response.Returns.Count} responses");
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
            Client apiClient,
            string url, RequestEnvelope requestEnvelope, IApiFailureStrategy strategy)
            where TRequest : IMessage<TRequest>
            where TResponsePayload : IMessage<TResponsePayload>, new()
        {
            Debug.WriteLine($"Requesting {typeof(TResponsePayload).Name}");

            ResponseEnvelope response = null;
            do
            {
                try
                {
                    response = await PostProto<TRequest>(client, apiClient, url, requestEnvelope);
                }
                catch (Exception e)
                {
                    var operation = await strategy.HandleApiFailure(requestEnvelope, response, e);
                    if (operation == ApiOperation.Abort)
                    {
                        throw e;
                    }
                }
            } while (response == null || response.Returns.Count == 0);

            if (response == null || response.Returns.Count == 0)
            {
                throw new InvalidResponseException();
            }

            strategy.HandleApiSuccess(requestEnvelope, response);

            //Decode payload
            //todo: multi-payload support
            var payload = response.Returns[0];
            var parsedPayload = new TResponsePayload();
            parsedPayload.MergeFrom(payload);

            return parsedPayload;
        }

        public static async Task<ResponseEnvelope> PostProto<TRequest>(this System.Net.Http.HttpClient client,
            Client apiClient, string url,
            RequestEnvelope requestEnvelope, int retryCount = 0) where TRequest : IMessage<TRequest>
        {
            //Encode payload and put in envelope, then send
            var data = requestEnvelope.ToByteString();

            // 3 requests per second is the max limit, so on average 1 request every 333 ms.
            double timespanSinceLastRequest = (DateTime.Now - LastApiRequestTime).TotalMilliseconds;
            if (timespanSinceLastRequest <= BaseThrottleMs)
            {
                // Throttle
                double throttleMs = GetThrottleTime(retryCount); //BaseThrottleMs - timespanSinceLastRequest;
                await Task.Delay((int)throttleMs);
            }

            var result = await client.PostAsync(url, new ByteArrayContent(data.ToByteArray()));
            LastApiRequestTime = DateTime.Now;

            //Decode message
            var responseData = await result.Content.ReadAsByteArrayAsync();
            var codedStream = new CodedInputStream(responseData);
            ResponseEnvelope decodedResponse = new ResponseEnvelope();
            decodedResponse.MergeFrom(codedStream);

            // Update client api url
            if (!string.IsNullOrEmpty(decodedResponse.ApiUrl))
                apiClient.ApiUrl = "https://" + decodedResponse.ApiUrl + "/rpc";

            if (decodedResponse.AuthTicket != null)
            {
                if ((ulong)Utils.GetTime(true) < decodedResponse.AuthTicket.ExpireTimestampMs)
                {
                    // Valid auth ticket so update auth ticket.
                    apiClient.AuthTicket = decodedResponse.AuthTicket;
                }
                else
                {
                    // Invalid auth ticket - expired.
                    apiClient.AuthTicket = null;
                }
            }
            
            switch (decodedResponse.StatusCode)
            {
                case ResponseEnvelope.Types.StatusCode.InvalidAuthToken:
                    apiClient.AuthToken = null;
                    throw new AccessTokenExpiredException();
                case ResponseEnvelope.Types.StatusCode.Redirect:
                    // New rpc api endpoint is available and you should redirect to there.
                    return await PostProto<TRequest>(client, apiClient, apiClient.ApiUrl, requestEnvelope, retryCount + 1);
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
                    // Request throttled by server... wait and retry up to retriesLeft
                    if (retryCount < MaxThrottleRetries)
                    {
                        return await PostProto<TRequest>(client, apiClient, url, requestEnvelope, retryCount + 1);
                    }
                    else
                    {
                        throw new InvalidPlatformRequestException();
                    }
                case ResponseEnvelope.Types.StatusCode.SessionInvalidated:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (decodedResponse.Returns.Count == 0)
                throw new InvalidResponseException();

            return decodedResponse;
        }

        public static async Task DelayAsync(int delay, int defdelay)
        {
            if (delay > defdelay)
            {
                var randomFactor = 0.3f;
                var randomMin = (int)(delay * (1 - randomFactor));
                var randomMax = (int)(delay * (1 + randomFactor));
                var randomizedDelay = new Random().Next(randomMin, randomMax);

                await Task.Delay(randomizedDelay);
            }
            else if (defdelay > 0)
            {
                await Task.Delay(defdelay);
            }
        }

        /*
         * Returns the next wait interval, in milliseconds, using an exponential
         * backoff algorithm with jitter.
         * See https://www.awsarchitectureblog.com/2015/03/backoff.html
         */
        public static ulong GetThrottleTime(int retryCount)
        {
            double temp = Math.Min(MaxThrottleMs, BaseThrottleMs * ((ulong)Math.Pow(2, retryCount)));
            return (ulong)Math.Round((temp / 2) + RequestBuilder.GenRandom(0, temp / 2));
        }
    }
}