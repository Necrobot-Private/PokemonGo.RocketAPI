using POGOProtos.Inventory.Item;
using PokemonGo.RocketAPI.Api.Exceptions;
using PokemonGo.RocketAPI.Api.InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class CatchOptions
    {

        private Client client;
        private bool useBestPokeball;
        private bool skipMasterBall;
        private bool useRazzBerries;
        private int maxRazzBerries;
        private Pokeball pokeBall;
        private bool strictBallType;
        private bool smartSelect;

        private int maxPokeballs;
        private double probability;

        private double normalizedHitPosition;

        private double normalizedReticleSize;

        private double spinModifier;

        /**
         * Instantiates a new CatchOptions object.
         *
         * @param api   the api
         */
        public CatchOptions(Client client)
        {
            this.client = client;
            this.useRazzBerries = false;
            this.maxRazzBerries = 0;
            this.useBestPokeball = false;
            this.skipMasterBall = false;
            this.pokeBall = Pokeball.PokeBall;
            this.strictBallType = false;
            this.smartSelect = false;
            this.maxPokeballs = 1;
            this.probability = 0.50;
            this.normalizedHitPosition = 1.0;
            this.normalizedReticleSize = 1.95 + new Random().NextDouble() * 0.05;
            this.spinModifier = 0.85 + new Random().NextDouble() * 0.15;
        }

        /**
         * Gets item ball to catch a pokemon
         *
         * @return the item ball
         * @throws NoSuchItemException   the no such item exception
         */
        public Pokeball GetItemBall()
        {
            ItemBag bag = client.Inventories.ItemBag;
            if (strictBallType) {
                if (bag.GetItem((ItemId)pokeBall).Count > 0)
                {
                    return pokeBall;
                }
                else if (useBestPokeball)
                {
                    if (!skipMasterBall && bag.GetItem(ItemId.ItemMasterBall).Count > 0)
                    {
                        return Pokeball.MasterBall;
                    }
                    else if (bag.GetItem(ItemId.ItemUltraBall).Count > 0)
                    {
                        return Pokeball.UltraBall;
                    }
                    else if (bag.GetItem(ItemId.ItemGreatBall).Count > 0)
                    {
                        return Pokeball.GreatBall;
                    }
                }
                if (bag.GetItem(ItemId.ItemPokeBall).Count > 0)
                {
                    return Pokeball.PokeBall;
                }
                throw new NoSuchItemException();
            }
            else
            {
                int index = new List<ItemId>(new ItemId[] { ItemId.ItemMasterBall, ItemId.ItemUltraBall,
                    ItemId.ItemGreatBall, ItemId.ItemPokeBall }).IndexOf((ItemId)pokeBall);

                if (useBestPokeball)
                {
                    if (!skipMasterBall && index >= 0 && bag.GetItem(ItemId.ItemMasterBall).Count > 0)
                    {
                        return Pokeball.MasterBall;
                    }
                    else if (index >= 1 && bag.GetItem(ItemId.ItemUltraBall).Count > 0)
                    {
                        return Pokeball.UltraBall;
                    }
                    else if (index >= 2 && bag.GetItem(ItemId.ItemGreatBall).Count > 0)
                    {
                        return Pokeball.GreatBall;
                    }
                    else if (bag.GetItem(ItemId.ItemPokeBall).Count > 0)
                    {
                        return Pokeball.PokeBall;
                    }
                }
                else
                {
                    if (index <= 3 && bag.GetItem(ItemId.ItemPokeBall).Count > 0)
                    {
                        return Pokeball.PokeBall;
                    }
                    else if (index <= 2 && bag.GetItem(ItemId.ItemGreatBall).Count > 0)
                    {
                        return Pokeball.GreatBall;
                    }
                    else if (index <= 1 && bag.GetItem(ItemId.ItemUltraBall).Count > 0)
                    {
                        return Pokeball.UltraBall;
                    }
                    else if (!skipMasterBall && bag.GetItem(ItemId.ItemMasterBall).Count > 0)
                    {
                        return Pokeball.MasterBall;
                    }
                }
            }
            if (smartSelect)
            {
                useBestPokeball = false;
                skipMasterBall = false;
                smartSelect = false;
                return GetItemBall();
            }
            throw new NoSuchItemException();
        }

        /**
	     * Gets item ball to catch a pokemon
	     *
	     * @param  encounterProbability  the capture probability to compare
	     * @return the item ball
	     * @throws LoginFailedException  the login failed exception
	     * @throws RemoteServerException the remote server exception
	     * @throws NoSuchItemException   the no such item exception
	     */
        public Pokeball GetItemBall(double encounterProbability)
        {
            if (encounterProbability >= probability) {
                useBestPokeball = false;
            } else {
                useBestPokeball = true;
            }
            return GetItemBall();
        }

        /**
	     * Gets razzberries to catch a pokemon
	     *
	     * @return the number to use
	     */
        public int GetRazzberries()
        {
            return useRazzBerries && maxRazzBerries == 0 ? 1 : maxRazzBerries;
        }

        /**
         * Enable or disable the use of razzberries
         *
         * @param useRazzBerries true or false
         * @return               the CatchOptions object
         */
        public CatchOptions UseRazzberries(bool useRazzBerries)
        {
            this.useRazzBerries = useRazzBerries;
            return this;
        }

        public bool GetUseRazzberries()
        {
            return this.useRazzBerries;
        }

        /**
         * Set a maximum number of razzberries
         *
         * @param maxRazzBerries maximum allowed
         * @return               the CatchOptions object
         */
        public CatchOptions MaxRazzberries(int maxRazzBerries)
        {
            this.maxRazzBerries = maxRazzBerries;
            return this;
        }

        /**
         * Set a specific Pokeball to use
         *
         * @param pokeBall the pokeball to use
         * @return         the CatchOptions object
         */
        public CatchOptions UsePokeball(Pokeball pokeBall)
        {
            this.pokeBall = pokeBall;
            return this;
        }

        /**
         * Set using the best available ball
         *
         * @param useBestPokeball true or false
         * @return                the CatchOptions object
         */
        public CatchOptions UseBestBall(bool useBestPokeball)
        {
            this.useBestPokeball = useBestPokeball;
            return this;
        }

        /**
         * <pre>
         * Set using only the defined ball type
         *   combined with useBestBall: Sets the minimum
         *   combined with usePokeball: Sets the maximum
         *
         *   without either will attempt the ball specified
         *       or throw an error
         * </pre>
         * @param strictBallType  true or false
         * @return                the CatchOptions object
         */
        public CatchOptions NoFallback(bool strictBallType)
        {
            this.strictBallType = strictBallType;
            return this;
        }

        /**
         * Set whether or not Master balls can be used
         *
         * @param skipMasterBall true or false
         * @return               the CatchOptions object
         */
        public CatchOptions NoMasterBall(bool skipMasterBall)
        {
            this.skipMasterBall = skipMasterBall;
            return this;
        }

        /**
         * Set whether or not to use adaptive ball selection
         *
         * @param smartSelect    true or false
         * @return               the CatchOptions object
         */
        public CatchOptions UseSmartSelect(bool smartSelect)
        {
            this.smartSelect = smartSelect;
            return this;
        }

        /**
         * Set a maximum number of pokeballs
         *
         * @param maxPokeballs maximum allowed
         * @return             the CatchOptions object
         */
        public CatchOptions MaxPokeballs(int maxPokeballs)
        {
            if (maxPokeballs <= 1)
                maxPokeballs = -1;
            this.maxPokeballs = maxPokeballs;
            return this;
        }

        public int GetMaxPokeballs()
        {
            return this.maxPokeballs;
        }

        /**
         * Set a capture probability before switching balls
         *		or the minimum probability for a specific ball
         *
         * @param probability    the probability
         * @return               the AsyncCatchOptions object
         */
        public CatchOptions WithProbability(double probability)
        {
            this.probability = probability;
            return this;
        }

        /**
         * Set the normalized hit position of a pokeball throw
         *
         * @param normalizedHitPosition the normalized position
         * @return                      the CatchOptions object
         */
        public CatchOptions SetNormalizedHitPosition(double normalizedHitPosition)
        {
            this.normalizedHitPosition = normalizedHitPosition;
            return this;
        }

        public double GetNormalizedHitPosition()
        {
            return this.normalizedHitPosition;
        }

        /**
         * Set the normalized reticle for a pokeball throw
         *
         * @param normalizedReticleSize the normalized size
         * @return                      the CatchOptions object
         */
        public CatchOptions SetNormalizedReticleSize(double normalizedReticleSize)
        {
            this.normalizedReticleSize = normalizedReticleSize;
            return this;
        }

        public double GetNormalizedReticleSize()
        {
            return this.normalizedReticleSize;
        }

        /**
         * Set the spin modifier of a pokeball throw
         *
         * @param spinModifier the spin modifier
         * @return             the CatchOptions object
         */
        public CatchOptions SetSpinModifier(double spinModifier)
        {
            this.spinModifier = spinModifier;
            return this;
        }

        public double GetSpinModifier()
        { 
            return this.spinModifier;
        }

    }
}
