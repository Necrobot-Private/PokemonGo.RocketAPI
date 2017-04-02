namespace PokemonGo.RocketAPI
{
    public interface ICaptchaResolver
    {
        void OnCaptcha(string captchaUrl);
    }
}
