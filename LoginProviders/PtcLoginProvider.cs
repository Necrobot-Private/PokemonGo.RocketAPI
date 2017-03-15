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
        public AccessToken GetAccessToken()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.AllowAutoRedirect = false;
                using (var httpClient = new System.Net.Http.HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(Constants.LoginUserAgent);
                    var loginData =  GetLoginData(httpClient);
                    var ticket =  PostLogin(httpClient, _username, _password, loginData);
                    var accessToken =  PostLoginOauth(httpClient, ticket);
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
        private LoginData GetLoginData(System.Net.Http.HttpClient httpClient)
        {
            var loginDataResponse = httpClient.GetAsync(Constants.LoginUrl).Result;
            var loginData = JsonConvert.DeserializeObject<LoginData>( loginDataResponse.Content.ReadAsStringAsync().Result);
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
        private string PostLogin(System.Net.Http.HttpClient httpClient, string username, string password, LoginData loginData)
        {
            var loginResponse =
                httpClient.PostAsync(Constants.LoginUrl, new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"lt", loginData.Lt},
                    {"execution", loginData.Execution},
                    {"_eventId", "submit"},
                    {"username", username},
                    {"password", password}
                })).Result;

            var loginResponseDataRaw = loginResponse.Content.ReadAsStringAsync().Result;
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
        private  AccessToken PostLoginOauth(System.Net.Http.HttpClient httpClient, string ticket)
        {
            var loginResponse =
                httpClient.PostAsync(Constants.LoginOauthUrl, new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", "mobile-app_pokemon-go"},
                    {"redirect_uri", "https://www.nianticlabs.com/pokemongo/error"},
                    {"client_secret", "w8ScCUXJQc6kXKw8FiOhd8Fixzht18Dq3PEVkUCP5ZPxtgyWsbTvWHFLm2wNY0JR"},
                    {"grant_type", "refresh_token"},
                    {"code", ticket}
                })).Result;

            var loginResponseDataRaw = loginResponse.Content.ReadAsStringAsync().Result;

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
