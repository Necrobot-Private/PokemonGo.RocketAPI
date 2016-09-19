using POGOProtos.Networking.Responses;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class EvolutionResult
    {
        private EvolvePokemonResponse proto;
        private Pokemon pokemon;

        /**
         * The evolution result.
         *
         * @param api   PokemonGo api
         * @param proto Pokemon proto
         */
        public EvolutionResult(Client client, EvolvePokemonResponse proto)
        {
            this.proto = proto;
            this.pokemon = new Pokemon(client, proto.EvolvedPokemonData);
        }

        public EvolvePokemonResponse.Types.Result GetResult()
        {
            return proto.Result;
        }

        public Pokemon GetEvolvedPokemon()
        {
            return pokemon;
        }

        public int GetExpAwarded()
        {
            return proto.ExperienceAwarded;
        }

        public int GetCandyAwarded()
        {
            return proto.CandyAwarded;
        }

        public bool IsSuccessful()
        {
            return GetResult() == EvolvePokemonResponse.Types.Result.Success;
        }
    }

}
