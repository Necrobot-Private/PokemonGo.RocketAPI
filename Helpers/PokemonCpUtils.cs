using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Settings.Master;
using POGOProtos.Settings.Master.Pokemon;
using System;
using System.Collections.Generic;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse.Types;

namespace PokemonGo.RocketAPI.Helpers
{
    public class PokemonCpUtils
    {
        private static Dictionary<float, double> LEVEL_CP_MULTIPLIER = new Dictionary<float, double>();

        public static void Initialize(RepeatedField<ItemTemplate> templates)
        {
            foreach (ItemTemplate template in templates)
            {
                if (template.PlayerLevel != null)
                {
                    PlayerLevelSettings settings = template.PlayerLevel;
                    RepeatedField<float> multipliers = settings.CpMultiplier;
                    for (int i = 0; i < multipliers.Count; i++)
                    {
                        double multiplier = multipliers[i];
                        LEVEL_CP_MULTIPLIER[i + 1.0F] =  multiplier;
                        double nextMultiplier = multipliers[Math.Min(multipliers.Count - 1, i + 1)];
                        double step = ((nextMultiplier * nextMultiplier) - (multiplier * multiplier)) / 2.0F;
                        if (i >= 30)
                        {
                            step /= 2.0;
                        }
                        LEVEL_CP_MULTIPLIER[i + 1.5F] = Math.Sqrt((multiplier * multiplier) + step);
                    }
                }
            }
        }

        private static float getLevel(double combinedCpMultiplier)
        {
            double level;
            if (combinedCpMultiplier < 0.734f)
            {
                // compute polynomial approximation obtained by regression
                level = 58.35178527 * combinedCpMultiplier * combinedCpMultiplier
                        - 2.838007664 * combinedCpMultiplier + 0.8539209906;
            }
            else
            {
                // compute linear approximation obtained by regression
                level = 171.0112688 * combinedCpMultiplier - 95.20425243;
            }
            // round to nearest .5 value and return
            return (float)(Math.Round((level) * 2) / 2.0);
        }

        public static float GetLevelFromCpMultiplier(double combinedCpMultiplier)
        {
            return getLevel(combinedCpMultiplier);
        }

        public static int GetMaxCp(int attack, int defense, int stamina)
        {
            return GetMaxCpForPlayer(attack, defense, stamina, 40);
        }

        public static int GetAbsoluteMaxCp(PokemonId id)
        {
            PokemonSettings settings = PokemonMeta.GetPokemonSettings(id);
		    if (settings == null) {
                throw new Exception("Cannot find meta data for " + id);
            }
            StatsAttributes stats = settings.Stats;

            int attack = 15 + stats.BaseAttack;
            int defense = 15 + stats.BaseDefense;
            int stamina = 15 + stats.BaseStamina;
		    return GetMaxCpForPlayer(attack, defense, stamina, 40);
        }

        public static int GetMaxCpForPlayer(int attack, int defense, int stamina, int playerLevel)
        {
            float maxLevel = Math.Min(playerLevel + 1.5f, 40f);
            double maxCpMultplier = LEVEL_CP_MULTIPLIER[maxLevel];
            return GetCp(attack, defense, stamina, maxCpMultplier);
        }

        public static int GetCp(int attack, int defense, int stamina, double combinedCpMultiplier)
        {
            return (int)Math.Round(attack * Math.Pow(defense, 0.5) * Math.Pow(stamina, 0.5)
                    * Math.Pow(combinedCpMultiplier, 2) / 10f);
        }

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

        public static double GetAdditionalCpMultiplierAfterPowerup(double cpMultiplier, double additionalCpMultiplier)
        {
            float nextLevel = GetLevelFromCpMultiplier(cpMultiplier + additionalCpMultiplier) + .5f;
            return LEVEL_CP_MULTIPLIER[nextLevel] - cpMultiplier;
        }

        public static int GetStardustCostsForPowerup(double combinedCpMultiplier)
        {
            int level = (int)GetLevelFromCpMultiplier(combinedCpMultiplier);
            return PokemonMeta.UpgradeSettings.StardustCost[level];
        }

        public static int GetCandyCostsForPowerup(double combinedCpMultiplier)
        {
            int level = (int)GetLevelFromCpMultiplier(combinedCpMultiplier);
            return PokemonMeta.UpgradeSettings.CandyCost[level];
        }
    }
}
