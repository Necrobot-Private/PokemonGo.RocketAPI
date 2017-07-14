﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonGo.RocketAPI.Authentication.Data;

namespace PokemonGo.RocketAPI.LoginProviders
{
    /// <summary>
    /// The <see cref="ILoginProvider"/> for Pokemon Trainer Club.
    /// Use this if you want to authenticate to PokemonGo using a Pokemon Trainer Club account.
    /// </summary>
    public class PtcLoginProvider : ILoginProvider
    {
        private readonly string _username;
        private readonly string _password;

        public PtcLoginProvider(string username, string password)
        {
            _username = username;
            _password = password;
        }

        /// <summary>
        /// The unique identifier of the <see cref="PtcLoginProvider"/>.
        /// </summary>
        public string ProviderId => "ptc";

        /// <summary>
        /// The unique identifier of the user trying to authenticate using the <see cref="PtcLoginProvider"/>.
        /// </summary>
        public string UserId => _username;

        /// <summary>
        /// Retrieves an <see cref="AccessToken"/> by logging into the Pokemon Trainer Club website.
        /// </summary>
        /// <returns>Returns an <see cref="AccessToken"/>.</returns>
        public async Task<AccessToken> GetAccessToken()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.AllowAutoRedirect = false;
                using (var httpClient = new System.Net.Http.HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(Constants.Accept);
                    httpClient.DefaultRequestHeaders.Host = Constants.LoginHostValue;
                    httpClient.DefaultRequestHeaders.Connection.Add(Constants.Connection);
                    httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(Constants.LoginUserAgent);
                    httpClient.DefaultRequestHeaders.AcceptLanguage.Add(Constants.AcceptLanguage);
                    httpClient.DefaultRequestHeaders.AcceptEncoding.Add(Constants.AcceptEncoding);
                    httpClient.DefaultRequestHeaders.Add(Constants.LoginManufactor, Constants.LoginManufactorVersion);
                    var loginData = await GetLoginData(httpClient).ConfigureAwait(false);
                    var ticket = await PostLogin(httpClient, _username, _password, loginData).ConfigureAwait(false);
                    var accessToken = await PostLoginOauth(httpClient, ticket).ConfigureAwait(false);
                    accessToken.Username = _username;
                    //Logger.Debug("Authenticated through PTC.");
                    return accessToken;
                }
            }
        }

        /// <summary>
        /// Responsible for retrieving login parameters for <see cref="PostLogin" />.
        /// </summary>
        /// <param name="httpClient">An initialized <see cref="HttpClient" />.</param>
        /// <returns><see cref="LoginData" /> for <see cref="PostLogin" />.</returns>
        private async Task<LoginData> GetLoginData(System.Net.Http.HttpClient httpClient)
        {
            var loginDataResponse = await httpClient.GetAsync(Constants.LoginUrl).ConfigureAwait(false);
            if (!loginDataResponse.IsSuccessStatusCode)
                throw new Exception($"Unexpected response from Pokemon SSO OAuth Login Url: Status code {loginDataResponse.StatusCode}");
            
            var jsonData = await loginDataResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var loginData = JsonConvert.DeserializeObject<LoginData>(jsonData);
            return loginData;
        }

        /// <summary>
        /// Responsible for submitting the login request.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="username">The user's PTC username.</param>
        /// <param name="password">The user's PTC password.</param>
        /// <param name="loginData"><see cref="LoginData" /> taken from PTC website using <see cref="GetLoginData" />.</param>
        /// <returns></returns>
        private async Task<string> PostLogin(System.Net.Http.HttpClient httpClient, string username, string password, LoginData loginData)
        {
            var loginResponse =
                await httpClient.PostAsync(Constants.LoginUrl, new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"lt", loginData.Lt},
                    {"execution", loginData.Execution},
                    {"_eventId", "submit"},
                    {"username", username},
                    {"password", password}
                })).ConfigureAwait(false);

            var loginResponseDataRaw = await loginResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!loginResponseDataRaw.Contains("{"))
            {
                var locationQuery = loginResponse.Headers.Location.Query;
                var ticketStartPosition = locationQuery.IndexOf("=", StringComparison.Ordinal) + 1;
                return locationQuery.Substring(ticketStartPosition, locationQuery.Length - ticketStartPosition);
            }

            var loginResponseData = JObject.Parse(loginResponseDataRaw);
            var loginResponseErrors = (JArray)loginResponseData["errors"];

            throw new Exception($"Pokemon Trainer Club gave error(s): '{string.Join(",", loginResponseErrors)}'");
        }

        /// <summary>
        /// Responsible for finishing the oauth login request.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        private async Task<AccessToken> PostLoginOauth(System.Net.Http.HttpClient httpClient, string ticket)
        {
            var loginResponse =
                await httpClient.PostAsync(Constants.LoginOauthUrl, new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", Constants.Client_Id},
                    {"redirect_uri", Constants.Redirect_Uri},
                    {"client_secret", Constants.Client_Secret},
                    {"grant_type", Constants.Grant_Type},
                    {"code", ticket}
                })).ConfigureAwait(false);

            var loginResponseDataRaw = await loginResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var oAuthData = Regex.Match(loginResponseDataRaw, "access_token=(?<accessToken>.*?)&expires=(?<expires>\\d+)");
            if (!oAuthData.Success)
                throw new Exception($"Couldn't verify the OAuth login response data '{loginResponseDataRaw}'.");

            return new AccessToken
            {
                Token = oAuthData.Groups["accessToken"].Value,
                Expiry = DateTime.UtcNow.AddSeconds(int.Parse(oAuthData.Groups["expires"].Value)),
                ProviderID = ProviderId
            };
        }
    }
}
