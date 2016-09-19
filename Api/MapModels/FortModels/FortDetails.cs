using POGOProtos.Enums;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Responses;
using System;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.MapModels.FortModels
{
    public class FortDetails
    {
        private FortDetailsResponse proto;

        public FortDetails(FortDetailsResponse proto)
        {
            this.proto = proto;
        }

        public String GetId()
        {
            return proto.FortId;
        }

        public TeamColor GetTeam()
        {
            return proto.TeamColor;
        }

        public String GetName()
        {
            return proto.Name;
        }

        public IList<string> GetImageUrl()
        {
            return proto.ImageUrls;
        }

        public int GetFp()
        {
            return proto.Fp;
        }

        public int GetStamina()
        {
            return proto.Stamina;
        }

        public int GetMaxStamina()
        {
            return proto.MaxStamina;
        }

        public FortType GetFortType()
        {
            return proto.Type;
        }

        public double GetLatitude()
        {
            return proto.Latitude;
        }

        public double GetLongitude()
        {
            return proto.Longitude;
        }

        public String GetDescription()
        {
            return proto.Description;
        }

        public IList<FortModifier> GetModifier()
        {
            return proto.Modifiers;
        }
    }

}
