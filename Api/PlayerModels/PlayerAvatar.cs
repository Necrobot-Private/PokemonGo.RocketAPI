using POGOProtos.Enums;

namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class PlayerAvatar
    {
        private POGOProtos.Data.Player.PlayerAvatar avatar;

        public PlayerAvatar(POGOProtos.Data.Player.PlayerAvatar data)
        {
            avatar = data;
        }

        public int GetSkin()
        {
            return avatar.Skin;
        }

        public int GetHair()
        {
            return avatar.Hair;
        }

        public int GetShirt()
        {
            return avatar.Shirt;
        }

        public int GetPants()
        {
            return avatar.Pants;
        }

        public int GetHat()
        {
            return avatar.Hat;
        }

        public int GetShoes()
        {
            return avatar.Shoes;
        }
        
        public Gender GetGender()
        {
            return avatar.Gender;
        }

        public int GetEyes()
        {
            return avatar.Eyes;
        }

        public int GetBackpack()
        {
            return avatar.Backpack;
        }

        public static int GetAvailableSkins()
        {
            return 4;
        }

        public static int GetAvailableHair()
        {
            return 6;
        }

        public static int GetAvailableEyes()
        {
            return 5;
        }

        public static int GetAvailableHats()
        {
            return 5;
        }

        public static int GetAvailableShirts(Gender gender)
        {
            return gender == Gender.Male ? 4 : 9;
        }

        public static int GetAvailablePants(Gender gender)
        {
            return gender == Gender.Male ? 3 : 6;
        }

        public static int GetAvailableShoes()
        {
            return 7;
        }

        public static int GetAvailableBags(Gender gender)
        {
            return gender == Gender.Male ? 6 : 3;
        }
    }
}
