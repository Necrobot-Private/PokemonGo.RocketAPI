using POGOProtos.Enums;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class CandyJar
    {
        private Client client;
	    private Dictionary<PokemonFamilyId, int> candies = new Dictionary<PokemonFamilyId, int>();

        public CandyJar(Client client)
        {
            this.client = client;
        }

        public void Reset()
        {
            candies.Clear();
        }

        /**
         * Sets the number of candies in the jar.
         *
         * @param family  Pokemon family id
         * @param candies Amount to set it to
         */
        public void SetCandy(PokemonFamilyId family, int candies)
        {
            this.candies[family] = candies;
        }

        /**
         * Adds a candy to the candy jar.
         *
         * @param family Pokemon family id
         * @param amount Amount of candies to add
         */
        public void AddCandy(PokemonFamilyId family, int amount)
        {
            if (candies.ContainsKey(family))
            {
                candies[family] = candies[family] + amount;
            }
            else
            {
                candies[family] = amount;
            }
        }

        /**
         * Remove a candy from the candy jar.
         *
         * @param family Pokemon family id
         * @param amount Amount of candies to remove
         */
        public void RemoveCandy(PokemonFamilyId family, int amount)
        {
            if (candies.ContainsKey(family))
            {
                if (candies[family] - amount < 0)
                {
                    candies[family] = 0;
                }
                else
                {
                    candies[family] = candies[family] - amount;
                }
            }
            else
            {
                candies[family] = 0;
            }
        }

        /**
         * Get number of candies from the candyjar.
         *
         * @param family Pokemon family id
         * @return number of candies in jar
         */
        public int GetCandies(PokemonFamilyId family)
        {
            if (candies.ContainsKey(family))
            {
                return this.candies[family];
            }
            else
            {
                return 0;
            }
        }
    }
}
