using POGOProtos.Data.Player;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class Stats
    {
        private PlayerStats proto;

        public Stats(PlayerStats proto)
        {
            this.proto = proto;
        }

        public int GetLevel()
        {
            return proto.Level;
        }

        public long GetExperience()
        {
            return proto.Experience;
        }

        public long GetPrevLevelXp()
        {
            return proto.PrevLevelXp;
        }

        public long GetNextLevelXp()
        {
            return proto.NextLevelXp;
        }

        public float GetKmWalked()
        {
            return proto.KmWalked;
        }

        public int GetPokemonsEncountered()
        {
            return proto.PokemonsEncountered;
        }

        public int GetUniquePokedexEntries()
        {
            return proto.UniquePokedexEntries;
        }

        public int GetPokemonsCaptured()
        {
            return proto.PokemonsCaptured;
        }

        public int GetEvolutions()
        {
            return proto.Evolutions;
        }

        public int GetPokeStopVisits()
        {
            return proto.PokeStopVisits;
        }

        public int GetPokeballsThrown()
        {
            return proto.PokeballsThrown;
        }

        public int GetEggsHatched()
        {
            return proto.EggsHatched;
        }

        public int GetBigMagikarpCaught()
        {
            return proto.BigMagikarpCaught;
        }

        public int GetBattleAttackWon()
        {
            return proto.BattleAttackWon;
        }

        public int GetBattleAttackTotal()
        {
            return proto.BattleAttackTotal;
        }

        public int GetBattleDefendedWon()
        {
            return proto.BattleDefendedWon;
        }

        public int GetBattleTrainingWon()
        {
            return proto.BattleTrainingWon;
        }

        public int GetBattleTrainingTotal()
        {
            return proto.BattleTrainingTotal;
        }

        public int GetPrestigeRaisedTotal()
        {
            return proto.PrestigeRaisedTotal;
        }

        public int GetPrestigeDroppedTotal()
        {
            return proto.PrestigeDroppedTotal;
        }

        public int GetPokemonDeployed()
        {
            return proto.PokemonDeployed;
        }

        public int GetSmallRattataCaught()
        {
            return proto.SmallRattataCaught;
        }
    }
}