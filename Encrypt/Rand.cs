namespace PokemonGo.RocketAPI.Encrypt
{
    /// <summary>
    /// Description of Rand.
    /// </summary>
    public class Rand
    {
        private long state;

        public Rand(long state)
        {
            this.state = state;
        }

        public byte Next()
        {
            state = (state * 0x41C64E6D) + 0x3039;
            return (byte)((state >> 16) & 0xFF);
        }
    }

}