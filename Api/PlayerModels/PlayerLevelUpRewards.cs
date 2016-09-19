using Google.Protobuf.Collections;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Responses;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    /**
     * A data class containing the results of a trainer level up. This includes a list of items received for this level up,
     * a list of items which were unlocked by this level up (for example razz berries)
     * and the status of these level up results.
     * If the rewards for this level up have been
     * accepted in the past the status will be ALREADY_ACCEPTED, if this level up has not yet been achieved
     * by the player it will be NOT_UNLOCKED_YET otherwise it will be NEW.
     *
     * @author Alex Schlosser
     */
    public class PlayerLevelUpRewards
    {
        private Status status;
	    private IList<ItemAward> rewards;
        private IList<ItemId> unlockedItems;


        /**
         * Create new empty result object with the specified status.
         *
         * @param status the status of this result
         */
        public PlayerLevelUpRewards(Status status)
        {
            this.status = status;
            this.rewards = new RepeatedField<ItemAward>();
            this.unlockedItems = new RepeatedField<ItemId>();
        }

        public enum Status
        {
            AlreadyAccepted,
            New,
            NotUnlockedYet
        }

        /**
         * Create a new result object based on a server response
         *
         * @param response the response which contains the request results
         */
        public PlayerLevelUpRewards(LevelUpRewardsResponse response)
        {
            this.rewards = response.ItemsAwarded;
            this.unlockedItems = response.ItemsUnlocked;
            this.status = (rewards.Count == 0 ? Status.AlreadyAccepted : Status.New);
        }
    }

}
