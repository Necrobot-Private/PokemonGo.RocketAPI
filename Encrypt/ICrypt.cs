namespace PokemonGo.RocketAPI.Encrypt
{
    public interface ICrypt
    {
        byte[] Encrypt(byte[] input, uint ms);
    }
}
