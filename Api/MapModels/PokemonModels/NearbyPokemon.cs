using POGOProtos.Enums;
using System;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels
{
    public class NearbyPokemon
    {
        private POGOProtos.Map.Pokemon.NearbyPokemon proto;

        public NearbyPokemon(POGOProtos.Map.Pokemon.NearbyPokemon proto)
        {
            this.proto = proto;
        }

        public PokemonId GetPokemonId()
        {
            return proto.PokemonId;
        }

        public float GetDistanceInMeters()
        {
            return proto.DistanceInMeters;
        }

        public ulong GetEncounterId()
        {
            return proto.EncounterId;
        }

        public String GetFortId()
        {
            return proto.FortId;
        }

        public String GetFortImageUrl()
        {
            return proto.FortImageUrl;
        }
    }

}
