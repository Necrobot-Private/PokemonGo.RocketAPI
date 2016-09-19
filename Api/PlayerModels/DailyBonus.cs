namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class DailyBonus
    {
        private POGOProtos.Data.Player.DailyBonus proto;

        public DailyBonus(POGOProtos.Data.Player.DailyBonus proto)
        {
            this.proto = proto;
        }

        public long GetNextCollectedTimestampMs()
        {
            return proto.NextCollectedTimestampMs;
        }

        public long GetNextDefenderBonusCollectTimestampMs()
        {
            return proto.NextDefenderBonusCollectTimestampMs;
        }
    }
}
