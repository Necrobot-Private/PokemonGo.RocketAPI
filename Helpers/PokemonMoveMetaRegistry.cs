using System.Collections.Generic;
using System.Linq;
using POGOProtos.Enums;


namespace PokemonGo.RocketAPI.Helpers
{
    public class PokemonMoveMetaRegistry
    {
        private static readonly Dictionary<PokemonMove, PokemonMoveMeta> Meta = new Dictionary<PokemonMove, PokemonMoveMeta>();
        static PokemonMoveMetaRegistry()
        {
            PokemonMoveMeta metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BodySlam);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1560);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.BodySlam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.CrossChop);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(60);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(2000);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.CrossChop, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DragonClaw);
            metam.SetType(PokemonType.Dragon);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(1500);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.DragonClaw, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PsychoCutFast);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(7);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(570);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.PsychoCutFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MudShotFast);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(550);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.MudShotFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PowerWhip);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(70);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(2800);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.PowerWhip, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AquaTail);
            metam.SetType(PokemonType.Water);
            metam.SetPower(45);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2350);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.AquaTail, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.IronHead);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2000);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.IronHead, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.GunkShot);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(65);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3000);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.GunkShot, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.LickFast);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(5);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.LickFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ScratchFast);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.ScratchFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WaterGunFast);
            metam.SetType(PokemonType.Water);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.WaterGunFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WaterGunFastBlastoise);
            metam.SetType(PokemonType.Water);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.WaterGunFastBlastoise, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SludgeBomb);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2600);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.SludgeBomb, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MetalClawFast);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(8);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(630);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.MetalClawFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Hurricane);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(80);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3200);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Hurricane, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BrickBreak);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(1600);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.BrickBreak, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Thunderbolt);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2700);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.Thunderbolt, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Psychic);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2800);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.Psychic, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.StoneEdge);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(80);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.5);
            metam.SetTime(3100);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.StoneEdge, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SludgeWave);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(70);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3400);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.SludgeWave, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Flamethrower);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2900);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.Flamethrower, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PlayRough);
            metam.SetType(PokemonType.Fairy);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2900);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.PlayRough, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Megahorn);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(80);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3200);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Megahorn, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ShadowClawFast);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(11);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(950);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.ShadowClawFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ThunderPunch);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2400);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.ThunderPunch, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HyperFang);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.HyperFang, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.LeafBlade);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(2800);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.LeafBlade, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Discharge);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2500);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Discharge, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WingAttackFast);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(9);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(750);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.WingAttackFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HeatWave);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(80);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3800);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.HeatWave, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HydroPump);
            metam.SetType(PokemonType.Water);
            metam.SetPower(90);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3800);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.HydroPump, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HydroPumpBlastoise);
            metam.SetType(PokemonType.Water);
            metam.SetPower(90);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3800);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.HydroPumpBlastoise, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PetalBlizzard);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(65);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3200);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.PetalBlizzard, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Blizzard);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(100);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3900);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Blizzard, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.VineWhipFast);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(7);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(650);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.VineWhipFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Thunder);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(100);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4300);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Thunder, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Psyshock);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2700);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Psyshock, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FrostBreathFast);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(9);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(810);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.FrostBreathFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PoundFast);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(7);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(540);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.PoundFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Moonblast);
            metam.SetType(PokemonType.Fairy);
            metam.SetPower(85);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4100);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Moonblast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FireBlast);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(100);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4100);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.FireBlast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Earthquake);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(100);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4200);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Earthquake, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Submission);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Submission, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.XScissor);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.XScissor, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PoisonJabFast);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1050);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.PoisonJabFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ZenHeadbuttFast);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1050);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.ZenHeadbuttFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FlashCannon);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(60);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3900);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.FlashCannon, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HyperBeam);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(120);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(5000);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.HyperBeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DragonPulse);
            metam.SetType(PokemonType.Dragon);
            metam.SetPower(65);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3600);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.DragonPulse, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PowerGem);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2900);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.PowerGem, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Psystrike);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(70);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(5100);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.Psystrike, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.IceBeam);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(65);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3650);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.IceBeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.CrossPoison);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(1500);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.CrossPoison, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BugBiteFast);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(5);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(450);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.BugBiteFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SolarBeam);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(120);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4900);
            metam.SetEnergy(-100);
            Meta.Add(PokemonMove.SolarBeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ShadowBall);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(45);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3080);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.ShadowBall, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DarkPulse);
            metam.SetType(PokemonType.Dark);
            metam.SetPower(45);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3500);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.DarkPulse, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.IcePunch);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(45);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3500);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.IcePunch, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SeedBomb);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2400);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.SeedBomb, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.RockSlide);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3200);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.RockSlide, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BoneClub);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1600);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.BoneClub, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FirePunch);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2800);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.FirePunch, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BiteFast);
            metam.SetType(PokemonType.Dark);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.BiteFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DragonBreathFast);
            metam.SetType(PokemonType.Dragon);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(500);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.DragonBreathFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FlameBurst);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.FlameBurst, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Stomp);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Stomp, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DrillRun);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(50);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(3400);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.DrillRun, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BugBuzz);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(75);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4250);
            metam.SetEnergy(-50);
            Meta.Add(PokemonMove.BugBuzz, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FeintAttackFast);
            metam.SetType(PokemonType.Dark);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1040);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.FeintAttackFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SignalBeam);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3100);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.SignalBeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Rest);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3100);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Rest, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SteelWingFast);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1330);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.SteelWingFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DrillPeck);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2700);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.DrillPeck, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.LowSweep);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2250);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.LowSweep, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.TackleFast);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1100);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.TackleFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DazzlingGleam);
            metam.SetType(PokemonType.Fairy);
            metam.SetPower(55);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4200);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.DazzlingGleam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.CutFast);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1130);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.CutFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PoisonStingFast);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(575);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.PoisonStingFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.RazorLeafFast);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1450);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.RazorLeafFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SuckerPunchFast);
            metam.SetType(PokemonType.Dark);
            metam.SetPower(7);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(700);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.SuckerPunchFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SparkFast);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(7);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(700);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.SparkFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.GigaDrain);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3600);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.GigaDrain, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Sludge);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2600);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Sludge, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MudBomb);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2600);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.MudBomb, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ShadowPunch);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.ShadowPunch, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.EmberFast);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1050);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.EmberFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AcidFast);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1050);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.AcidFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.NightSlash);
            metam.SetType(PokemonType.Dark);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(2700);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.NightSlash, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Psybeam);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3800);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Psybeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WaterPulse);
            metam.SetType(PokemonType.Water);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3300);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.WaterPulse, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HornAttack);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2200);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.HornAttack, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MagnetBomb);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2800);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.MagnetBomb, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Struggle);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1695);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.Struggle, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Bulldoze);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3400);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Bulldoze, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.RockThrowFast);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(12);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1360);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.RockThrowFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Scald);
            metam.SetType(PokemonType.Water);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4000);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Scald, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ScaldBlastoise);
            metam.SetType(PokemonType.Water);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4000);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.ScaldBlastoise, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PeckFast);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1150);
            metam.SetEnergy(10);
            Meta.Add(PokemonMove.PeckFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AerialAce);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2900);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.AerialAce, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BubbleBeam);
            metam.SetType(PokemonType.Water);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2900);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.BubbleBeam, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AncientPower);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(35);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3600);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.AncientPower, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Brine);
            metam.SetType(PokemonType.Water);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2400);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Brine, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Swift);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3000);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.Swift, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ThunderShockFast);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(5);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(600);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.ThunderShockFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.LowKickFast);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(5);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(600);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.LowKickFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BulletPunchFast);
            metam.SetType(PokemonType.Steel);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1200);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.BulletPunchFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FireFangFast);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(840);
            metam.SetEnergy(4);
            Meta.Add(PokemonMove.FireFangFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.SplashFast);
            metam.SetType(PokemonType.Water);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1230);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.SplashFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.OminousWind);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3100);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.OminousWind, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ConfusionFast);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1510);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.ConfusionFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.HeartStamp);
            metam.SetType(PokemonType.Psychic);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2550);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.HeartStamp, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Dig);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(70);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(5800);
            metam.SetEnergy(-33);
            Meta.Add(PokemonMove.Dig, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FlameWheel);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(40);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4600);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.FlameWheel, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AirCutter);
            metam.SetType(PokemonType.Flying);
            metam.SetPower(30);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(3300);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.AirCutter, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.QuickAttackFast);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(10);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1330);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.QuickAttackFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FuryCutterFast);
            metam.SetType(PokemonType.Bug);
            metam.SetPower(3);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(400);
            metam.SetEnergy(12);
            Meta.Add(PokemonMove.FuryCutterFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.KarateChopFast);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(6);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(800);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.KarateChopFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.RockTomb);
            metam.SetType(PokemonType.Rock);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.25);
            metam.SetTime(3400);
            metam.SetEnergy(-25);
            Meta.Add(PokemonMove.RockTomb, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.IceShardFast);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1400);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.IceShardFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ViceGrip);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.ViceGrip, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ParabolicCharge);
            metam.SetType(PokemonType.Electric);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2100);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.ParabolicCharge, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.BubbleFast);
            metam.SetType(PokemonType.Water);
            metam.SetType(PokemonType.Water);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2300);
            metam.SetEnergy(15);
            Meta.Add(PokemonMove.BubbleFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.FlameCharge);
            metam.SetType(PokemonType.Fire);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3100);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.FlameCharge, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.AquaJet);
            metam.SetType(PokemonType.Water);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2350);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.AquaJet, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.PoisonFang);
            metam.SetType(PokemonType.Poison);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2400);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.PoisonFang, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Twister);
            metam.SetType(PokemonType.Dragon);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2700);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.Twister, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DrainingKiss);
            metam.SetType(PokemonType.Fairy);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(2800);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.DrainingKiss, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.DisarmingVoice);
            metam.SetType(PokemonType.Fairy);
            metam.SetPower(20);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3900);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.DisarmingVoice, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.ShadowSneak);
            metam.SetType(PokemonType.Ghost);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3100);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.ShadowSneak, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MegaDrain);
            metam.SetType(PokemonType.Grass);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3200);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.MegaDrain, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.MudSlapFast);
            metam.SetType(PokemonType.Ground);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1350);
            metam.SetEnergy(9);
            Meta.Add(PokemonMove.MudSlapFast, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WrapGreen);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3700);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.WrapGreen, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.WrapPink);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3700);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.WrapPink, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.IcyWind);
            metam.SetType(PokemonType.Ice);
            metam.SetPower(25);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(3800);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.IcyWind, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.Wrap);
            metam.SetType(PokemonType.Normal);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(4000);
            metam.SetEnergy(-20);
            Meta.Add(PokemonMove.Wrap, metam);

            metam = new PokemonMoveMeta();
            metam.SetMove(PokemonMove.RockSmashFast);
            metam.SetType(PokemonType.Fighting);
            metam.SetPower(15);
            metam.SetAccuracy(1);
            metam.SetCritChance(0.05);
            metam.SetTime(1410);
            metam.SetEnergy(7);
            Meta.Add(PokemonMove.RockSmashFast, metam);
        }

        public static PokemonMoveMeta GetMeta(PokemonMove id)
        {
            return Meta.Values.FirstOrDefault(x => x.Move == id);
        }
    }
}