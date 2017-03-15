#region using directives

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    internal class RetryHandler : DelegatingHandler
    {
        private const int MaxRetries = 25;

        public RetryHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            for (var i = 0; i <= MaxRetries; i++)
            {
                try
                {
                    var response =  base.SendAsync(request, cancellationToken).Result;
                    if (response.StatusCode == HttpStatusCode.BadGateway ||
                        response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new Exception(); //todo: proper implementation

                    return response;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[#{i} of {MaxRetries}] retry request {request.RequestUri} - Error: {ex}");
                    if (i < MaxRetries)
                    {
                        Task.Delay(1000, cancellationToken).Wait();
                        continue;
                    }
                    throw;
                }
            }
            return null;
        }
    }
}