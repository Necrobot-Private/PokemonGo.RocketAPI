using POGOProtos.Enums;
using System;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class PokemonMeta
    {
        public String TemplateId { get; set; }
        public PokemonFamilyId Family;
        public PokemonClass PokemonClass;
        public PokemonType Type2;
        public double PokedexHeightM;
        public double HeightStdDev;
        public int BaseStamina;
        public double CylRadiusM;
        public double BaseFleeRate;
        public int BaseAttack;
        public double DiskRadiusM;
        public double CollisionRadiusM;
        public double PokedexWeightKg;
        public MovementType MovementType;
        public PokemonType Type1;
        public double CollisionHeadRadiusM;
        public double MovementTimerS;
        public double JumpTimeS;
        public double ModelScale;
        public String UniqueId;
        public int BaseDefense;
        public int AttackTimerS;
        public double WeightStdDev;
        public double CylHeightM;
        public int CandyToEvolve;
        public double CollisionHeightM;
        public double ShoulderModeScale;
        public double BaseCaptureRate;
        public PokemonId ParentId;
        public double CylGroundM;
        public PokemonMove[] QuickMoves;
        public PokemonMove[] CinematicMoves;
        public int Number;
    }

}
