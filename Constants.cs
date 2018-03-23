
using System;

namespace PokemonGo.RocketAPI
{
    public static class Constants
    {
        // API stuff

        public const string ApiUrl = "https://pgorelease.nianticlabs.com/plfe/rpc";

        public const string VersionUrl = "https://pgorelease.nianticlabs.com/plfe/version";

        // Login stuff

        public const string LoginUrl = "https://sso.pokemon.com/sso/login?service=https%3A%2F%2Fsso.pokemon.com%2Fsso%2Foauth2.0%2FcallbackAuthorize";

        public const string LoginUserAgent = "pokemongo/1 CFNetwork/897.1 Darwin/17.5.0"; //iOS 11.3.0
        public const string LoginManufactor = "X-Unity-Version";
        public const string LoginManufactorVersion = "2017.1.2f1"; //"5.5.1f1";//"5.6.1f1";
        public const string LoginHostValue = "sso.pokemon.com";
        public const string LoginOauthUrl = "https://sso.pokemon.com/sso/oauth2.0/accessToken";
        public const string Connection = "keep-alive";
        public const string AcceptLanguage = "en-US";
        public const string AcceptEncoding = "gzip-deflate";
        public const string Accept = "*/*";
        public const string Client_Id = "mobile-app_pokemon-go";
        public const string Redirect_Uri = "https://www.nianticlabs.com/pokemongo/error";
        public const string Client_Secret = "w8ScCUXJQc6kXKw8FiOhd8Fixzht18Dq3PEVkUCP5ZPxtgyWsbTvWHFLm2wNY0JR";
        public const string Grant_Type = "refresh_token";
        public static TimeSpan TimeOut = new TimeSpan(0,10,0);

        public const string GoogleAuthService = "audience:server:client_id:848232511240-7so421jotr2609rmqakceuu1luuq0ptb.apps.googleusercontent.com";
        public const string GoogleAuthApp = "com.nianticlabs.pokemongo";
        public const string GoogleAuthClientSig = "321187995bc7cdc2b5fc91b11a96e2baa8602c62";

        public const string ApiEndPoint = "api/v159_1/hash";
        public const string API_VERSION = "0.95.3";
        public const uint AppVersion = 9500;
        public const long Unknown25 = unchecked((long)0xF522F8878F08FFD6);
        public const string UnknownPlat8Field = "d9ad6deadadfd59abc0e8a85d01e2f6140e6e79b";

        // Login PTC

        public const string RpcUrl = ApiUrl;

        public const string NumberedRpcUrl = @"https://pgorelease.nianticlabs.com/plfe/{0}/rpc";

        public const string PtcLoginUrl = LoginUrl;

        public const string PtcLoginOauth = LoginOauthUrl;
        public const string GoogleGrantRefreshAccessUrl = "https://android.clients.google.com/auth";
    }
}
