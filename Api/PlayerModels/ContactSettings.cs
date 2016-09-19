namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class ContactSettings
    {
        private POGOProtos.Data.Player.ContactSettings proto;

        public ContactSettings(POGOProtos.Data.Player.ContactSettings proto)
        {
            this.proto = proto;
        }

        public bool GetSendMarketingEmails()
        {
            return proto.SendMarketingEmails;
        }

        public bool GetSendPushNotifications()
        {
            return proto.SendPushNotifications;
        }
    }
}
