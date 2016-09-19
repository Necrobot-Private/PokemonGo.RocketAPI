using Google.Protobuf;
using POGOProtos.Data;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.InventoryModels;
using PokemonGo.RocketAPI.Api.PlayerModels;
using System;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class Pokemon : PokemonDetails
    {
        public int Stamina;

        public Pokemon(Client client, PokemonData proto) : base(client, proto)
        {
        }

        /**
	     * Transfers the pokemon.
	     *
	     * @return the result
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
        public async Task<ReleasePokemonResponse.Types.Result> TransferPokemon()
        {
            var reqMsg = new ReleasePokemonMessage
            {
                PokemonId = GetId()
            };

            ReleasePokemonResponse response;
            
		    try {
			    response = await PostProtoPayload<Request, ReleasePokemonResponse>(RequestType.ReleasePokemon, reqMsg);
            } catch (InvalidProtocolBufferException) {
                return ReleasePokemonResponse.Types.Result.Failed;
		    }

            // TODO: Review - In the original code this block was commented in.
            /*
		    if (response.Result == ReleasePokemonResponse.Types.Result.Success) {
			    Client.Inventories.PokeBank.RemovePokemon(this);
		    }
            */

            Client.Inventories.PokeBank.RemovePokemon(this);

            await Client.Inventories.UpdateInventories();

            return response.Result;
	    }

        /**
	 * Rename pokemon nickname pokemon response . result.
	 *
	 * @param nickname the nickname
	 * @return the nickname pokemon response . result
	 * @throws LoginFailedException  the login failed exception
	 * @throws RemoteServerException the remote server exception
	 */
        public async Task<NicknamePokemonResponse.Types.Result> RenamePokemon(string nickname)
        {
            var reqMsg = new NicknamePokemonMessage()
            {
                PokemonId = GetId(),
                Nickname = nickname
            };

            NicknamePokemonResponse response = await PostProtoPayload<Request, NicknamePokemonResponse>(RequestType.NicknamePokemon, reqMsg);

            Client.Inventories.PokeBank.RemovePokemon(this);
            await Client.Inventories.UpdateInventories();

            return response.Result;
	    }

        /**
	     * Function to mark the pokemon as favorite or not.
	     *
	     * @param markFavorite Mark Pokemon as Favorite?
	     * @return the SetFavoritePokemonResponse.Result
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
        public async Task<SetFavoritePokemonResponse.Types.Result> SetFavoritePokemon(bool markFavorite)
        {
            var reqMsg = new SetFavoritePokemonMessage()
            {
                PokemonId = (long)GetId(),
                IsFavorite = markFavorite
            };

            SetFavoritePokemonResponse response = await PostProtoPayload<Request, SetFavoritePokemonResponse>(RequestType.SetFavoritePokemon, reqMsg);
            
            Client.Inventories.PokeBank.RemovePokemon(this);
            await Client.Inventories.UpdateInventories();

            return response.Result;
	    }

        /**
	     * Check if can powers up this pokemon
	     *
	     * @return the boolean
	     */
        public bool CanPowerUp()
        {
            return GetCandy() >= GetCandyCostsForPowerup() && Client.PlayerProfile
                    .GetCurrency(PlayerProfile.Currency.STARDUST) >= GetStardustCostsForPowerup();
        }

        /**
	     * Check if can powers up this pokemon, you can choose whether or not to consider the max cp limit for current
	     * player level passing true to consider and false to not consider.
	     *
	     * @param considerMaxCPLimitForPlayerLevel Consider max cp limit for actual player level
	     * @return the boolean
	     * @throws NoSuchItemException   If the PokemonId value cannot be found in the {@link PokemonMetaRegistry}.
	     */
        public bool CanPowerUp(bool considerMaxCPLimitForPlayerLevel)
        {
    		return considerMaxCPLimitForPlayerLevel
				? this.CanPowerUp() && (this.GetCp() < this.GetMaxCpForPlayer())
				: CanPowerUp();
        }

        /**
	     * Check if can evolve this pokemon
	     *
	     * @return the boolean
	     */
        public bool CanEvolve()
        {
            return !EvolutionInfo.IsFullyEvolved(GetPokemonId()) && (GetCandy() >= GetCandiesToEvolve());
        }

        /**
	     * Powers up a pokemon with candy and stardust.
	     * After powering up this pokemon object will reflect the new changes.
	     *
	     * @return The result
	     */
        public async Task<UpgradePokemonResponse.Types.Result> PowerUp()
        {
            var reqMsg = new UpgradePokemonMessage()
            {
                PokemonId = GetId()
            };

            UpgradePokemonResponse response = await PostProtoPayload<Request, UpgradePokemonResponse>(RequestType.UpgradePokemon, reqMsg);
            
            //set new pokemon details
            this.proto = response.UpgradedPokemon;
            return response.Result;
        }

        /**
	     * dus
	     * Evolve evolution result.
	     *
	     * @return the evolution result
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     */
        public async Task<EvolutionResult> Evolve()
        {
            var reqMsg = new EvolvePokemonMessage
            {
                PokemonId = GetId()
            };

            EvolvePokemonResponse response = await PostProtoPayload<Request, EvolvePokemonResponse>(RequestType.EvolvePokemon, reqMsg);
            
            EvolutionResult result = new EvolutionResult(Client, response);

            Client.Inventories.PokeBank.RemovePokemon(this);

            await Client.Inventories.UpdateInventories();

		    return result;
	    }

        /**
	     * Check if pokemon its injured but not fainted. need potions to heal
	     *
	     * @return true if pokemon is injured
	     */
        public bool IsInjured()
        {
            return !IsFainted() && this.Stamina < GetMaxStamina();
        }

        /**
         * check if a pokemon it's died (fainted). need a revive to resurrect
         *
         * @return true if a pokemon is fainted
         */
        public bool IsFainted()
        {
            return this.Stamina == 0;
        }

        /**
	     * Heal a pokemon, using various fallbacks for potions
	     *
	     * @return Result, ERROR_CANNOT_USE if the requirements arent met
	     * @throws LoginFailedException  If login failed.
	     * @throws RemoteServerException If server communication issues occurred.
	     */
        public async Task<UseItemPotionResponse.Types.Result> Heal()
        {
		    if (!IsInjured())
			    return UseItemPotionResponse.Types.Result.ErrorCannotUse;

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemPotion).Count > 0)
			    return await UsePotion(ItemId.ItemPotion);

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemSuperPotion).Count > 0)
                return await UsePotion(ItemId.ItemSuperPotion);

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemHyperPotion).Count > 0)
                return await UsePotion(ItemId.ItemHyperPotion);

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemMaxPotion).Count > 0)
                return await UsePotion(ItemId.ItemMaxPotion);

		    return UseItemPotionResponse.Types.Result.ErrorCannotUse;
	    }

        /**
	     * use a potion on that pokemon. Will check if there is enough potions and if the pokemon need
	     * to be healed.
	     *
	     * @param itemId {@link ItemId} of the potion to use.
	     * @return Result, ERROR_CANNOT_USE if the requirements aren't met
	     * @throws LoginFailedException  If login failed.
	     * @throws RemoteServerException If server communications failed.
	     */
        public async Task<UseItemPotionResponse.Types.Result> UsePotion(ItemId itemId)
        {
            Item potion = Client.Inventories.ItemBag.GetItem(itemId);
		    //some sanity check, to prevent wrong use of this call
		    if (!potion.IsPotion() || potion.Count < 1 || !IsInjured())
			    return UseItemPotionResponse.Types.Result.ErrorCannotUse;

            var reqMsg = new UseItemPotionMessage()
            {
                ItemId = itemId,
                PokemonId = GetId()
            };

            UseItemPotionResponse response;
		    try {
                response = await PostProtoPayload<Request, UseItemPotionResponse>(RequestType.UseItemPotion, reqMsg);
			    if (response.Result == UseItemPotionResponse.Types.Result.Success) {
                    this.Stamina = response.Stamina;
			    }
                return response.Result;
		    } catch (InvalidProtocolBufferException e) {
                throw e;
		    }
	    }

        /**
	     * Revive a pokemon, using various fallbacks for revive items
	     *
	     * @return Result, ERROR_CANNOT_USE if the requirements arent met
	     * @throws LoginFailedException  If login failed.
	     * @throws RemoteServerException If server communications failed.
	     */
        public async Task<UseItemReviveResponse.Types.Result> Revive()
        {
            if (!IsFainted())
			    return UseItemReviveResponse.Types.Result.ErrorCannotUse;

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemRevive).Count > 0)
			    return await UseRevive(ItemId.ItemRevive);

		    if (Client.Inventories.ItemBag.GetItem(ItemId.ItemMaxRevive).Count > 0)
			    return await UseRevive(ItemId.ItemMaxRevive);

		    return UseItemReviveResponse.Types.Result.ErrorCannotUse;
	    }

        /**
	     * Use a revive item on the pokemon. Will check if there is enough revive &amp; if the pokemon need
	     * to be revived.
	     *
	     * @param itemId {@link ItemId} of the Revive to use.
	     * @return Result, ERROR_CANNOT_USE if the requirements arent met
	     * @throws LoginFailedException  If login failed.
	     * @throws RemoteServerException If server communications failed.
	     */
        public async Task<UseItemReviveResponse.Types.Result> UseRevive(ItemId itemId)
        {
            Item item = Client.Inventories.ItemBag.GetItem(itemId);
		    if (!item.IsRevive() || item.Count < 1 || !IsFainted())
			    return UseItemReviveResponse.Types.Result.ErrorCannotUse;

            var reqMsg = new UseItemReviveMessage()
            {
                ItemId = itemId,
                PokemonId = GetId()
            };

            UseItemReviveResponse response;
		    try {
			    response = await PostProtoPayload<Request, UseItemReviveResponse>(RequestType.UseItemRevive, reqMsg);
                if (response.Result == UseItemReviveResponse.Types.Result.Success) {
                    this.Stamina = response.Stamina;
			    }
                return response.Result;
            } catch (InvalidProtocolBufferException e) {
                throw e;
		    }
	    }

        public EvolutionForm GetEvolutionForm()
        {
            return new EvolutionForm(GetPokemonId());
        }
        
        /**
         * @return Actual stamina in percentage relative to the current maximum stamina (useful in ProgressBars)
         */
        public int GetStaminaInPercentage()
        {
            return (this.Stamina * 100) / GetMaxStamina();
        }

        /**
	     * Actual cp in percentage relative to the maximum cp that this pokemon can reach
	     * at the actual player level (useful in ProgressBars)
	     *
	     * @return Actual cp in percentage
	     * @throws NoSuchItemException   if threw from {@link #getMaxCpForPlayer()}
	     */
        public int GetCPInPercentageActualPlayerLevel()
        {
    		return ((GetCp() * 100) / GetMaxCpForPlayer());
        }

        /**
	     * Actual cp in percentage relative to the maximum cp that this pokemon can reach at player-level 40
	     * (useful in ProgressBars)
	     *
	     * @return Actual cp in percentage
	     * @throws NoSuchItemException if threw from {@link #getMaxCp()}
	     */
        public int GetCPInPercentageMaxPlayerLevel()
        {
		    return ((GetCp() * 100) / GetMaxCp());
        }

        /**
         * @return IV in percentage
         */
        public double GetIvInPercentage()
        {
            return ((Math.Floor((this.GetIvRatio() * 100) * 100)) / 100);
        }
    }
}
