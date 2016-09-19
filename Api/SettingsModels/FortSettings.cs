using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class FortSettings
    {

        /**
         * Min distance to interact with the fort
         *
         * @return distance in meters.
         */
        public double InteractionRangeInMeters;

        /**
         * NOT SURE: max number of pokemons in the fort
         *
         * @return number of pokemons.
         */
        public int MaxTotalDeployedPokemon;

        /**
         * NOT SURE: max number of players who can add pokemons to the fort
         *
         * @return number of players.
         */
        public int MaxPlayerDeployedPokemon;

        /**
         * Stamina multiplier
         *
         * @return multiplier.
         */
        public double DeployStaminaMultiplier;

        /**
         * Attack multiplier
         *
         * @return multiplier.
         */
        public double DeployAttackMultiplier;

        /**
         * NO IDEA
         *
         * @return distance in meters.
         */
        public double FarInteractionRangeMeters;

        /**
         * Update the fort settings from the network response.
         *
         * @param fortSettings the new fort settings
         */
        public void Update(POGOProtos.Settings.FortSettings fortSettings)
        {
            InteractionRangeInMeters = fortSettings.InteractionRangeMeters;
            MaxTotalDeployedPokemon = fortSettings.MaxTotalDeployedPokemon;
            MaxPlayerDeployedPokemon = fortSettings.MaxPlayerDeployedPokemon;
            DeployStaminaMultiplier = fortSettings.DeployStaminaMultiplier;
            DeployAttackMultiplier = fortSettings.DeployAttackMultiplier;
            FarInteractionRangeMeters = fortSettings.FarInteractionRangeMeters;
        }
    }
}
