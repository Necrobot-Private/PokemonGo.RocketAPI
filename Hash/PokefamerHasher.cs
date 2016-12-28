using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public long Client_Unknown25 => -8832040574896607694;
        private string apiKey;
        public PokefamerHasher(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task<HashResponseContent> RequestHashesAsync(HashRequestContent request)
        {
            // This value will determine which version of hashing you receive.
            // Currently supported versions:
            // v119 -> Pogo iOS 1.19
            // v121 -> Pogo iOS 1.21
            const string endpoint = "api/v122/hash";

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

                var response = await client.PostAsync(endpoint, content);

                // TODO: Fix this up with proper retry-after when we get rate limited.
                switch (response.StatusCode)
                {
                    // All good. Return the hashes back to the caller. :D
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<HashResponseContent>(await response.Content.ReadAsStringAsync());

                    // Returned when something in your request is "invalid". Also when X-AuthToken is not set.
                    // See the error message for why it is bad.
                    case HttpStatusCode.BadRequest:
                        Console.WriteLine($"Bad request sent to the hashing server! {await response.Content.ReadAsStringAsync()}");
                        break;

                    // This error code is returned when your "key" is not in a valid state. (Expired, invalid, etc)
                    case HttpStatusCode.Unauthorized:
                        Console.WriteLine("You are not authorized to use this service. please check you api key correct");
                        Console.ReadKey();
                        Environment.Exit(0); //TODO need logic layer to handle this error , maybe validation when first start
                        break;

                    // This error code is returned when you have exhausted your current "hashes per second" value
                    // You should queue up your requests, and retry in a second.
                    case (HttpStatusCode)429:
                        Console.WriteLine($"Your request has been limited. {await response.Content.ReadAsStringAsync()}");
                        await Task.Delay(2 * 1000);  //stop for 2 sec
                        return await RequestHashesAsync(request);
                        break;
                }
            }

            return null;
        }


    }
}
