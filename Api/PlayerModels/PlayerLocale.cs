using POGOProtos.Networking.Requests.Messages;
using System;
using System.Globalization;

namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class PlayerLocale
    {

        private GetPlayerMessage.Types.PlayerLocale playerLocale;

        /**
         * Contructor to use the default Locale
         */
        public PlayerLocale()
        {
            CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
            RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);

            // TODO This is untested - need to be sure that we are passing the correct country and language.
            playerLocale = new GetPlayerMessage.Types.PlayerLocale()
            {
                Country = regionInfo.EnglishName,
                Language = cultureInfo.Name
            };
        }

        public GetPlayerMessage.Types.PlayerLocale GetPlayerLocale()
        {
            return playerLocale;
        }

        public String GetCountry()
        {
            return playerLocale.Country;
        }

        public String GetLanguage()
        {
            return playerLocale.Language;
        }
    }

}
