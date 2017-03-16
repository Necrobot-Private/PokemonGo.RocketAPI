#region using directives

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public static class HttpClientHelper
    {
        public static TResponse PostFormEncodedAsync<TResponse>(string url,
            params KeyValuePair<string, string>[] keyValuePairs)

        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                AllowAutoRedirect = false,
                UseProxy = Client.Proxy != null,
                Proxy = Client.Proxy
            };

            using (var tempHttpClient = new System.Net.Http.HttpClient(handler))
            {
                var response = tempHttpClient.PostAsync(url, new FormUrlEncodedContent(keyValuePairs)).Result;
                return response.Content.ReadAsAsync<TResponse>().Result;
            }
        }
    }
}