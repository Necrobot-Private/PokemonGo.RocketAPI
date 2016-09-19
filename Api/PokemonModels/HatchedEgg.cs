namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class HatchedEgg
    {
        public ulong Id;
        public int Experience;
        public int Candy;
        public int Stardust;

        public HatchedEgg(ulong id, int experience, int candy, int stardust)
        {
            this.Id = id;
            this.Experience = experience;
            this.Candy = candy;
            this.Stardust = stardust;
        }
    }

}
