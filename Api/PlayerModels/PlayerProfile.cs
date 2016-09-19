using Google.Protobuf;
using POGOProtos.Data;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Api.InventoryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Rpc;

namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class PlayerProfile : BaseRpc
    {
        private PlayerLocale playerLocale;
        private PlayerData playerData;
        //private EquippedBadge badge;
        private PlayerAvatar avatar;
        private DailyBonus dailyBonus;
        private ContactSettings contactSettings;
        private Dictionary<Currency, int> currencies = new Dictionary<Currency, int>();

        public Stats Stats;

        private TutorialState tutorialState;

        /**
	     * Gets currency.
	     *
	     * @param currency the currency
	     * @return the currency
	     */
        public int GetCurrency(Currency currency)
        {
            return currencies[currency];
        }

        public enum Currency
        {
            STARDUST, POKECOIN
        }

        /**
	     * @param api the api
	     * @throws LoginFailedException  when the auth is invalid
	     * @throws RemoteServerException when the server is down/having issues
	     */
        public PlayerProfile(Client client) : base(client)
        {
            this.playerLocale = new PlayerLocale();
            /*
            if (playerData == null)
            {
                UpdateProfile();
            }
            */
        }

        /**
	     * Updates the player profile with the latest data.
	     *
	     * @throws LoginFailedException  when the auth is invalid
	     * @throws RemoteServerException when the server is down/having issues
	     */
        public async Task UpdateProfile()
        {
            GetPlayerMessage getPlayerReqMsg = new GetPlayerMessage()
            {
                PlayerLocale = playerLocale.GetPlayerLocale()
            };
            Request getPlayerRequest = new Request
            {
                RequestType = RequestType.GetPlayer,
                RequestMessage = getPlayerReqMsg.ToByteString()
            };
            
            Request[] requests = CommonRequest.AppendCheckChallenge(getPlayerRequest);
            RequestEnvelope getPlayerServerRequest = GetRequestBuilder().GetRequestEnvelope(requests);
     
		    try
            {
                var serverResponse = await PostProto<Request>(getPlayerServerRequest);
                var responses = serverResponse.Returns;
                var getPlayerResponse = new GetPlayerResponse();
                getPlayerResponse.MergeFrom(responses[0]);

                UpdateProfile(getPlayerResponse);
		    }
            catch (InvalidProtocolBufferException e)
            {
                throw e;
            }
	    }

        /**
	     * Update the profile with the given response
	     *
	     * @param playerResponse the response
	     */
        public void UpdateProfile(GetPlayerResponse playerResponse)
        {
            UpdateProfile(playerResponse.PlayerData);
        }

        /**
	     * Update the profile with the given player data
	     *
	     * @param playerData the data for update
	     */
        public void UpdateProfile(PlayerData playerData)
        {
            this.playerData = playerData;

            avatar = new PlayerAvatar(playerData.Avatar);
            dailyBonus = new DailyBonus(playerData.DailyBonus);
            contactSettings = new ContactSettings(playerData.ContactSettings);

            // maybe something more graceful?
            foreach (POGOProtos.Data.Player.Currency currency in playerData.Currencies)
            {
                try
                {
                    AddCurrency(currency.Name, currency.Amount);
                }
                catch (Exception)
                {
                    // Error adding currency. You can probably ignore this.
                }
            }

            // Tutorial state
            tutorialState = new TutorialState(playerData.TutorialState);
        }

        /**
	     * Accept the rewards granted and the items unlocked by gaining a trainer level up. Rewards are retained by the
	     * server until a player actively accepts them.
	     * The rewarded items are automatically inserted into the players item bag.
	     *
	     * @param level the trainer level that you want to accept the rewards for
	     * @return a PlayerLevelUpRewards object containing information about the items rewarded and unlocked for this level
	     * @throws LoginFailedException  when the auth is invalid
	     * @throws RemoteServerException when the server is down/having issues
	     * @see PlayerLevelUpRewards
	     */
        public async Task<PlayerLevelUpRewards> AcceptLevelUpRewards(int level)
        {
		    // Check if we even have achieved this level yet
		    if (level > Stats.GetLevel()) {
			    return new PlayerLevelUpRewards(PlayerLevelUpRewards.Status.NotUnlockedYet);
            }

            LevelUpRewardsResponse response = await PostProtoPayload<Request, LevelUpRewardsResponse>(RequestType.LevelUpRewards, new LevelUpRewardsMessage()
            {
                Level = level
            });

            // Add the awarded items to our bag
            ItemBag bag = Client.Inventories.ItemBag;
            foreach (ItemAward itemAward in response.ItemsAwarded)
            { 
			    Item item = bag.GetItem(itemAward.ItemId);
                item.Count = item.Count + itemAward.ItemCount;
		    }
		    // Build a new rewards object and return it
		    return new PlayerLevelUpRewards(response);
	    }

        /**
	     * Add currency.
	     *
	     * @param name   the name
	     * @param amount the amount
	     * @throws InvalidCurrencyException the invalid currency exception
	     */
        public void AddCurrency(string name, int amount)
        {
		    try
            {
                if (name.Equals("STARDUST", StringComparison.InvariantCultureIgnoreCase))
                {
                    currencies[Currency.STARDUST] = amount;
                }
                else if (name.Equals("POKECOIN", StringComparison.InvariantCultureIgnoreCase))
                {
                    currencies[Currency.POKECOIN] = amount;
                }
                else
                {
                    // Should never get here unless another currency type is added.
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        /**
	     * Check and equip badges.
	     *
	     * @throws LoginFailedException  when the auth is invalid
	     * @throws RemoteServerException When a buffer exception is thrown
	     */
        public async Task CheckAndEquipBadges()
        {
            CheckAwardedBadgesResponse response = await PostProtoPayload<Request, CheckAwardedBadgesResponse>(RequestType.CheckAwardedBadges, new CheckAwardedBadgesMessage());
            
		    if (response.Success)
            {
                for (int i = 0; i<response.AwardedBadges.Count; i++) {
                    
                    EquipBadgeMessage msg1 = new EquipBadgeMessage()
                    {
                        BadgeType = response.AwardedBadges[i] //,
                        //BadgeTypeValue = response.AwardedBadgeLevels[i]
                    };
                    
                    EquipBadgeResponse response1;
				    try {
                        response1 = await PostProtoPayload<Request, EquipBadgeResponse>(RequestType.EquipBadge, msg1);
				    } catch (InvalidProtocolBufferException e) {
                        throw e;
				    }
			    }
		    }
	    }

    }

}
