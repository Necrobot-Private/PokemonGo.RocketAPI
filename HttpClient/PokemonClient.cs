﻿#region using directives

using System.Net;
using System.Net.Http;
using PokemonGo.RocketAPI.Helpers;

#endregion

namespace PokemonGo.RocketAPI.HttpClient
{
    public class PokemonHttpClient : System.Net.Http.HttpClient
    {
        private static readonly HttpClientHandler Handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = false,
            UseProxy = Client.Proxy != null,
            Proxy = Client.Proxy
        };

        public PokemonHttpClient() : base(new RetryHandler(Handler))
        {
            DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Niantic App");
            DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            //DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/binary");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "identity, gzip");
            DefaultRequestHeaders.ExpectContinue = false;
        }
    }
}