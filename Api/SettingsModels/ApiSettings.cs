using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class ApiSettings : BaseRpc
    {
        /**
	     * Settings for various parameters on map
	     *
	     * @return MapSettings instance.
	     */
        public MapSettings MapSettings;

	    /**
	     * Settings for various parameters during levelup
	     *
	     * @return LevelUpSettings instance.
	     */
        public LevelUpSettings LevelUpSettings;

	    /**
	     * Settings for various parameters during levelup
	     *
	     * @return LevelUpSettings instance.
	     */
        public FortSettings FortSettings;


        /**
	     * Settings for various parameters during levelup
	     *
	     * @return LevelUpSettings instance.
	     */
        public InventorySettings InventorySettings;

        /**
	     * Settings for showing speed warnings
	     *
	     * @return GpsSettings instance.
	     */
        public GpsSettings GpsSettings;

	    /**
         * Settings for hash
         *
         * @return String hash.
         */
        public String Hash;

        /**
         * Settings object that hold different configuration aspect of the game.
         * Can be used to simulate the real app behaviour.
         *
         * @param api api instance
         */
        public ApiSettings(Client client) : base(client)
        {
            this.MapSettings = new MapSettings();
            this.LevelUpSettings = new LevelUpSettings();
            this.FortSettings = new FortSettings();
            this.InventorySettings = new InventorySettings();
            this.GpsSettings = new GpsSettings();
            this.Hash = "";
        }

        /**
         * Updates settings latest data.
         *
         * @throws LoginFailedException  the login failed exception
         * @throws RemoteServerException the remote server exception
         */
        public async Task UpdateSettings() {
            var downloadSettingsMessage = new DownloadSettingsMessage()
            {
                Hash = this.Hash
            };

            DownloadSettingsResponse response = await PostProtoPayload<Request, DownloadSettingsResponse>(RequestType.DownloadSettings, downloadSettingsMessage);
            UpdateSettings(response);
	    }

	    /**
	     * Updates settings latest data.
	     *
	     * @param response the settings download response
	     */
	    public void UpdateSettings(DownloadSettingsResponse response)
        {
            if (response.Settings != null)
            {
                if (response.Settings.MapSettings != null)
                {
                    MapSettings.Update(response.Settings.MapSettings);
                }
                if (response.Settings.LevelSettings != null)
                {
                    LevelUpSettings.Update(response.Settings.InventorySettings);
                }
                if (response.Settings.FortSettings != null)
                {
                    FortSettings.Update(response.Settings.FortSettings);
                }
                if (response.Settings.InventorySettings != null)
                {
                    InventorySettings.Update(response.Settings.InventorySettings);
                }
                if (response.Settings.GpsSettings != null)
                {
                    GpsSettings.Update(response.Settings.GpsSettings);
                }
            }
            this.Hash = response.Hash;
        }
    }

}
