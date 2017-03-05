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
    public class PokefamerHasher : IHasher
    {
        public class Stat
        {
            public DateTime Timestamp { get; set; }
            public long ResponseTime { get; set; }
        }
        private string apiKey;
        public bool VerboseLog { get; set; }
        private List<Stat> statistics = new List<Stat>();
        private string apiEndPoint;

        private HashInfo fullStats = new HashInfo();

        public PokefamerHasher(string apiKey, bool log, string apiEndPoint)
        {
            this.VerboseLog = log;
            this.apiKey = apiKey;
            this.apiEndPoint = apiEndPoint;
        }
        public async Task<HashResponseContent> RequestHashesAsync(HashRequestContent request)
        {
            int retry = 3;
            do
            {
                try
                {
                    return await InternalRequestHashesAsync(request);
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
                await Task.Delay(1000);
            } while (retry > 0);

            throw new HasherException("Pokefamer Hash API server might down");

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
                client.BaseAddress = new Uri("http://pokehash.buddyauth.com/");

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
                    response = await client.PostAsync(apiEndPoint, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    watcher.Stop();
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

                    IEnumerable<string> headers;
                    int maxRequestCount = 0;
                    if (response.Headers.TryGetValues("X-MaxRequestCount", out headers))
                    {
                        // Get the rate-limit period ends at timestamp in seconds.
                        maxRequestCount = Convert.ToInt32(headers.First());
                    }

                    IEnumerable<string> requestRemains;
                    if (response.Headers.TryGetValues("X-RateRequestsRemaining", out requestRemains))
                    {
                        // Get the rate-limit period ends at timestamp in seconds.
                        int requestRemain = Convert.ToInt32(requestRemains.First());
                        fullStats.HealthyRate = (double)(requestRemain) / maxRequestCount;
                        UpdateRate(key, requestRemain);
                    }
                    APIConfiguration.Logger.HashStatusUpdate(fullStats);

                }

                // TODO: Fix this up with proper retry-after when we get rate limited.
                switch (response.StatusCode)
                {
                    // All good. Return the hashes back to the caller. :D
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<HashResponseContent>(await response.Content.ReadAsStringAsync());

                    // Returned when something in your request is "invalid". Also when X-AuthToken is not set.
                    // See the error message for why it is bad.
                    case HttpStatusCode.BadRequest:
                        string responseText = await response.Content.ReadAsStringAsync();
                        if (responseText.Contains("Unauthorized"))
                        {
                            APIConfiguration.Logger.LogDebug($"Unauthorized : {key}  ");
                            throw new HasherException($"Your API key {maskedKey} is incorrect or expired, please check auth.json (Pokefamer message : {responseText})");
                        }
                        Console.WriteLine($"Bad request sent to the hashing server! {responseText}");

                        break;

                    // This error code is returned when your "key" is not in a valid state. (Expired, invalid, etc)
                    case HttpStatusCode.Unauthorized:
                        APIConfiguration.Logger.LogDebug($"Unauthorized : {key}  ");

                        throw new HasherException($"You are not authorized to use this service.  Please check that your API key {maskedKey} is correct.");

                    // This error code is returned when you have exhausted your current "hashes per second" value
                    // You should queue up your requests, and retry in a second.
                    case (HttpStatusCode)429:
                        APIConfiguration.Logger.LogInfo($"Your request has been limited. {await response.Content.ReadAsStringAsync()}");
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
                            await Task.Delay((int)(timeToWaitInSeconds * 1000));  // Wait until next rate-limit period begins.

                        return await RequestHashesAsync(request);
                    default:
                        throw new HasherException($"Hash API server ({client.BaseAddress}{apiEndPoint}) might down!");
                }

            }

            return null;
        }

        List<KeyValuePair<string, int>> apiKeys = null;
        private void UpdateRate(string key, int remain)
        {
            this.apiKeys.RemoveAll(x => x.Key == key);
            this.apiKeys.Add(new KeyValuePair<string, int>(key, remain));
        }
        private string GetAPIKey()
        {
            if (apiKeys == null)
            {
                apiKeys = new List<KeyValuePair<string, int>>();

                var allkeys = this.apiKey.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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
