using Newtonsoft.Json;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Helpers;
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
        public long Client_Unknown25 => -8832040574896607694;
        private string apiKey;
        public bool VerboseLog { get; set; }
        private List<Stat> statistics = new List<Stat>();
        public PokefamerHasher(string apiKey, bool log)
        {
            this.VerboseLog = log;
            this.apiKey = apiKey;
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
                    Debug.Write(ex.Message);
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
            // This value will determine which version of hashing you receive.
            // Currently supported versions:
            // v119 -> Pogo iOS 1.19
            // v121 -> Pogo iOS 1.21
            // v121_2 => IOS 1.22
            const string endpoint = "api/v123_1/hash";

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
                client.DefaultRequestHeaders.Add("X-AuthToken", this.apiKey);

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.ASCII, "application/json");
                // An odd bug with HttpClient. You need to set the content type again.
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                Stopwatch watcher = new Stopwatch();
                HttpResponseMessage response = null;
                watcher.Start();
                Stat stat = new Stat() { Timestamp = DateTime.Now };
                try
                {
                    response = await client.PostAsync(endpoint, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    watcher.Stop();
                    stat.ResponseTime = watcher.ElapsedMilliseconds;
                    statistics.Add(stat);
                    statistics.RemoveAll(x => x.Timestamp < DateTime.Now.AddMinutes(-1));
                    if (VerboseLog && lastPrintVerbose.AddSeconds(15) < DateTime.Now)
                    {

                        if (statistics.Count > 0)
                        {
                            lastPrintVerbose = DateTime.Now;
                            double agv = statistics.Sum(x => x.ResponseTime) / statistics.Count;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (HASH SERVER)  in last 1 minute  {statistics.Count} request/min , AVG: {agv:0.00} ms/request , Fastest : {statistics.Min(t=>t.ResponseTime)}, Slowest: {statistics.Max(t => t.ResponseTime)}");
                        }
                    }
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
                        if (responseText.Contains("Unauthorized")) throw new HasherException($"Your API key is incorrect or expired, please check auth.json (Pokefamer message : {responseText})");
                        Console.WriteLine($"Bad request sent to the hashing server! {responseText}");
                        break;

                    // This error code is returned when your "key" is not in a valid state. (Expired, invalid, etc)
                    case HttpStatusCode.Unauthorized:
                        throw new HasherException("You are not authorized to use this service.  Please check that your API key is correct.");

                    // This error code is returned when you have exhausted your current "hashes per second" value
                    // You should queue up your requests, and retry in a second.
                    case (HttpStatusCode)429:
                        Console.WriteLine($"Your request has been limited. {await response.Content.ReadAsStringAsync()}");
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
                        throw new HasherException($"Hash API server ({client.BaseAddress}{endpoint}) might down!");
                }
            }

            return null;
        }


    }
}
