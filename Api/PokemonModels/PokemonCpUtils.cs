using POGOProtos.Enums;
using System;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    /**
     * Information in this class is based on:
     * http://pokemongo.gamepress.gg/cp-multiplier
     * and
     * http://pokemongo.gamepress.gg/pokemon-stats-advanced
     */
    public class PokemonCpUtils
    {
        private static Dictionary<double, double> LEVEL_CP_MULTIPLIER = new Dictionary<double, double>();

        static PokemonCpUtils()
        { 
		    LEVEL_CP_MULTIPLIER[1f] = 0.094f;
		    LEVEL_CP_MULTIPLIER[1.5f] = 0.135137432f;
		    LEVEL_CP_MULTIPLIER[2f] = 0.16639787f;
		    LEVEL_CP_MULTIPLIER[2.5f] = 0.192650919f;
		    LEVEL_CP_MULTIPLIER[3f] = 0.21573247f;
		    LEVEL_CP_MULTIPLIER[3.5f] = 0.236572661f;
		    LEVEL_CP_MULTIPLIER[4f] = 0.25572005f;
		    LEVEL_CP_MULTIPLIER[4.5f] = 0.273530381f;
		    LEVEL_CP_MULTIPLIER[5f] = 0.29024988f;
		    LEVEL_CP_MULTIPLIER[5.5f] = 0.306057377f;
		    LEVEL_CP_MULTIPLIER[6f] = 0.3210876f;
		    LEVEL_CP_MULTIPLIER[6.5f] = 0.335445036f;
		    LEVEL_CP_MULTIPLIER[7f] = 0.34921268f;
		    LEVEL_CP_MULTIPLIER[7.5f] = 0.362457751f;
		    LEVEL_CP_MULTIPLIER[8f] = 0.37523559f;
		    LEVEL_CP_MULTIPLIER[8.5f] = 0.387592406f;
		    LEVEL_CP_MULTIPLIER[9f] = 0.39956728f;
		    LEVEL_CP_MULTIPLIER[9.5f] = 0.411193551f;
		    LEVEL_CP_MULTIPLIER[10f] = 0.42250001f;
		    LEVEL_CP_MULTIPLIER[10.5f] = 0.432926419f;
		    LEVEL_CP_MULTIPLIER[11f] = 0.44310755f;
		    LEVEL_CP_MULTIPLIER[11.5f] = 0.453059958f;
		    LEVEL_CP_MULTIPLIER[12f] = 0.46279839f;
		    LEVEL_CP_MULTIPLIER[12.5f] = 0.472336083f;
		    LEVEL_CP_MULTIPLIER[13f] = 0.48168495f;
		    LEVEL_CP_MULTIPLIER[13.5f] = 0.4908558f;
		    LEVEL_CP_MULTIPLIER[14f] = 0.49985844f;
		    LEVEL_CP_MULTIPLIER[14.5f] = 0.508701765f;
		    LEVEL_CP_MULTIPLIER[15f] = 0.51739395f;
		    LEVEL_CP_MULTIPLIER[15.5f] = 0.525942511f;
		    LEVEL_CP_MULTIPLIER[16f] = 0.53435433f;
		    LEVEL_CP_MULTIPLIER[16.5f] = 0.542635767f;
		    LEVEL_CP_MULTIPLIER[17f] = 0.55079269f;
		    LEVEL_CP_MULTIPLIER[17.5f] = 0.558830576f;
		    LEVEL_CP_MULTIPLIER[18f] = 0.56675452f;
		    LEVEL_CP_MULTIPLIER[18.5f] = 0.574569153f;
		    LEVEL_CP_MULTIPLIER[19f] = 0.58227891f;
		    LEVEL_CP_MULTIPLIER[19.5f] = 0.589887917f;
		    LEVEL_CP_MULTIPLIER[20f] = 0.59740001f;
		    LEVEL_CP_MULTIPLIER[20.5f] = 0.604818814f;
		    LEVEL_CP_MULTIPLIER[21f] = 0.61215729f;
		    LEVEL_CP_MULTIPLIER[21.5f] = 0.619399365f;
		    LEVEL_CP_MULTIPLIER[22f] = 0.62656713f;
		    LEVEL_CP_MULTIPLIER[22.5f] = 0.633644533f;
		    LEVEL_CP_MULTIPLIER[23f] = 0.64065295f;
		    LEVEL_CP_MULTIPLIER[23.5f] = 0.647576426f;
		    LEVEL_CP_MULTIPLIER[24f] = 0.65443563f;
		    LEVEL_CP_MULTIPLIER[24.5f] = 0.661214806f;
		    LEVEL_CP_MULTIPLIER[25f] = 0.667934f;
		    LEVEL_CP_MULTIPLIER[25.5f] = 0.674577537f;
		    LEVEL_CP_MULTIPLIER[26f] = 0.68116492f;
		    LEVEL_CP_MULTIPLIER[26.5f] = 0.687680648f;
		    LEVEL_CP_MULTIPLIER[27f] = 0.69414365f;
		    LEVEL_CP_MULTIPLIER[27.5f] = 0.700538673f;
		    LEVEL_CP_MULTIPLIER[28f] = 0.70688421f;
		    LEVEL_CP_MULTIPLIER[28.5f] = 0.713164996f;
		    LEVEL_CP_MULTIPLIER[29f] = 0.71939909f;
		    LEVEL_CP_MULTIPLIER[29.5f] = 0.725571552f;
		    LEVEL_CP_MULTIPLIER[30f] = 0.7317f;
		    LEVEL_CP_MULTIPLIER[30.5f] = 0.734741009f;
		    LEVEL_CP_MULTIPLIER[31f] = 0.73776948f;
		    LEVEL_CP_MULTIPLIER[31.5f] = 0.740785574f;
		    LEVEL_CP_MULTIPLIER[32f] = 0.74378943f;
		    LEVEL_CP_MULTIPLIER[32.5f] = 0.746781211f;
		    LEVEL_CP_MULTIPLIER[33f] = 0.74976104f;
		    LEVEL_CP_MULTIPLIER[33.5f] = 0.752729087f;
		    LEVEL_CP_MULTIPLIER[34f] = 0.75568551f;
		    LEVEL_CP_MULTIPLIER[34.5f] = 0.758630378f;
		    LEVEL_CP_MULTIPLIER[35f] = 0.76156384f;
		    LEVEL_CP_MULTIPLIER[35.5f] = 0.764486065f;
		    LEVEL_CP_MULTIPLIER[36f] = 0.76739717f;
		    LEVEL_CP_MULTIPLIER[36.5f] = 0.770297266f;
		    LEVEL_CP_MULTIPLIER[37f] = 0.7731865f;
		    LEVEL_CP_MULTIPLIER[37.5f] = 0.776064962f;
		    LEVEL_CP_MULTIPLIER[38f] = 0.77893275f;
		    LEVEL_CP_MULTIPLIER[38.5f] = 0.781790055f;
		    LEVEL_CP_MULTIPLIER[39f] = 0.78463697f;
		    LEVEL_CP_MULTIPLIER[39.5f] = 0.787473578f;
		    LEVEL_CP_MULTIPLIER[40f] = 0.79030001f;
	    }

        private static double GetLevel(double combinedCpMultiplier)
        {
            double level;
            if (combinedCpMultiplier < 0.734f)
            {
                // compute polynomial approximation obtained by regression
                level = 58.35178527f * combinedCpMultiplier * combinedCpMultiplier
                        - 2.838007664f * combinedCpMultiplier + 0.8539209906f;
            }
            else
            {
                // compute linear approximation obtained by regression
                level = 171.0112688f * combinedCpMultiplier - 95.20425243f;
            }
            // round to nearest .5 value and return
            return Math.Round((level) * 2) / 2.0f;
        }

        /**
	     * Get the level from the cp multiplier
	     *
	     * @param combinedCpMultiplier All CP multiplier values combined
	     * @return Level
	     */
        public static double GetLevelFromCpMultiplier(double combinedCpMultiplier)
        {
            return GetLevel(combinedCpMultiplier);
        }

        /**
	     * Get the maximum CP from the values
	     *
	     * @param attack  All attack values combined
	     * @param defense All defense values combined
	     * @param stamina All stamina values combined
	     * @return Maximum CP for these levels
	     */
        public static int GetMaxCp(int attack, int defense, int stamina)
        {
            return GetMaxCpForPlayer(attack, defense, stamina, 40);
        }

        /**
	     * Get the absolute maximum CP for pokemons with their PokemonId.
	     *
	     * @param id The {@link PokemonIdOuterClass.PokemonId} of the Pokemon to get CP for.
	     * @return The absolute maximum CP
	     * @throws NoSuchItemException If the PokemonId value cannot be found in the {@link PokemonMetaRegistry}.
	     */
        public static int GetAbsoluteMaxCp(PokemonId id)
        {
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(id);
		    if (pokemonMeta == null) {
                throw new Exception("Cannot find meta data for " + id);
            }
            int attack = 15 + pokemonMeta.BaseAttack;
            int defense = 15 + pokemonMeta.BaseDefense;
            int stamina = 15 + pokemonMeta.BaseStamina;
		    return GetMaxCpForPlayer(attack, defense, stamina, 40);
        }

        /**
	     * Get the maximum CP from the values
	     *
	     * @param attack      All attack values combined
	     * @param defense     All defense values combined
	     * @param stamina     All stamina values combined
	     * @param playerLevel The player level
	     * @return Maximum CP for these levels
	     */
        public static int GetMaxCpForPlayer(int attack, int defense, int stamina, int playerLevel)
        {
            double maxLevel = Math.Min(playerLevel + 1.5f, 40f);
            double maxCpMultplier = LEVEL_CP_MULTIPLIER[maxLevel];
            return GetCp(attack, defense, stamina, maxCpMultplier);
        }

        /**
	     * Calculate CP based on raw values
	     *
	     * @param attack               All attack values combined
	     * @param defense              All defense values combined
	     * @param stamina              All stamina values combined
	     * @param combinedCpMultiplier All CP multiplier values combined
	     * @return CP
	     */
        public static int GetCp(int attack, int defense, int stamina, double combinedCpMultiplier)
        {
            return (int)Math.Round(attack * Math.Pow(defense, 0.5) * Math.Pow(stamina, 0.5)
                    * Math.Pow(combinedCpMultiplier, 2) / 10f);
        }

        /**
	     * Get the CP after powerup
	     *
	     * @param cp                   Current CP level
	     * @param combinedCpMultiplier All CP multiplier values combined
	     * @return New CP
	     */
        public static int GetCpAfterPowerup(int cp, double combinedCpMultiplier)
        {
            // Based on http://pokemongo.gamepress.gg/power-up-costs
            double level = GetLevelFromCpMultiplier(combinedCpMultiplier);
            if (level <= 10)
            {
                return cp + (int)Math.Round((cp * 0.009426125469) / Math.Pow(combinedCpMultiplier, 2));
            }
            if (level <= 20)
            {
                return cp + (int)Math.Round((cp * 0.008919025675) / Math.Pow(combinedCpMultiplier, 2));
            }
            if (level <= 30)
            {
                return cp + (int)Math.Round((cp * 0.008924905903) / Math.Pow(combinedCpMultiplier, 2));
            }
            return cp + (int)Math.Round((cp * 0.00445946079) / Math.Pow(combinedCpMultiplier, 2));
        }

        /**
	     * Get the new additional multiplier after powerup
	     *
	     * @param cpMultiplier           Multiplier
	     * @param additionalCpMultiplier Additional multiplier
	     * @return Additional CP multiplier after upgrade
	     */
        public static double GetAdditionalCpMultiplierAfterPowerup(double cpMultiplier, double additionalCpMultiplier)
        {
            double nextLevel = GetLevelFromCpMultiplier(cpMultiplier + additionalCpMultiplier) + .5f;
            return LEVEL_CP_MULTIPLIER[nextLevel] - cpMultiplier;
        }

        /**
	     * Get the amount of stardust required to do a powerup
	     *
	     * @param combinedCpMultiplier All CP multiplier values combined
	     * @param powerups             Number of previous powerups
	     * @return Amount of stardust
	     */
        public static int GetStartdustCostsForPowerup(double combinedCpMultiplier, int powerups)
        {
            // Based on http://pokemongo.gamepress.gg/power-up-costs
            double level = GetLevelFromCpMultiplier(combinedCpMultiplier);
            if (level < 3 && powerups <= 4)
            {
                return 200;
            }
            if (level < 5 && powerups <= 8)
            {
                return 400;
            }
            if (level < 7 && powerups <= 12)
            {
                return 600;
            }
            if (level < 9 && powerups <= 16)
            {
                return 800;
            }
            if (level < 11 && powerups <= 20)
            {
                return 1000;
            }
            if (level < 13 && powerups <= 24)
            {
                return 1300;
            }
            if (level < 15 && powerups <= 28)
            {
                return 1600;
            }
            if (level < 17 && powerups <= 32)
            {
                return 1900;
            }
            if (level < 19 && powerups <= 36)
            {
                return 2200;
            }
            if (level < 21 && powerups <= 40)
            {
                return 2500;
            }
            if (level < 23 && powerups <= 44)
            {
                return 3000;
            }
            if (level < 25 && powerups <= 48)
            {
                return 3500;
            }
            if (level < 27 && powerups <= 52)
            {
                return 4000;
            }
            if (level < 29 && powerups <= 56)
            {
                return 4500;
            }
            if (level < 31 && powerups <= 60)
            {
                return 5000;
            }
            if (level < 33 && powerups <= 64)
            {
                return 6000;
            }
            if (level < 35 && powerups <= 68)
            {
                return 7000;
            }
            if (level < 37 && powerups <= 72)
            {
                return 8000;
            }
            if (level < 39 && powerups <= 76)
            {
                return 9000;
            }
            return 10000;
        }

        /**
	     * Get the amount of candy required to do a powerup
	     *
	     * @param combinedCpMultiplier All CP multiplier values combined
	     * @param powerups             Number of previous powerups
	     * @return Amount of candy
	     */
        public static int GetCandyCostsForPowerup(double combinedCpMultiplier, int powerups)
        {
            // Based on http://pokemongo.gamepress.gg/power-up-costs
            double level = GetLevelFromCpMultiplier(combinedCpMultiplier);
            if (level < 11 && powerups <= 20)
            {
                return 1;
            }
            if (level < 21 && powerups <= 40)
            {
                return 2;
            }
            if (level < 31 && powerups <= 60)
            {
                return 3;
            }
            return 4;
        }
    }
}
