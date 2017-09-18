using Newtonsoft.Json;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public class PokefarmerHasher : IHasher
    {
        public static string PokeHashURL = "https://pokehash.buddyauth.com/";
        public static string PokeHashURL2 = "http://pokehash.buddyauth.com/";
        
        public class Stat
        {
            public DateTime Timestamp { get; set; }
            public long ResponseTime { get; set; }
        }
        private string apiKey;
        private List<KeyValuePair<string, int>> apiKeys = null;
        public bool VerboseLog { get; set; }
        private List<Stat> statistics = new List<Stat>();
        private string apiEndPoint;

        private HashInfo fullStats = new HashInfo();

        public PokefarmerHasher(ISettings settings, string apiKey, bool log, string apiEndPoint)
        {
            VerboseLog = log;
            this.apiKey = apiKey;
            this.apiEndPoint = apiEndPoint;
            if (settings.UseCustomAPI)
            {
                PokeHashURL = settings.UrlHashServices;
                PokeHashURL2 = settings.UrlHashServices;
                if (!string.IsNullOrEmpty(settings.EndPoint))
                    this.apiEndPoint = settings.EndPoint;
            }
        }

        public async Task<HashResponseContent> RequestHashesAsync(HashRequestContent request)
        {
            int retry = 3;
            do
            {
                try
                {
                    return await InternalRequestHashesAsync(request).ConfigureAwait(false);
                }
                catch (HasherException hashEx)
                {
                    throw hashEx;
                }
                catch (TimeoutException)
                {
                    throw new HasherException("Pokefamer Hasher server might down - timeout out");
                }
                catch (Exception ex)
                {
                    APIConfiguration.Logger.LogDebug(ex.Message);
                }
                finally
                {
                    retry--;
                }
                await Task.Delay(1000).ConfigureAwait(false);
            } while (retry > 0);

            throw new HasherException("Pokefamer Hash API server might be down");

        }
        private DateTime lastPrintVerbose = DateTime.Now;

        private async Task<HashResponseContent> InternalRequestHashesAsync(HashRequestContent request)
        {
            string key = GetAPIKey();

            var maskedKey = key.Substring(0, 4) + "".PadLeft(key.Length - 8, 'X') + key.Substring(key.Length - 4, 4);
            // NOTE: This is really bad. Don't create new HttpClient's all the time.
            // Use a single client per-thread if you need one.
            using (var client = new System.Net.Http.HttpClient())
            {
                // The URL to the hashing server.
                // Do not include "api/v1/hash" unless you know why you're doing it, and want to modify this code.
                client.BaseAddress = new Uri(PokeHashURL);

                // By default, all requests (and this example) are in JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Set the X-AuthToken to the key you purchased from Bossland GmbH
                client.DefaultRequestHeaders.Add("X-AuthToken", key);

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.ASCII, "application/json");
                // An odd bug with HttpClient. You need to set the content type again.
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                Stopwatch watcher = new Stopwatch();
                HttpResponseMessage response = null;
                watcher.Start();
                Stat stat = new Stat() { Timestamp = DateTime.Now };
                try
                {
                    response = await client.PostAsync(apiEndPoint, content).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    try
                    {
                        client.BaseAddress = new Uri(PokeHashURL2);
                        response = await client.PostAsync(apiEndPoint, content).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                finally
                {
                    watcher.Stop();

                    // Need to check for null response.
                    if (response == null)
                        throw new HasherException($"Hash API server ({client.BaseAddress}{apiEndPoint}) might down!");

                    fullStats.APICalles++;
                    fullStats.TotalTimes += watcher.ElapsedMilliseconds;

                    stat.ResponseTime = watcher.ElapsedMilliseconds;
                    statistics.Add(stat);
                    statistics.RemoveAll(x => x.Timestamp < DateTime.Now.AddMinutes(-1));
                    if (statistics.Count > 0)
                    {
                        lastPrintVerbose = DateTime.Now;
                        double agv = statistics.Sum(x => x.ResponseTime) / statistics.Count;
                        fullStats.Last60MinAPICalles = statistics.Count;
                        fullStats.Last60MinAPIAvgTime = agv;
                        fullStats.Fastest = fullStats.Fastest == 0 ? watcher.ElapsedMilliseconds : Math.Min(fullStats.Fastest, watcher.ElapsedMilliseconds);
                        fullStats.Slowest = Math.Max(fullStats.Slowest, watcher.ElapsedMilliseconds);
                        fullStats.MaskedAPIKey = maskedKey;
                    }
#pragma warning disable IDE0018 // Inline variable declaration - Build.Bat Error Happens if We Do
                    int maxRequestCount = 0;
                    IEnumerable<string> headers;
                    IEnumerable<string> requestRemains;
                    if (response.Headers.TryGetValues("X-MaxRequestCount", out headers))
                    {
                        // Get the rate-limit period ends at timestamp in seconds.
                        maxRequestCount = Convert.ToInt32(headers.First());
                    }

                    if (response.Headers.TryGetValues("X-AuthTokenExpiration", out headers))
                    {
                        uint secondsToExpiration = 0;
                        secondsToExpiration = Convert.ToUInt32(headers.First());
                        fullStats.Expired = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(secondsToExpiration).ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
                    }

                    if (response.Headers.TryGetValues("X-RateRequestsRemaining", out requestRemains))
                    {
                        // Get the rate-limit period ends at timestamp in seconds.
                        int requestRemain = Convert.ToInt32(requestRemains.First());
                        fullStats.HealthyRate = (double)(requestRemain) / maxRequestCount;
                        UpdateRate(key, requestRemain);
                    }
                    APIConfiguration.Logger.HashStatusUpdate(fullStats);
#pragma warning restore IDE0018 // Inline variable declaration - Build.Bat Error Happens if We Do
                }

                // TODO: Fix this up with proper retry-after when we get rate limited.
                switch (response.StatusCode)
                {
                    // All good. Return the hashes back to the caller. :D
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<HashResponseContent>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                    // Returned when something in your request is "invalid". Also when X-AuthToken is not set.
                    // See the error message for why it is bad.
                    case HttpStatusCode.BadRequest:
                        string responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (responseText.Contains("Unauthorized"))
                        {
                            APIConfiguration.Logger.LogDebug($"Unauthorized : {key}  ");
                            if (apiKeys.Count()> 1){
                                apiKeys.RemoveAll(x => x.Key == key);
                                return await RequestHashesAsync(request).ConfigureAwait(false);
                            }
                            throw new HasherException($"Your API Key: {maskedKey} is incorrect or expired, please check auth.json (Pokefamer Message : {responseText})");
                        }
                        Console.WriteLine($"Bad request sent to the hashing server! {responseText}");

                        break;

                    // This error code is returned when your "key" is not in a valid state. (Expired, invalid, etc)
                    case HttpStatusCode.Unauthorized:
                        APIConfiguration.Logger.LogDebug($"Unauthorized : {key}  ");
                        if (apiKeys.Count()> 1){
                            apiKeys.RemoveAll(x => x.Key == key);
                            return await RequestHashesAsync(request).ConfigureAwait(false);
                        }

                        throw new HasherException($"You are not authorized to use this service.  Please check that your API key {maskedKey} is correct.");

                    // This error code is returned when you have exhausted your current "hashes per second" value
                    // You should queue up your requests, and retry in a second.
                    case (HttpStatusCode)429:
                        APIConfiguration.Logger.LogInfo($"Your request has been limited. {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}");
                        if (apiKeys.Count()> 1){
                            return await RequestHashesAsync(request).ConfigureAwait(false);
                        }

                        long ratePeriodEndsAtTimestamp;
                        IEnumerable<string> ratePeriodEndHeaderValues;
                        if (response.Headers.TryGetValues("X-RatePeriodEnd", out ratePeriodEndHeaderValues))
                        {
                            // Get the rate-limit period ends at timestamp in seconds.
                            ratePeriodEndsAtTimestamp = Convert.ToInt64(ratePeriodEndHeaderValues.First());
                        }
                        else
                        {
                            // If for some reason we couldn't get the timestamp, just default to 2 second wait.
                            ratePeriodEndsAtTimestamp = Utils.GetTime(false) + 2;
                        }

                        long timeToWaitInSeconds = ratePeriodEndsAtTimestamp - Utils.GetTime(false);

                        if (timeToWaitInSeconds > 0)
                            await Task.Delay((int)(timeToWaitInSeconds * 1000)).ConfigureAwait(false);  // Wait until next rate-limit period begins.

                        return await RequestHashesAsync(request).ConfigureAwait(false);
                    default:
                        throw new HasherException($"Hash API server ({client.BaseAddress}{apiEndPoint}) might down!");
                }

            }

            return null;
        }

        private void UpdateRate(string key, int remain)
        {
            apiKeys.RemoveAll(x => x.Key == key);
            apiKeys.Add(new KeyValuePair<string, int>(key, remain));
        }
        private string GetAPIKey()
        {
            if (apiKeys == null)
            {
                apiKeys = new List<KeyValuePair<string, int>>();

                var allkeys = apiKey.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in allkeys)
                {
                    apiKeys.Add(new KeyValuePair<string, int>(item, int.MaxValue));
                }
            }
            apiKeys.OrderByDescending(x => x.Value);
            return apiKeys.First().Key;
        }
    }
}
