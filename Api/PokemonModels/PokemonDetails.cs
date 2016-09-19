using POGOProtos.Data;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class PokemonDetails : BaseRpc
    {
        protected PokemonData proto;
        protected PokemonMeta meta;

        public PokemonDetails(Client client, PokemonData proto) : base(client)
        {
            this.proto = proto;
        }

        public int GetCandy()
        {
            return Client.Inventories.CandyJar.GetCandies(GetPokemonFamily());
        }

        public PokemonFamilyId GetPokemonFamily()
        {
            return GetMeta().Family;
        }

        public PokemonData GetDefaultInstanceForType()
        {
            return new PokemonData();
        }

        public ulong GetId()
        {
            return proto.Id;
        }

        public PokemonId GetPokemonId()
        {
            return proto.PokemonId;
        }

        public int GetCp()
        {
            return proto.Cp;
        }

        public int GetMaxStamina()
        {
            return proto.StaminaMax;
        }

        public PokemonMove GetMove1()
        {
            return proto.Move1;
        }

        public PokemonMove GetMove2()
        {
            return proto.Move2;
        }

        public String GetDeployedFortId()
        {
            return proto.DeployedFortId;
        }

        public String GetOwnerName()
        {
            return proto.OwnerName;
        }

        public bool GetIsEgg()
        {
            return proto.IsEgg;
        }

        public double GetEggKmWalkedTarget()
        {
            return proto.EggKmWalkedTarget;
        }

        public double GetEggKmWalkedStart()
        {
            return proto.EggKmWalkedStart;
        }

        public int GetOrigin()
        {
            return proto.Origin;
        }

        public float GetHeightM()
        {
            return proto.HeightM;
        }

        public float WeightKg()
        {
            return proto.WeightKg;
        }

        public int GetIndividualAttack()
        {
            return proto.IndividualAttack;
        }

        public int GetIndividualDefense()
        {
            return proto.IndividualDefense;
        }

        public int GetIndividualStamina()
        {
            return proto.IndividualStamina;
        }

        /**
         * Calculates the pokemons IV ratio.
         *
         * @return the pokemons IV ratio as a double between 0 and 1.0, 1.0 being perfect IVs
         */
        public double GetIvRatio()
        {
            return (this.GetIndividualAttack() + this.GetIndividualDefense() + this.GetIndividualStamina()) / 45.0;
        }

        public float GetCpMultiplier()
        {
            return proto.CpMultiplier;
        }

        public float GetAdditionalCpMultiplier()
        {
            return proto.AdditionalCpMultiplier;
        }

        public float GetCombinedCpMultiplier()
        {
            return GetCpMultiplier() + GetAdditionalCpMultiplier();
        }

        public ItemId GetPokeball()
        {
            return proto.Pokeball;
        }

        public ulong GetCapturedS2CellId()
        {
            return proto.CapturedCellId;
        }

        public int GetBattlesAttacked()
        {
            return proto.BattlesAttacked;
        }

        public int GetBattlesDefended()
        {
            return proto.BattlesDefended;
        }

        public string GetEggIncubatorId()
        {
            return proto.EggIncubatorId;
        }

        public ulong GetCreationTimeMs()
        {
            return proto.CreationTimeMs;
        }

        /**
         * Checks whether the Pokémon is set as favorite.
         *
         * @return true if the Pokémon is set as favorite
         */
        public bool IsFavorite()
        {
            return proto.Favorite > 0;
        }

        // Deprecated
        /*
        public bool GetFavorite()
        {
            return proto.Favorite > 0;
        }
        */

        public string GetNickname()
        {
            return proto.Nickname;
        }

        public bool GetFromFort()
        {
            return proto.FromFort > 0;
        }
        
        public int GetBaseStam()
        {
            return GetMeta().BaseStamina;
        }

        public double GetBaseCaptureRate()
        {
            return GetMeta().BaseCaptureRate;
        }

        public int GetCandiesToEvolve()
        {
            return GetMeta().CandyToEvolve;
        }

        public double GetBaseFleeRate()
        {
            return GetMeta().BaseFleeRate;
        }

        public double GetLevel()
        {
            return PokemonCpUtils.GetLevelFromCpMultiplier(GetCombinedCpMultiplier());
        }

        /**
         * Get the meta info for a pokemon.
         *
         * @return PokemonMeta
         */
        public PokemonMeta GetMeta()
        {
            if (meta == null)
            {
                meta = PokemonMetaRegistry.GetMeta(this.GetPokemonId());
            }

            return meta;
        }

        /**
         * Calculate the maximum CP for this individual pokemon when the player is at level 40
         *
         * @return The maximum CP for this pokemon
         * @throws NoSuchItemException If the PokemonId value cannot be found in the {@link PokemonMetaRegistry}.
         */
        public int GetMaxCp()
        {
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(proto.PokemonId);
		    if (pokemonMeta == null) {
                throw new Exception("Cannot find meta data for " + proto.PokemonId);
            }

            int attack = GetIndividualAttack() + pokemonMeta.BaseAttack;
            int defense = GetIndividualDefense() + pokemonMeta.BaseDefense;
            int stamina = GetIndividualStamina() + pokemonMeta.BaseStamina;
		    return PokemonCpUtils.GetMaxCp(attack, defense, stamina);
	    }

        /**
	     * Calculate the maximum CP for this individual pokemon and this player's level
	     *
	     * @return The maximum CP for this pokemon
	     * @throws NoSuchItemException   If the PokemonId value cannot be found in the {@link PokemonMetaRegistry}.
	     */
        public int GetMaxCpForPlayer()
        {
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(proto.PokemonId);
		    if (pokemonMeta == null) {
                throw new Exception("Cannot find meta data for " + proto.PokemonId);
            }
		    int attack = GetIndividualAttack() + pokemonMeta.BaseAttack;
		    int defense = GetIndividualDefense() + pokemonMeta.BaseDefense;
		    int stamina = GetIndividualStamina() + pokemonMeta.BaseStamina;
		    int playerLevel = Client.PlayerProfile.Stats.GetLevel();
		    return PokemonCpUtils.GetMaxCpForPlayer(attack, defense, stamina, playerLevel);
        }

        /**
	     * Calculates the absolute maximum CP for all pokemons with this PokemonId
	     *
	     * @return The absolute maximum CP
	     * @throws NoSuchItemException If the PokemonId value cannot be found in the {@link PokemonMetaRegistry}.
	     */
        public int GetAbsoluteMaxCp()
        {
		    return PokemonCpUtils.GetAbsoluteMaxCp(GetPokemonId());
        }

        /**
	     * Calculated the max cp of this pokemon, if you upgrade it fully and the player is at level 40
	     *
	     * @return Max cp of this pokemon
	     */
        public int GetCpFullEvolveAndPowerup()
        {
            return GetMaxCpFullEvolveAndPowerup(40);
        }

        /**
	     * Calculated the max cp of this pokemon, if you upgrade it fully with your current player level
	     *
	     * @return Max cp of this pokemon
	     */
        public int GetMaxCpFullEvolveAndPowerupForPlayer()
        {
            return GetMaxCpFullEvolveAndPowerup(Client.PlayerProfile.Stats.GetLevel());
        }

        /**
	     * Calculated the max cp of this pokemon, if you upgrade it fully with your current player level
	     *
	     * @return Max cp of this pokemon
	     */
        private int GetMaxCpFullEvolveAndPowerup(int playerLevel)
        {
            PokemonId highestUpgradedFamily;
            if (new List<PokemonId>(new PokemonId[] {PokemonId.Vaporeon, PokemonId.Jolteon, PokemonId.Flareon}).Contains(GetPokemonId()))
            {
                highestUpgradedFamily = GetPokemonId();
            }
            else if (GetPokemonId() == PokemonId.Eevee)
            {
                highestUpgradedFamily = PokemonId.Flareon;
            }
            else
            {
                highestUpgradedFamily = PokemonMetaRegistry.GetHighestForFamily(GetPokemonFamily());
            }
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(highestUpgradedFamily);
            int attack = GetIndividualAttack() + pokemonMeta.BaseAttack;
            int defense = GetIndividualDefense() + pokemonMeta.BaseDefense;
            int stamina = GetIndividualStamina() + pokemonMeta.BaseStamina;
            return PokemonCpUtils.GetMaxCpForPlayer(attack, defense, stamina, playerLevel);
        }

        /**
	     * Calculate the CP after evolving this Pokemon
	     *
	     * @return New CP after evolve
	     */
        public int GetCpAfterEvolve()
        {
            if (new List<PokemonId>(new PokemonId[] { PokemonId.Vaporeon, PokemonId.Jolteon, PokemonId.Flareon }).Contains(GetPokemonId()))
            {
                return GetCp();
            }
            PokemonId highestUpgradedFamily = PokemonMetaRegistry.GetHighestForFamily(GetPokemonFamily());
            if (GetPokemonId() == highestUpgradedFamily)
            {
                return GetCp();
            }
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(highestUpgradedFamily);
            PokemonId secondHighest = pokemonMeta.ParentId;
            if (GetPokemonId() == secondHighest)
            {
                int attack1 = GetIndividualAttack() + pokemonMeta.BaseAttack;
                int defense1 = GetIndividualDefense() + pokemonMeta.BaseDefense;
                int stamina1 = GetIndividualStamina() + pokemonMeta.BaseStamina;
                return PokemonCpUtils.GetCp(attack1, defense1, stamina1, GetCombinedCpMultiplier());
            }
            pokemonMeta = PokemonMetaRegistry.GetMeta(secondHighest);
            int attack = GetIndividualAttack() + pokemonMeta.BaseAttack;
            int defense = GetIndividualDefense() + pokemonMeta.BaseDefense;
            int stamina = GetIndividualStamina() + pokemonMeta.BaseStamina;
            return PokemonCpUtils.GetCp(attack, defense, stamina, GetCombinedCpMultiplier());
        }

        /**
	     * Calculate the CP after fully evolving this Pokemon
	     *
	     * @return New CP after evolve
	     */
        public int GetCpAfterFullEvolve()
        {
            if (new List<PokemonId>(new PokemonId[] { PokemonId.Vaporeon, PokemonId.Jolteon, PokemonId.Flareon }).Contains(GetPokemonId()))
            {
                return GetCp();
            }
            PokemonId highestUpgradedFamily = PokemonMetaRegistry.GetHighestForFamily(GetPokemonFamily());
            if (GetPokemonId() == highestUpgradedFamily)
            {
                return GetCp();
            }
            PokemonMeta pokemonMeta = PokemonMetaRegistry.GetMeta(highestUpgradedFamily);
            int attack = this.proto.IndividualAttack + pokemonMeta.BaseAttack;
            int defense = this.proto.IndividualDefense + pokemonMeta.BaseDefense;
            int stamina = this.proto.IndividualStamina + pokemonMeta.BaseStamina;
            return PokemonCpUtils.GetCp(attack, defense, stamina, GetCombinedCpMultiplier());
        }

        /**
	     * @return The number of powerups already done
	     */
        public int GetNumerOfPowerupsDone()
        {
            return this.proto.NumUpgrades;
        }

        /**
	     * @return The CP for this pokemon after powerup
	     */
        public int GetCpAfterPowerup()
        {
            return PokemonCpUtils.GetCpAfterPowerup(GetCp(), GetCombinedCpMultiplier());
        }

        /**
	     * @return Cost of candy for a powerup
	     */
        public int GetCandyCostsForPowerup()
        {
            return PokemonCpUtils.GetCandyCostsForPowerup(GetCombinedCpMultiplier(), GetNumerOfPowerupsDone());
        }

        /**
	     * @return Cost of stardust for a powerup
	     */
        public int GetStardustCostsForPowerup()
        {
            return PokemonCpUtils.GetStartdustCostsForPowerup(GetCombinedCpMultiplier(), GetNumerOfPowerupsDone());
        }
    }

}
