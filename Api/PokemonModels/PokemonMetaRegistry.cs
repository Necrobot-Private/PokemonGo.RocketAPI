using POGOProtos.Enums;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    public class PokemonMetaRegistry
    {
        public static Dictionary<PokemonFamilyId, PokemonId> HighestForFamily = new Dictionary<PokemonFamilyId, PokemonId>();

        public static Dictionary<PokemonId, PokemonMeta> Meta = new Dictionary<PokemonId, PokemonMeta>();

        static PokemonMetaRegistry()
        {
            HighestForFamily[PokemonFamilyId.FamilyBulbasaur] = PokemonId.Venusaur;
            HighestForFamily[PokemonFamilyId.FamilyCharmander] = PokemonId.Charizard;
            HighestForFamily[PokemonFamilyId.FamilySquirtle] = PokemonId.Blastoise;
            HighestForFamily[PokemonFamilyId.FamilyCaterpie] = PokemonId.Butterfree;
            HighestForFamily[PokemonFamilyId.FamilyWeedle] = PokemonId.Beedrill;
            HighestForFamily[PokemonFamilyId.FamilyPidgey] = PokemonId.Pidgeot;
            HighestForFamily[PokemonFamilyId.FamilyRattata] = PokemonId.Raticate;
            HighestForFamily[PokemonFamilyId.FamilySpearow] = PokemonId.Fearow;
            HighestForFamily[PokemonFamilyId.FamilyEkans] = PokemonId.Arbok;
            HighestForFamily[PokemonFamilyId.FamilyPikachu] = PokemonId.Raichu;
            HighestForFamily[PokemonFamilyId.FamilySandshrew] = PokemonId.Sandslash;
            HighestForFamily[PokemonFamilyId.FamilyNidoranFemale] = PokemonId.Nidoqueen;
            HighestForFamily[PokemonFamilyId.FamilyNidoranMale] = PokemonId.Nidoking;
            HighestForFamily[PokemonFamilyId.FamilyClefairy] = PokemonId.Clefable;
            HighestForFamily[PokemonFamilyId.FamilyVulpix] = PokemonId.Ninetales;
            HighestForFamily[PokemonFamilyId.FamilyJigglypuff] = PokemonId.Wigglytuff;
            HighestForFamily[PokemonFamilyId.FamilyZubat] = PokemonId.Golbat;
            HighestForFamily[PokemonFamilyId.FamilyOddish] = PokemonId.Vileplume;
            HighestForFamily[PokemonFamilyId.FamilyParas] = PokemonId.Parasect;
            HighestForFamily[PokemonFamilyId.FamilyVenonat] = PokemonId.Venomoth;
            HighestForFamily[PokemonFamilyId.FamilyDiglett] = PokemonId.Dugtrio;
            HighestForFamily[PokemonFamilyId.FamilyMeowth] = PokemonId.Persian;
            HighestForFamily[PokemonFamilyId.FamilyPsyduck] = PokemonId.Golduck;
            HighestForFamily[PokemonFamilyId.FamilyMankey] = PokemonId.Primeape;
            HighestForFamily[PokemonFamilyId.FamilyGrowlithe] = PokemonId.Arcanine;
            HighestForFamily[PokemonFamilyId.FamilyPoliwag] = PokemonId.Poliwrath;
            HighestForFamily[PokemonFamilyId.FamilyAbra] = PokemonId.Alakazam;
            HighestForFamily[PokemonFamilyId.FamilyMachop] = PokemonId.Machamp;
            HighestForFamily[PokemonFamilyId.FamilyBellsprout] = PokemonId.Victreebel;
            HighestForFamily[PokemonFamilyId.FamilyTentacool] = PokemonId.Tentacruel;
            HighestForFamily[PokemonFamilyId.FamilyGeodude] = PokemonId.Golem;
            HighestForFamily[PokemonFamilyId.FamilyPonyta] = PokemonId.Rapidash;
            HighestForFamily[PokemonFamilyId.FamilySlowpoke] = PokemonId.Slowbro;
            HighestForFamily[PokemonFamilyId.FamilyMagnemite] = PokemonId.Magneton;
            HighestForFamily[PokemonFamilyId.FamilyFarfetchd] = PokemonId.Farfetchd;
            HighestForFamily[PokemonFamilyId.FamilyDoduo] = PokemonId.Dodrio;
            HighestForFamily[PokemonFamilyId.FamilySeel] = PokemonId.Dewgong;
            HighestForFamily[PokemonFamilyId.FamilyGrimer] = PokemonId.Muk;
            HighestForFamily[PokemonFamilyId.FamilyShellder] = PokemonId.Cloyster;
            HighestForFamily[PokemonFamilyId.FamilyGastly] = PokemonId.Gengar;
            HighestForFamily[PokemonFamilyId.FamilyOnix] = PokemonId.Onix;
            HighestForFamily[PokemonFamilyId.FamilyDrowzee] = PokemonId.Hypno;
            HighestForFamily[PokemonFamilyId.FamilyKrabby] = PokemonId.Kingler;
            HighestForFamily[PokemonFamilyId.FamilyVoltorb] = PokemonId.Electrode;
            HighestForFamily[PokemonFamilyId.FamilyExeggcute] = PokemonId.Exeggutor;
            HighestForFamily[PokemonFamilyId.FamilyCubone] = PokemonId.Marowak;
            HighestForFamily[PokemonFamilyId.FamilyHitmonlee] = PokemonId.Hitmonlee;
            HighestForFamily[PokemonFamilyId.FamilyHitmonchan] = PokemonId.Hitmonchan;
            HighestForFamily[PokemonFamilyId.FamilyLickitung] = PokemonId.Lickitung;
            HighestForFamily[PokemonFamilyId.FamilyKoffing] = PokemonId.Weezing;
            HighestForFamily[PokemonFamilyId.FamilyRhyhorn] = PokemonId.Rhydon;
            HighestForFamily[PokemonFamilyId.FamilyChansey] = PokemonId.Chansey;
            HighestForFamily[PokemonFamilyId.FamilyTangela] = PokemonId.Tangela;
            HighestForFamily[PokemonFamilyId.FamilyKangaskhan] = PokemonId.Kangaskhan;
            HighestForFamily[PokemonFamilyId.FamilyHorsea] = PokemonId.Seadra;
            HighestForFamily[PokemonFamilyId.FamilyGoldeen] = PokemonId.Seaking;
            HighestForFamily[PokemonFamilyId.FamilyStaryu] = PokemonId.Starmie;
            HighestForFamily[PokemonFamilyId.FamilyMrMime] = PokemonId.MrMime;
            HighestForFamily[PokemonFamilyId.FamilyScyther] = PokemonId.Scyther;
            HighestForFamily[PokemonFamilyId.FamilyJynx] = PokemonId.Jynx;
            HighestForFamily[PokemonFamilyId.FamilyElectabuzz] = PokemonId.Electabuzz;
            HighestForFamily[PokemonFamilyId.FamilyMagmar] = PokemonId.Magmar;
            HighestForFamily[PokemonFamilyId.FamilyPinsir] = PokemonId.Pinsir;
            HighestForFamily[PokemonFamilyId.FamilyTauros] = PokemonId.Tauros;
            HighestForFamily[PokemonFamilyId.FamilyMagikarp] = PokemonId.Gyarados;
            HighestForFamily[PokemonFamilyId.FamilyLapras] = PokemonId.Lapras;
            HighestForFamily[PokemonFamilyId.FamilyDitto] = PokemonId.Ditto;
            HighestForFamily[PokemonFamilyId.FamilyEevee] = PokemonId.Eevee;
            HighestForFamily[PokemonFamilyId.FamilyPorygon] = PokemonId.Porygon;
            HighestForFamily[PokemonFamilyId.FamilyOmanyte] = PokemonId.Omastar;
            HighestForFamily[PokemonFamilyId.FamilyKabuto] = PokemonId.Kabutops;
            HighestForFamily[PokemonFamilyId.FamilyAerodactyl] = PokemonId.Aerodactyl;
            HighestForFamily[PokemonFamilyId.FamilySnorlax] = PokemonId.Snorlax;
            HighestForFamily[PokemonFamilyId.FamilyArticuno] = PokemonId.Articuno;
            HighestForFamily[PokemonFamilyId.FamilyZapdos] = PokemonId.Zapdos;
            HighestForFamily[PokemonFamilyId.FamilyMoltres] = PokemonId.Moltres;
            HighestForFamily[PokemonFamilyId.FamilyDratini] = PokemonId.Dragonite;
            HighestForFamily[PokemonFamilyId.FamilyMewtwo] = PokemonId.Mewtwo;
            HighestForFamily[PokemonFamilyId.FamilyMew] = PokemonId.Mew;

            PokemonMeta metap;
            metap = new PokemonMeta();
            metap.TemplateId = " V0001_POKEMON_BULBASAUR";
            metap.Family = PokemonFamilyId.FamilyBulbasaur;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.3815;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 126;
            metap.DiskRadiusM = 0.5723;
            metap.CollisionRadiusM = 0.3815;
            metap.PokedexWeightKg = 6.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.2725;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.15;
            metap.ModelScale = 1.09;
            metap.UniqueId = "V0001_POKEMON_BULBASAUR";
            metap.BaseDefense = 126;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.8625;
            metap.CylHeightM = 0.763;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.654;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.VineWhipFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.SeedBomb,
                    PokemonMove.PowerWhip
            };
            metap.Number = 1;
            Meta[PokemonId.Bulbasaur] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0002_POKEMON_IVYSAUR";
            metap.Family = PokemonFamilyId.FamilyBulbasaur;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.51;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 156;
            metap.DiskRadiusM = 0.765;
            metap.CollisionRadiusM = 0.31875;
            metap.PokedexWeightKg = 13;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.255;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1.5;
            metap.ModelScale = 0.85;
            metap.UniqueId = "V0002_POKEMON_IVYSAUR";
            metap.BaseDefense = 158;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 1.625;
            metap.CylHeightM = 1.0625;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.6375;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Bulbasaur;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.VineWhipFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.PowerWhip,
                    PokemonMove.SolarBeam
            };
            metap.Number = 2;
            Meta[PokemonId.Ivysaur] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0003_POKEMON_VENUSAUR";
            metap.Family = PokemonFamilyId.FamilyBulbasaur;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 2;
            metap.HeightStdDev = 0.25;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.759;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 198;
            metap.DiskRadiusM = 1.1385;
            metap.CollisionRadiusM = 0.759;
            metap.PokedexWeightKg = 100;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.3795;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.69;
            metap.UniqueId = "V0003_POKEMON_VENUSAUR";
            metap.BaseDefense = 200;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 12.5;
            metap.CylHeightM = 1.2075;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.035;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.04;
            metap.ParentId = PokemonId.Ivysaur;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.VineWhipFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.PetalBlizzard,
                    PokemonMove.SolarBeam
            };
            metap.Number = 3;
            Meta[PokemonId.Venusaur] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0004_POKEMON_CHARMANDER";
            metap.Family = PokemonFamilyId.FamilyCharmander;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 78;
            metap.CylRadiusM = 0.3125;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 128;
            metap.DiskRadiusM = 0.4688;
            metap.CollisionRadiusM = 0.15625;
            metap.PokedexWeightKg = 8.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.15625;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.25;
            metap.UniqueId = "V0004_POKEMON_CHARMANDER";
            metap.BaseDefense = 108;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 1.0625;
            metap.CylHeightM = 0.75;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.46875;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ScratchFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FlameCharge,
                    PokemonMove.FlameBurst,
                    PokemonMove.Flamethrower
            };
            metap.Number = 4;
            Meta[PokemonId.Charmander] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0005_POKEMON_CHARMELEON";
            metap.Family = PokemonFamilyId.FamilyCharmander;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 116;
            metap.CylRadiusM = 0.4635;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 160;
            metap.DiskRadiusM = 0.6953;
            metap.CollisionRadiusM = 0.2575;
            metap.PokedexWeightKg = 19;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.23175;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.03;
            metap.UniqueId = "V0005_POKEMON_CHARMELEON";
            metap.BaseDefense = 140;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.375;
            metap.CylHeightM = 1.133;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.7725;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Charmander;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ScratchFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FirePunch,
                    PokemonMove.FlameBurst,
                    PokemonMove.Flamethrower
            };
            metap.Number = 5;
            Meta[PokemonId.Charmeleon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0006_POKEMON_CHARIZARD";
            metap.Family = PokemonFamilyId.FamilyCharmander;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 156;
            metap.CylRadiusM = 0.81;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 212;
            metap.DiskRadiusM = 1.215;
            metap.CollisionRadiusM = 0.405;
            metap.PokedexWeightKg = 90.5;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.2025;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.81;
            metap.UniqueId = "V0006_POKEMON_CHARIZARD";
            metap.BaseDefense = 182;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 11.3125;
            metap.CylHeightM = 1.377;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.0125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.04;
            metap.ParentId = PokemonId.Charmeleon;
            metap.CylGroundM = 0.405;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WingAttackFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DragonClaw,
                    PokemonMove.Flamethrower,
                    PokemonMove.FireBlast
            };
            metap.Number = 6;
            Meta[PokemonId.Charizard] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0007_POKEMON_SQUIRTLE";
            metap.Family = PokemonFamilyId.FamilySquirtle;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 88;
            metap.CylRadiusM = 0.3825;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 112;
            metap.DiskRadiusM = 0.5738;
            metap.CollisionRadiusM = 0.2295;
            metap.PokedexWeightKg = 9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.19125;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.53;
            metap.UniqueId = "V0007_POKEMON_SQUIRTLE";
            metap.BaseDefense = 142;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1.125;
            metap.CylHeightM = 0.64259988;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.3825;
            metap.ShoulderModeScale = 0.1;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.TackleFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AquaTail,
                    PokemonMove.WaterPulse,
                    PokemonMove.AquaJet
            };
            metap.Number = 7;
            Meta[PokemonId.Squirtle] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0008_POKEMON_WARTORTLE";
            metap.Family = PokemonFamilyId.FamilySquirtle;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 118;
            metap.CylRadiusM = 0.375;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 144;
            metap.DiskRadiusM = 0.5625;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 22.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.1875;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1;
            metap.UniqueId = "V0008_POKEMON_WARTORTLE";
            metap.BaseDefense = 176;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.8125;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Squirtle;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IceBeam,
                    PokemonMove.HydroPump,
                    PokemonMove.AquaJet
            };
            metap.Number = 8;
            Meta[PokemonId.Wartortle] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0009_POKEMON_BLASTOISE";
            metap.Family = PokemonFamilyId.FamilySquirtle;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 158;
            metap.CylRadiusM = 0.564;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 186;
            metap.DiskRadiusM = 0.846;
            metap.CollisionRadiusM = 0.564;
            metap.PokedexWeightKg = 85.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.282;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.94;
            metap.UniqueId = "V0009_POKEMON_BLASTOISE";
            metap.BaseDefense = 222;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 10.6875;
            metap.CylHeightM = 1.2925;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.175;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.04;
            metap.ParentId = PokemonId.Wartortle;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IceBeam,
                    PokemonMove.FlashCannon,
                    PokemonMove.HydroPump
            };
            metap.Number = 9;
            Meta[PokemonId.Blastoise] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0010_POKEMON_CATERPIE";
            metap.Family = PokemonFamilyId.FamilyCaterpie;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.306;
            metap.BaseFleeRate = 0.2;
            metap.BaseAttack = 62;
            metap.DiskRadiusM = 0.459;
            metap.CollisionRadiusM = 0.102;
            metap.PokedexWeightKg = 2.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.153;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 0;
            metap.ModelScale = 2.04;
            metap.UniqueId = "V0010_POKEMON_CATERPIE";
            metap.BaseDefense = 66;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.3625;
            metap.CylHeightM = 0.408;
            metap.CandyToEvolve = 12;
            metap.CollisionHeightM = 0.306;
            metap.ShoulderModeScale = 0;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BugBiteFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 10;
            Meta[PokemonId.Caterpie] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0011_POKEMON_METAPOD";
            metap.Family = PokemonFamilyId.FamilyCaterpie;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.351;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 56;
            metap.DiskRadiusM = 0.5265;
            metap.CollisionRadiusM = 0.117;
            metap.PokedexWeightKg = 9.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.1755;
            metap.MovementTimerS = 3600;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.17;
            metap.UniqueId = "V0011_POKEMON_METAPOD";
            metap.BaseDefense = 86;
            metap.AttackTimerS = 3600;
            metap.WeightStdDev = 1.2375;
            metap.CylHeightM = 0.6435;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.6435;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Caterpie;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BugBiteFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 11;
            Meta[PokemonId.Metapod] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0012_POKEMON_BUTTERFREE";
            metap.Family = PokemonFamilyId.FamilyCaterpie;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.666;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 144;
            metap.DiskRadiusM = 0.999;
            metap.CollisionRadiusM = 0.1665;
            metap.PokedexWeightKg = 32;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.1776;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.11;
            metap.UniqueId = "V0012_POKEMON_BUTTERFREE";
            metap.BaseDefense = 144;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 4;
            metap.CylHeightM = 1.11;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.555;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Metapod;
            metap.CylGroundM = 0.555;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.BugBiteFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.BugBuzz,
                    PokemonMove.Psychic,
                    PokemonMove.SignalBeam
            };
            metap.Number = 12;
            Meta[PokemonId.Butterfree] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0013_POKEMON_WEEDLE";
            metap.Family = PokemonFamilyId.FamilyWeedle;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.209;
            metap.BaseFleeRate = 0.2;
            metap.BaseAttack = 68;
            metap.DiskRadiusM = 0.3135;
            metap.CollisionRadiusM = 0.1045;
            metap.PokedexWeightKg = 3.2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.15675;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 2.09;
            metap.UniqueId = "V0013_POKEMON_WEEDLE";
            metap.BaseDefense = 64;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.4;
            metap.CylHeightM = 0.418;
            metap.CandyToEvolve = 12;
            metap.CollisionHeightM = 0.209;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.BugBiteFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 13;
            Meta[PokemonId.Weedle] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0014_POKEMON_KAKUNA";
            metap.Family = PokemonFamilyId.FamilyWeedle;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.25;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 62;
            metap.DiskRadiusM = 0.375;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 10;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.125;
            metap.MovementTimerS = 3600;
            metap.JumpTimeS = 0;
            metap.ModelScale = 1.25;
            metap.UniqueId = "V0014_POKEMON_KAKUNA";
            metap.BaseDefense = 82;
            metap.AttackTimerS = 3600;
            metap.WeightStdDev = 1.25;
            metap.CylHeightM = 0.75;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.75;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Weedle;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.BugBiteFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 14;
            Meta[PokemonId.Kakuna] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0015_POKEMON_BEEDRILL";
            metap.Family = PokemonFamilyId.FamilyWeedle;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.462;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 144;
            metap.DiskRadiusM = 0.693;
            metap.CollisionRadiusM = 0.308;
            metap.PokedexWeightKg = 29.5;
            metap.MovementType = MovementType.Electric;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.231;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.77;
            metap.UniqueId = "V0015_POKEMON_BEEDRILL";
            metap.BaseDefense = 130;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 3.6875;
            metap.CylHeightM = 0.77;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.5775;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Kakuna;
            metap.CylGroundM = 0.385;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BugBiteFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.AerialAce,
                    PokemonMove.XScissor
            };
            metap.Number = 15;
            Meta[PokemonId.Beedrill] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0016_POKEMON_PIDGEY";
            metap.Family = PokemonFamilyId.FamilyPidgey;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.252;
            metap.BaseFleeRate = 0.2;
            metap.BaseAttack = 94;
            metap.DiskRadiusM = 0.378;
            metap.CollisionRadiusM = 0.1344;
            metap.PokedexWeightKg = 1.8;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.126;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.4;
            metap.ModelScale = 1.68;
            metap.UniqueId = "V0016_POKEMON_PIDGEY";
            metap.BaseDefense = 90;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.225;
            metap.CylHeightM = 0.504;
            metap.CandyToEvolve = 12;
            metap.CollisionHeightM = 0.252;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.TackleFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.AerialAce,
                    PokemonMove.AirCutter
            };
            metap.Number = 16;
            Meta[PokemonId.Pidgey] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0017_POKEMON_PIDGEOTTO";
            metap.Family = PokemonFamilyId.FamilyPidgey;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 126;
            metap.CylRadiusM = 0.474;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 126;
            metap.DiskRadiusM = 0.711;
            metap.CollisionRadiusM = 0.316;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.237;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.79;
            metap.UniqueId = "V0017_POKEMON_PIDGEOTTO";
            metap.BaseDefense = 122;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 0.9875;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.69125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Pidgey;
            metap.CylGroundM = 0.395;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.WingAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.AerialAce,
                    PokemonMove.AirCutter
            };
            metap.Number = 17;
            Meta[PokemonId.Pidgeotto] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0018_POKEMON_PIDGEOT";
            metap.Family = PokemonFamilyId.FamilyPidgey;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 166;
            metap.CylRadiusM = 0.864;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 170;
            metap.DiskRadiusM = 1.296;
            metap.CollisionRadiusM = 0.36;
            metap.PokedexWeightKg = 39.5;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.216;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.72;
            metap.UniqueId = "V0018_POKEMON_PIDGEOT";
            metap.BaseDefense = 166;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 4.9375;
            metap.CylHeightM = 1.44;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.008;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Pidgeotto;
            metap.CylGroundM = 0.36;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.WingAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Hurricane,
                    PokemonMove.AerialAce,
                    PokemonMove.AirCutter
            };
            metap.Number = 18;
            Meta[PokemonId.Pidgeot] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0019_POKEMON_RATTATA";
            metap.Family = PokemonFamilyId.FamilyRattata;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.252;
            metap.BaseFleeRate = 0.2;
            metap.BaseAttack = 92;
            metap.DiskRadiusM = 0.378;
            metap.CollisionRadiusM = 0.189;
            metap.PokedexWeightKg = 3.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.126;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 0.9;
            metap.ModelScale = 1.26;
            metap.UniqueId = "V0019_POKEMON_RATTATA";
            metap.BaseDefense = 86;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.4375;
            metap.CylHeightM = 0.378;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.252;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.QuickAttackFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.BodySlam,
                    PokemonMove.HyperFang
            };
            metap.Number = 19;
            Meta[PokemonId.Rattata] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0020_POKEMON_RATICATE";
            metap.Family = PokemonFamilyId.FamilyRattata;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.5265;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 146;
            metap.DiskRadiusM = 0.7898;
            metap.CollisionRadiusM = 0.2925;
            metap.PokedexWeightKg = 18.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.26325;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.17;
            metap.UniqueId = "V0020_POKEMON_RATICATE";
            metap.BaseDefense = 150;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.3125;
            metap.CylHeightM = 0.936;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.585;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Rattata;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.HyperBeam,
                    PokemonMove.HyperFang
            };
            metap.Number = 20;
            Meta[PokemonId.Raticate] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0021_POKEMON_SPEAROW";
            metap.Family = PokemonFamilyId.FamilySpearow;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.296;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 102;
            metap.DiskRadiusM = 0.444;
            metap.CollisionRadiusM = 0.148;
            metap.PokedexWeightKg = 2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.148;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0021_POKEMON_SPEAROW";
            metap.BaseDefense = 78;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.25;
            metap.CylHeightM = 0.518;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.2664;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.QuickAttackFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.AerialAce,
                    PokemonMove.DrillPeck
            };
            metap.Number = 21;
            Meta[PokemonId.Spearow] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0022_POKEMON_FEAROW";
            metap.Family = PokemonFamilyId.FamilySpearow;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.504;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 168;
            metap.DiskRadiusM = 1.26;
            metap.CollisionRadiusM = 0.252;
            metap.PokedexWeightKg = 38;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.126;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.84;
            metap.UniqueId = "V0022_POKEMON_FEAROW";
            metap.BaseDefense = 146;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 4.75;
            metap.CylHeightM = 1.05;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.63;
            metap.ShoulderModeScale = 0.375;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Spearow;
            metap.CylGroundM = 0.42;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.AerialAce,
                    PokemonMove.DrillRun
            };
            metap.Number = 22;
            Meta[PokemonId.Fearow] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0023_POKEMON_EKANS";
            metap.Family = PokemonFamilyId.FamilyEkans;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 2;
            metap.HeightStdDev = 0.25;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.4325;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 112;
            metap.DiskRadiusM = 0.6488;
            metap.CollisionRadiusM = 0.2595;
            metap.PokedexWeightKg = 6.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.1384;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.73;
            metap.UniqueId = "V0023_POKEMON_EKANS";
            metap.BaseDefense = 112;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.8625;
            metap.CylHeightM = 0.6055;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.346;
            metap.ShoulderModeScale = 0.375;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.AcidFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.Wrap,
                    PokemonMove.GunkShot
            };
            metap.Number = 23;
            Meta[PokemonId.Ekans] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0024_POKEMON_ARBOK";
            metap.Family = PokemonFamilyId.FamilyEkans;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 3.5;
            metap.HeightStdDev = 0.4375;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.615;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 166;
            metap.DiskRadiusM = 0.9225;
            metap.CollisionRadiusM = 0.41;
            metap.PokedexWeightKg = 65;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.164;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.82;
            metap.UniqueId = "V0024_POKEMON_ARBOK";
            metap.BaseDefense = 166;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 8.125;
            metap.CylHeightM = 1.353;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.353;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Ekans;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.AcidFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DarkPulse,
                    PokemonMove.GunkShot,
                    PokemonMove.SludgeWave
            };
            metap.Number = 24;
            Meta[PokemonId.Arbok] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0025_POKEMON_PIKACHU";
            metap.Family = PokemonFamilyId.FamilyPikachu;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.37;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 124;
            metap.DiskRadiusM = 0.555;
            metap.CollisionRadiusM = 0.185;
            metap.PokedexWeightKg = 6;
            metap.MovementType = MovementType.Normal;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.185;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0025_POKEMON_PIKACHU";
            metap.BaseDefense = 108;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.75;
            metap.CylHeightM = 0.74;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.518;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.QuickAttackFast,
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Thunder,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 25;
            Meta[PokemonId.Pikachu] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0026_POKEMON_RAICHU";
            metap.Family = PokemonFamilyId.FamilyPikachu;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.486;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 200;
            metap.DiskRadiusM = 0.729;
            metap.CollisionRadiusM = 0.27;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.216;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.08;
            metap.UniqueId = "V0026_POKEMON_RAICHU";
            metap.BaseDefense = 154;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 1.35;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.54;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Pikachu;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SparkFast,
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.ThunderPunch,
                    PokemonMove.Thunder,
                    PokemonMove.BrickBreak
            };
            metap.Number = 26;
            Meta[PokemonId.Raichu] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0027_POKEMON_SANDSHREW";
            metap.Family = PokemonFamilyId.FamilySandshrew;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.3225;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 90;
            metap.DiskRadiusM = 0.4838;
            metap.CollisionRadiusM = 0.258;
            metap.PokedexWeightKg = 12;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.1935;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.29;
            metap.UniqueId = "V0027_POKEMON_SANDSHREW";
            metap.BaseDefense = 114;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 1.5;
            metap.CylHeightM = 0.774;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.48375;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.RockSlide,
                    PokemonMove.RockTomb
            };
            metap.Number = 27;
            Meta[PokemonId.Sandshrew] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0028_POKEMON_SANDSLASH";
            metap.Family = PokemonFamilyId.FamilySandshrew;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 150;
            metap.CylRadiusM = 0.4;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 150;
            metap.DiskRadiusM = 0.6;
            metap.CollisionRadiusM = 0.35;
            metap.PokedexWeightKg = 29.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.35;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1;
            metap.UniqueId = "V0028_POKEMON_SANDSLASH";
            metap.BaseDefense = 172;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 3.6875;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.9;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Sandshrew;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.MetalClawFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Bulldoze,
                    PokemonMove.Earthquake,
                    PokemonMove.RockTomb
            };
            metap.Number = 28;
            Meta[PokemonId.Sandslash] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0029_POKEMON_NIDORAN";
            metap.Family = PokemonFamilyId.FamilyNidoranFemale;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.37;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 100;
            metap.DiskRadiusM = 0.555;
            metap.CollisionRadiusM = 0.185;
            metap.PokedexWeightKg = 7;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.185;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0029_POKEMON_NIDORAN";
            metap.BaseDefense = 104;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.875;
            metap.CylHeightM = 0.666;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.37;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.PoisonStingFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PoisonFang,
                    PokemonMove.SludgeBomb,
                    PokemonMove.BodySlam
            };
            metap.Number = 29;
            Meta[PokemonId.NidoranFemale] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0030_POKEMON_NIDORINA";
            metap.Family = PokemonFamilyId.FamilyNidoranFemale;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.4388;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 132;
            metap.DiskRadiusM = 0.6581;
            metap.CollisionRadiusM = 0.2925;
            metap.PokedexWeightKg = 20;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.1755;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.17;
            metap.UniqueId = "V0030_POKEMON_NIDORINA";
            metap.BaseDefense = 136;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.5;
            metap.CylHeightM = 0.87749988;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.585;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.NidoranFemale;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.PoisonStingFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PoisonFang,
                    PokemonMove.Dig,
                    PokemonMove.SludgeBomb
            };
            metap.Number = 30;
            Meta[PokemonId.Nidorina] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0031_POKEMON_NIDOQUEEN";
            metap.Family = PokemonFamilyId.FamilyNidoranFemale;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.4095;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 184;
            metap.DiskRadiusM = 0.6143;
            metap.CollisionRadiusM = 0.455;
            metap.PokedexWeightKg = 60;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.2275;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.91;
            metap.UniqueId = "V0031_POKEMON_NIDOQUEEN";
            metap.BaseDefense = 190;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 7.5;
            metap.CylHeightM = 1.183;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.79625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Nidorina;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.Earthquake,
                    PokemonMove.SludgeWave
            };
            metap.Number = 31;
            Meta[PokemonId.Nidoqueen] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0032_POKEMON_NIDORAN";
            metap.Family = PokemonFamilyId.FamilyNidoranMale;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 92;
            metap.CylRadiusM = 0.4725;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 0.7088;
            metap.CollisionRadiusM = 0.252;
            metap.PokedexWeightKg = 9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.1575;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.26;
            metap.UniqueId = "V0032_POKEMON_NIDORAN";
            metap.BaseDefense = 94;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 1.125;
            metap.CylHeightM = 0.756;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.315;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.HornAttack,
                    PokemonMove.BodySlam
            };
            metap.Number = 32;
            Meta[PokemonId.NidoranMale] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0033_POKEMON_NIDORINO";
            metap.Family = PokemonFamilyId.FamilyNidoranMale;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 122;
            metap.CylRadiusM = 0.495;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 142;
            metap.DiskRadiusM = 0.7425;
            metap.CollisionRadiusM = 0.297;
            metap.PokedexWeightKg = 19.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.2475;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.99;
            metap.UniqueId = "V0033_POKEMON_NIDORINO";
            metap.BaseDefense = 128;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.4375;
            metap.CylHeightM = 0.792;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.594;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.NidoranMale;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.Dig,
                    PokemonMove.HornAttack
            };
            metap.Number = 33;
            Meta[PokemonId.Nidorino] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0034_POKEMON_NIDOKING";
            metap.Family = PokemonFamilyId.FamilyNidoranMale;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 162;
            metap.CylRadiusM = 0.5481;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 204;
            metap.DiskRadiusM = 0.8222;
            metap.CollisionRadiusM = 0.5481;
            metap.PokedexWeightKg = 62;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.27405;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0034_POKEMON_NIDOKING";
            metap.BaseDefense = 170;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 7.75;
            metap.CylHeightM = 1.305;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.87;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Nidorino;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.FuryCutterFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Megahorn,
                    PokemonMove.Earthquake,
                    PokemonMove.SludgeWave
            };
            metap.Number = 34;
            Meta[PokemonId.Nidoking] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0035_POKEMON_CLEFAIRY";
            metap.Family = PokemonFamilyId.FamilyClefairy;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.45;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 116;
            metap.DiskRadiusM = 0.675;
            metap.CollisionRadiusM = 0.3125;
            metap.PokedexWeightKg = 7.5;
            metap.MovementType = MovementType.Normal;
            metap.Type1 = PokemonType.Fairy;
            metap.CollisionHeadRadiusM = 0.225;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.25;
            metap.UniqueId = "V0035_POKEMON_CLEFAIRY";
            metap.BaseDefense = 124;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.9375;
            metap.CylHeightM = 0.75;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.75;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DisarmingVoice,
                    PokemonMove.Moonblast,
                    PokemonMove.BodySlam
            };
            metap.Number = 35;
            Meta[PokemonId.Clefairy] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0036_POKEMON_CLEFABLE";
            metap.Family = PokemonFamilyId.FamilyClefairy;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 190;
            metap.CylRadiusM = 0.712;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 178;
            metap.DiskRadiusM = 1.1681;
            metap.CollisionRadiusM = 0.445;
            metap.PokedexWeightKg = 40;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fairy;
            metap.CollisionHeadRadiusM = 0.445;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.89;
            metap.UniqueId = "V0036_POKEMON_CLEFABLE";
            metap.BaseDefense = 178;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 5;
            metap.CylHeightM = 1.44625;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.1125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Clefairy;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DazzlingGleam,
                    PokemonMove.Psychic,
                    PokemonMove.Moonblast
            };
            metap.Number = 36;
            Meta[PokemonId.Clefable] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0037_POKEMON_VULPIX";
            metap.Family = PokemonFamilyId.FamilyVulpix;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 76;
            metap.CylRadiusM = 0.567;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 106;
            metap.DiskRadiusM = 0.8505;
            metap.CollisionRadiusM = 0.315;
            metap.PokedexWeightKg = 9.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.252;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.26;
            metap.UniqueId = "V0037_POKEMON_VULPIX";
            metap.BaseDefense = 118;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1.2375;
            metap.CylHeightM = 0.756;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.63;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.EmberFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FlameCharge,
                    PokemonMove.Flamethrower,
                    PokemonMove.BodySlam
            };
            metap.Number = 37;
            Meta[PokemonId.Vulpix] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0038_POKEMON_NINETALES";
            metap.Family = PokemonFamilyId.FamilyVulpix;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 146;
            metap.CylRadiusM = 0.864;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 176;
            metap.DiskRadiusM = 1.296;
            metap.CollisionRadiusM = 0.36;
            metap.PokedexWeightKg = 19.9;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.24;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.96;
            metap.UniqueId = "V0038_POKEMON_NINETALES";
            metap.BaseDefense = 194;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 2.4875;
            metap.CylHeightM = 1.2;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.96;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Vulpix;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.FeintAttackFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Flamethrower,
                    PokemonMove.HeatWave,
                    PokemonMove.FireBlast
            };
            metap.Number = 38;
            Meta[PokemonId.Ninetales] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0039_POKEMON_JIGGLYPUFF";
            metap.Family = PokemonFamilyId.FamilyJigglypuff;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Fairy;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 230;
            metap.CylRadiusM = 0.512;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 98;
            metap.DiskRadiusM = 0.768;
            metap.CollisionRadiusM = 0.32;
            metap.PokedexWeightKg = 5.5;
            metap.MovementType = MovementType.Normal;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.256;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 3;
            metap.ModelScale = 1.28;
            metap.UniqueId = "V0039_POKEMON_JIGGLYPUFF";
            metap.BaseDefense = 54;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.6875;
            metap.CylHeightM = 0.96;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.64;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.FeintAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DisarmingVoice,
                    PokemonMove.PlayRough,
                    PokemonMove.BodySlam
            };
            metap.Number = 39;
            Meta[PokemonId.Jigglypuff] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0040_POKEMON_WIGGLYTUFF";
            metap.Family = PokemonFamilyId.FamilyJigglypuff;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Fairy;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 280;
            metap.CylRadiusM = 0.445;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 168;
            metap.DiskRadiusM = 1.0013;
            metap.CollisionRadiusM = 0.356;
            metap.PokedexWeightKg = 12;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.2225;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.89;
            metap.UniqueId = "V0040_POKEMON_WIGGLYTUFF";
            metap.BaseDefense = 108;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 1.5;
            metap.CylHeightM = 1.22375;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.89;
            metap.ShoulderModeScale = 0.4;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Jigglypuff;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.FeintAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DazzlingGleam,
                    PokemonMove.PlayRough,
                    PokemonMove.HyperBeam
            };
            metap.Number = 40;
            Meta[PokemonId.Wigglytuff] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0041_POKEMON_ZUBAT";
            metap.Family = PokemonFamilyId.FamilyZubat;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.642;
            metap.BaseFleeRate = 0.2;
            metap.BaseAttack = 88;
            metap.DiskRadiusM = 0.963;
            metap.CollisionRadiusM = 0.0535;
            metap.PokedexWeightKg = 7.5;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.1605;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.07;
            metap.UniqueId = "V0041_POKEMON_ZUBAT";
            metap.BaseDefense = 90;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.9375;
            metap.CylHeightM = 0.6955;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.0535;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.535;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PoisonFang,
                    PokemonMove.SludgeBomb,
                    PokemonMove.AirCutter
            };
            metap.Number = 41;
            Meta[PokemonId.Zubat] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0042_POKEMON_GOLBAT";
            metap.Family = PokemonFamilyId.FamilyZubat;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 150;
            metap.CylRadiusM = 0.75;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 164;
            metap.DiskRadiusM = 1.5975;
            metap.CollisionRadiusM = 0.0355;
            metap.PokedexWeightKg = 55;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.355;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.71;
            metap.UniqueId = "V0042_POKEMON_GOLBAT";
            metap.BaseDefense = 164;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 6.875;
            metap.CylHeightM = 1.2425;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.0355;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Zubat;
            metap.CylGroundM = 1.065;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.WingAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PoisonFang,
                    PokemonMove.AirCutter,
                    PokemonMove.OminousWind
            };
            metap.Number = 42;
            Meta[PokemonId.Golbat] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0043_POKEMON_ODDISH";
            metap.Family = PokemonFamilyId.FamilyOddish;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.405;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 134;
            metap.DiskRadiusM = 0.6075;
            metap.CollisionRadiusM = 0.2025;
            metap.PokedexWeightKg = 5.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.2025;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.35;
            metap.UniqueId = "V0043_POKEMON_ODDISH";
            metap.BaseDefense = 130;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.675;
            metap.CylHeightM = 0.81000012;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.50625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.48;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.SeedBomb,
                    PokemonMove.Moonblast
            };
            metap.Number = 43;
            Meta[PokemonId.Oddish] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0044_POKEMON_GLOOM";
            metap.Family = PokemonFamilyId.FamilyOddish;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.495;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 162;
            metap.DiskRadiusM = 0.7425;
            metap.CollisionRadiusM = 0.4125;
            metap.PokedexWeightKg = 8.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.2475;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0044_POKEMON_GLOOM";
            metap.BaseDefense = 158;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 1.075;
            metap.CylHeightM = 0.88000011;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.88000011;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Oddish;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.PetalBlizzard,
                    PokemonMove.Moonblast
            };
            metap.Number = 44;
            Meta[PokemonId.Gloom] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0045_POKEMON_VILEPLUME";
            metap.Family = PokemonFamilyId.FamilyOddish;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 150;
            metap.CylRadiusM = 0.828;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 202;
            metap.DiskRadiusM = 1.242;
            metap.CollisionRadiusM = 1.012;
            metap.PokedexWeightKg = 18.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.552;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.92;
            metap.UniqueId = "V0045_POKEMON_VILEPLUME";
            metap.BaseDefense = 190;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 2.325;
            metap.CylHeightM = 1.196;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.196;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Gloom;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Moonblast,
                    PokemonMove.PetalBlizzard,
                    PokemonMove.SolarBeam
            };
            metap.Number = 45;
            Meta[PokemonId.Vileplume] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0046_POKEMON_PARAS";
            metap.Family = PokemonFamilyId.FamilyParas;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Grass;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.384;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 122;
            metap.DiskRadiusM = 0.576;
            metap.CollisionRadiusM = 0.192;
            metap.PokedexWeightKg = 5.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.192;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1.1;
            metap.ModelScale = 1.28;
            metap.UniqueId = "V0046_POKEMON_PARAS";
            metap.BaseDefense = 120;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.675;
            metap.CylHeightM = 0.448;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.32;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BugBiteFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.CrossPoison,
                    PokemonMove.XScissor,
                    PokemonMove.SeedBomb
            };
            metap.Number = 46;
            Meta[PokemonId.Paras] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0047_POKEMON_PARASECT";
            metap.Family = PokemonFamilyId.FamilyParas;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Grass;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.6313;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 162;
            metap.DiskRadiusM = 0.9469;
            metap.CollisionRadiusM = 0.4545;
            metap.PokedexWeightKg = 29.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.505;
            metap.MovementTimerS = 17;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.01;
            metap.UniqueId = "V0047_POKEMON_PARASECT";
            metap.BaseDefense = 170;
            metap.AttackTimerS = 6;
            metap.WeightStdDev = 3.6875;
            metap.CylHeightM = 1.01;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.01;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Paras;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BugBiteFast,
                    PokemonMove.FuryCutterFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.CrossPoison,
                    PokemonMove.XScissor,
                    PokemonMove.SolarBeam
            };
            metap.Number = 47;
            Meta[PokemonId.Parasect] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0048_POKEMON_VENONAT";
            metap.Family = PokemonFamilyId.FamilyVenonat;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.5325;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 108;
            metap.DiskRadiusM = 0.7988;
            metap.CollisionRadiusM = 0.355;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.26625;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.71;
            metap.UniqueId = "V0048_POKEMON_VENONAT";
            metap.BaseDefense = 118;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 1.1715;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.71;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.BugBiteFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DazzlingGleam,
                    PokemonMove.ShadowBall,
                    PokemonMove.Psybeam
            };
            metap.Number = 48;
            Meta[PokemonId.Venonat] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0049_POKEMON_VENOMOTH";
            metap.Family = PokemonFamilyId.FamilyVenonat;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.576;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 172;
            metap.DiskRadiusM = 0.864;
            metap.CollisionRadiusM = 0.36;
            metap.PokedexWeightKg = 12.5;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.288;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.72;
            metap.UniqueId = "V0049_POKEMON_VENOMOTH";
            metap.BaseDefense = 154;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 1.5625;
            metap.CylHeightM = 1.08;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.72;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Venonat;
            metap.CylGroundM = 0.36;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.BugBiteFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PoisonFang,
                    PokemonMove.Psychic,
                    PokemonMove.BugBuzz
            };
            metap.Number = 49;
            Meta[PokemonId.Venomoth] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0050_POKEMON_DIGLETT";
            metap.Family = PokemonFamilyId.FamilyDiglett;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.2;
            metap.HeightStdDev = 0.025;
            metap.BaseStamina = 20;
            metap.CylRadiusM = 0.3;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 108;
            metap.DiskRadiusM = 0.45;
            metap.CollisionRadiusM = 0.16;
            metap.PokedexWeightKg = 0.8;
            metap.MovementType = MovementType.Normal;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.18;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 0;
            metap.ModelScale = 2;
            metap.UniqueId = "V0050_POKEMON_DIGLETT";
            metap.BaseDefense = 86;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.1;
            metap.CylHeightM = 0.4;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.4;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.MudBomb,
                    PokemonMove.RockTomb
            };
            metap.Number = 50;
            Meta[PokemonId.Diglett] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0051_POKEMON_DUGTRIO";
            metap.Family = PokemonFamilyId.FamilyDiglett;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.672;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 148;
            metap.DiskRadiusM = 1.008;
            metap.CollisionRadiusM = 0.448;
            metap.PokedexWeightKg = 33.3;
            metap.MovementType = MovementType.Normal;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.336;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 0;
            metap.ModelScale = 1.12;
            metap.UniqueId = "V0051_POKEMON_DUGTRIO";
            metap.BaseDefense = 140;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 4.1625;
            metap.CylHeightM = 0.84;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.84;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Diglett;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SuckerPunchFast,
                    PokemonMove.MudShotFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.Earthquake,
                    PokemonMove.MudBomb
            };
            metap.Number = 51;
            Meta[PokemonId.Dugtrio] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0052_POKEMON_MEOWTH";
            metap.Family = PokemonFamilyId.FamilyMeowth;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.4;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 104;
            metap.DiskRadiusM = 0.6;
            metap.CollisionRadiusM = 0.128;
            metap.PokedexWeightKg = 4.2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.2;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.6;
            metap.UniqueId = "V0052_POKEMON_MEOWTH";
            metap.BaseDefense = 94;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.525;
            metap.CylHeightM = 0.64;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.4;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DarkPulse,
                    PokemonMove.NightSlash,
                    PokemonMove.BodySlam
            };
            metap.Number = 52;
            Meta[PokemonId.Meowth] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0053_POKEMON_PERSIAN";
            metap.Family = PokemonFamilyId.FamilyMeowth;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.533;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 156;
            metap.DiskRadiusM = 0.7995;
            metap.CollisionRadiusM = 0.328;
            metap.PokedexWeightKg = 32;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.164;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.82;
            metap.UniqueId = "V0053_POKEMON_PERSIAN";
            metap.BaseDefense = 146;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 4;
            metap.CylHeightM = 0.902;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.615;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Meowth;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ScratchFast,
                    PokemonMove.FeintAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PlayRough,
                    PokemonMove.PowerGem,
                    PokemonMove.NightSlash
            };
            metap.Number = 53;
            Meta[PokemonId.Persian] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0054_POKEMON_PSYDUCK";
            metap.Family = PokemonFamilyId.FamilyPsyduck;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.3638;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 132;
            metap.DiskRadiusM = 0.5456;
            metap.CollisionRadiusM = 0.291;
            metap.PokedexWeightKg = 19.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.3395;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.97;
            metap.UniqueId = "V0054_POKEMON_PSYDUCK";
            metap.BaseDefense = 112;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 2.45;
            metap.CylHeightM = 0.97;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.60625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AquaTail,
                    PokemonMove.Psybeam,
                    PokemonMove.CrossChop
            };
            metap.Number = 54;
            Meta[PokemonId.Psyduck] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0055_POKEMON_GOLDUCK";
            metap.Family = PokemonFamilyId.FamilyPsyduck;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.465;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 194;
            metap.DiskRadiusM = 0.9765;
            metap.CollisionRadiusM = 0.2325;
            metap.PokedexWeightKg = 76.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.2325;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.93;
            metap.UniqueId = "V0055_POKEMON_GOLDUCK";
            metap.BaseDefense = 176;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 9.575;
            metap.CylHeightM = 1.3485;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.81375;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Psyduck;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.HydroPump,
                    PokemonMove.IceBeam
            };
            metap.Number = 55;
            Meta[PokemonId.Golduck] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0056_POKEMON_MANKEY";
            metap.Family = PokemonFamilyId.FamilyMankey;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.4838;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 122;
            metap.DiskRadiusM = 0.7256;
            metap.CollisionRadiusM = 0.1935;
            metap.PokedexWeightKg = 28;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.129;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.29;
            metap.UniqueId = "V0056_POKEMON_MANKEY";
            metap.BaseDefense = 96;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 3.5;
            metap.CylHeightM = 0.80625;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.645;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.LowSweep,
                    PokemonMove.BrickBreak,
                    PokemonMove.CrossChop
            };
            metap.Number = 56;
            Meta[PokemonId.Mankey] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0057_POKEMON_PRIMEAPE";
            metap.Family = PokemonFamilyId.FamilyMankey;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.46;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 178;
            metap.DiskRadiusM = 0.69;
            metap.CollisionRadiusM = 0.46;
            metap.PokedexWeightKg = 32;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.23;
            metap.MovementTimerS = 17;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.92;
            metap.UniqueId = "V0057_POKEMON_PRIMEAPE";
            metap.BaseDefense = 150;
            metap.AttackTimerS = 6;
            metap.WeightStdDev = 4;
            metap.CylHeightM = 1.15;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.104;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Mankey;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.LowKickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.LowSweep,
                    PokemonMove.NightSlash,
                    PokemonMove.CrossChop
            };
            metap.Number = 57;
            Meta[PokemonId.Primeape] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0058_POKEMON_GROWLITHE";
            metap.Family = PokemonFamilyId.FamilyGrowlithe;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.585;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 156;
            metap.DiskRadiusM = 0.8775;
            metap.CollisionRadiusM = 0.234;
            metap.PokedexWeightKg = 19;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.1755;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.17;
            metap.UniqueId = "V0058_POKEMON_GROWLITHE";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 2.375;
            metap.CylHeightM = 1.02375;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.585;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FlameWheel,
                    PokemonMove.Flamethrower,
                    PokemonMove.BodySlam
            };
            metap.Number = 58;
            Meta[PokemonId.Growlithe] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0059_POKEMON_ARCANINE";
            metap.Family = PokemonFamilyId.FamilyGrowlithe;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.9;
            metap.HeightStdDev = 0.2375;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.666;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 230;
            metap.DiskRadiusM = 0.999;
            metap.CollisionRadiusM = 0.37;
            metap.PokedexWeightKg = 155;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.333;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.74;
            metap.UniqueId = "V0059_POKEMON_ARCANINE";
            metap.BaseDefense = 180;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 19.375;
            metap.CylHeightM = 1.48;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.74;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Growlithe;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.FireFangFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Bulldoze,
                    PokemonMove.Flamethrower,
                    PokemonMove.FireBlast
            };
            metap.Number = 59;
            Meta[PokemonId.Arcanine] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0060_POKEMON_POLIWAG";
            metap.Family = PokemonFamilyId.FamilyPoliwag;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.5;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 108;
            metap.DiskRadiusM = 0.75;
            metap.CollisionRadiusM = 0.3125;
            metap.PokedexWeightKg = 12.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.3125;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.25;
            metap.UniqueId = "V0060_POKEMON_POLIWAG";
            metap.BaseDefense = 98;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1.55;
            metap.CylHeightM = 0.875;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.75;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.MudBomb,
                    PokemonMove.BubbleBeam,
                    PokemonMove.BodySlam
            };
            metap.Number = 60;
            Meta[PokemonId.Poliwag] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0061_POKEMON_POLIWHIRL";
            metap.Family = PokemonFamilyId.FamilyPoliwag;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.735;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 132;
            metap.DiskRadiusM = 1.1025;
            metap.CollisionRadiusM = 0.49;
            metap.PokedexWeightKg = 20;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.3675;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 0.8;
            metap.ModelScale = 0.98;
            metap.UniqueId = "V0061_POKEMON_POLIWHIRL";
            metap.BaseDefense = 132;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 2.5;
            metap.CylHeightM = 1.078;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.882;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Poliwag;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Scald,
                    PokemonMove.MudBomb,
                    PokemonMove.BubbleBeam
            };
            metap.Number = 61;
            Meta[PokemonId.Poliwhirl] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0062_POKEMON_POLIWRATH";
            metap.Family = PokemonFamilyId.FamilyPoliwag;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Fighting;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.817;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 180;
            metap.DiskRadiusM = 1.2255;
            metap.CollisionRadiusM = 0.645;
            metap.PokedexWeightKg = 54;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.344;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1.05;
            metap.ModelScale = 0.86;
            metap.UniqueId = "V0062_POKEMON_POLIWRATH";
            metap.BaseDefense = 202;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 6.75;
            metap.CylHeightM = 1.204;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.118;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Poliwhirl;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.HydroPump,
                    PokemonMove.Submission,
                    PokemonMove.IcePunch
            };
            metap.Number = 62;
            Meta[PokemonId.Poliwrath] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0063_POKEMON_ABRA";
            metap.Family = PokemonFamilyId.FamilyAbra;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 50;
            metap.CylRadiusM = 0.448;
            metap.BaseFleeRate = 0.99;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 0.672;
            metap.CollisionRadiusM = 0.28;
            metap.PokedexWeightKg = 19.5;
            metap.MovementType = MovementType.Psychic;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.28;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.12;
            metap.UniqueId = "V0063_POKEMON_ABRA";
            metap.BaseDefense = 76;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 2.4375;
            metap.CylHeightM = 0.784;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.56;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.168;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.ShadowBall,
                    PokemonMove.Psyshock,
                    PokemonMove.SignalBeam
            };
            metap.Number = 63;
            Meta[PokemonId.Abra] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0064_POKEMON_KADABRA";
            metap.Family = PokemonFamilyId.FamilyAbra;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.6675;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 150;
            metap.DiskRadiusM = 1.0013;
            metap.CollisionRadiusM = 0.445;
            metap.PokedexWeightKg = 56.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.33375;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.89;
            metap.UniqueId = "V0064_POKEMON_KADABRA";
            metap.BaseDefense = 112;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 7.0625;
            metap.CylHeightM = 1.157;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.89;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Abra;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.PsychoCutFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DazzlingGleam,
                    PokemonMove.ShadowBall,
                    PokemonMove.Psybeam
            };
            metap.Number = 64;
            Meta[PokemonId.Kadabra] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0065_POKEMON_ALAKAZAM";
            metap.Family = PokemonFamilyId.FamilyAbra;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.51;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 186;
            metap.DiskRadiusM = 0.765;
            metap.CollisionRadiusM = 0.425;
            metap.PokedexWeightKg = 48;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.255;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.85;
            metap.UniqueId = "V0065_POKEMON_ALAKAZAM";
            metap.BaseDefense = 152;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 6;
            metap.CylHeightM = 1.275;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.93500012;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Kadabra;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.PsychoCutFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.DazzlingGleam,
                    PokemonMove.ShadowBall
            };
            metap.Number = 65;
            Meta[PokemonId.Alakazam] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0066_POKEMON_MACHOP";
            metap.Family = PokemonFamilyId.FamilyMachop;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.4125;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 118;
            metap.DiskRadiusM = 0.6188;
            metap.CollisionRadiusM = 0.22;
            metap.PokedexWeightKg = 19.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.20625;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0066_POKEMON_MACHOP";
            metap.BaseDefense = 96;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 2.4375;
            metap.CylHeightM = 0.88000011;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.55;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.LowKickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.LowSweep,
                    PokemonMove.BrickBreak,
                    PokemonMove.CrossChop
            };
            metap.Number = 66;
            Meta[PokemonId.Machop] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0067_POKEMON_MACHOKE";
            metap.Family = PokemonFamilyId.FamilyMachop;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.546;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 154;
            metap.DiskRadiusM = 0.819;
            metap.CollisionRadiusM = 0.54600012;
            metap.PokedexWeightKg = 70.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.1365;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.91;
            metap.UniqueId = "V0067_POKEMON_MACHOKE";
            metap.BaseDefense = 144;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 8.8125;
            metap.CylHeightM = 1.274;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 1.092;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Machop;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.LowKickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Submission,
                    PokemonMove.BrickBreak,
                    PokemonMove.CrossChop
            };
            metap.Number = 67;
            Meta[PokemonId.Machoke] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0068_POKEMON_MACHAMP";
            metap.Family = PokemonFamilyId.FamilyMachop;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.5785;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 198;
            metap.DiskRadiusM = 0.8678;
            metap.CollisionRadiusM = 0.5785;
            metap.PokedexWeightKg = 130;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.1335;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.89;
            metap.UniqueId = "V0068_POKEMON_MACHAMP";
            metap.BaseDefense = 180;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 16.25;
            metap.CylHeightM = 1.424;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.246;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Machoke;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.BulletPunchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.Submission,
                    PokemonMove.CrossChop
            };
            metap.Number = 68;
            Meta[PokemonId.Machamp] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0069_POKEMON_BELLSPROUT";
            metap.Family = PokemonFamilyId.FamilyBellsprout;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.7;
            metap.HeightStdDev = 0.0875;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.4515;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 158;
            metap.DiskRadiusM = 0.6773;
            metap.CollisionRadiusM = 0.1935;
            metap.PokedexWeightKg = 4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.22575;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 1.29;
            metap.UniqueId = "V0069_POKEMON_BELLSPROUT";
            metap.BaseDefense = 78;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.5;
            metap.CylHeightM = 0.90299988;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.4515;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.VineWhipFast,
                    PokemonMove.AcidFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PowerWhip,
                    PokemonMove.SludgeBomb,
                    PokemonMove.Wrap
            };
            metap.Number = 69;
            Meta[PokemonId.Bellsprout] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0070_POKEMON_WEEPINBELL";
            metap.Family = PokemonFamilyId.FamilyBellsprout;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.65;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 190;
            metap.DiskRadiusM = 0.975;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 6.4;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.25;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1;
            metap.UniqueId = "V0070_POKEMON_WEEPINBELL";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.8;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.95;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Bellsprout;
            metap.CylGroundM = 0.375;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PowerWhip,
                    PokemonMove.SludgeBomb,
                    PokemonMove.SeedBomb
            };
            metap.Number = 70;
            Meta[PokemonId.Weepinbell] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0071_POKEMON_VICTREEBEL";
            metap.Family = PokemonFamilyId.FamilyBellsprout;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.546;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 222;
            metap.DiskRadiusM = 0.819;
            metap.CollisionRadiusM = 0.336;
            metap.PokedexWeightKg = 15.5;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.273;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.84;
            metap.UniqueId = "V0071_POKEMON_VICTREEBEL";
            metap.BaseDefense = 152;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 1.9375;
            metap.CylHeightM = 1.428;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.428;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Weepinbell;
            metap.CylGroundM = 0.42;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.RazorLeafFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.LeafBlade,
                    PokemonMove.SolarBeam
            };
            metap.Number = 71;
            Meta[PokemonId.Victreebel] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0072_POKEMON_TENTACOOL";
            metap.Family = PokemonFamilyId.FamilyTentacool;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.315;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 106;
            metap.DiskRadiusM = 0.4725;
            metap.CollisionRadiusM = 0.21;
            metap.PokedexWeightKg = 45.5;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.1575;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.05;
            metap.UniqueId = "V0072_POKEMON_TENTACOOL";
            metap.BaseDefense = 136;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 5.6875;
            metap.CylHeightM = 0.91874993;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.91874993;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.2625;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonStingFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.WaterPulse,
                    PokemonMove.BubbleBeam,
                    PokemonMove.Wrap
            };
            metap.Number = 72;
            Meta[PokemonId.Tentacool] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0073_POKEMON_TENTACRUEL";
            metap.Family = PokemonFamilyId.FamilyTentacool;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.492;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 170;
            metap.DiskRadiusM = 0.738;
            metap.CollisionRadiusM = 0.492;
            metap.PokedexWeightKg = 55;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.246;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.82;
            metap.UniqueId = "V0073_POKEMON_TENTACRUEL";
            metap.BaseDefense = 196;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 6.875;
            metap.CylHeightM = 1.312;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.23;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Tentacool;
            metap.CylGroundM = 0.205;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Blizzard,
                    PokemonMove.HydroPump,
                    PokemonMove.SludgeWave
            };
            metap.Number = 73;
            Meta[PokemonId.Tentacruel] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0074_POKEMON_GEODUDE";
            metap.Family = PokemonFamilyId.FamilyGeodude;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.3915;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 106;
            metap.DiskRadiusM = 0.5873;
            metap.CollisionRadiusM = 0.3915;
            metap.PokedexWeightKg = 20;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.19575;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0074_POKEMON_GEODUDE";
            metap.BaseDefense = 118;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 2.5;
            metap.CylHeightM = 0.348;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.1305;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.261;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.RockThrowFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.RockSlide,
                    PokemonMove.RockTomb
            };
            metap.Number = 74;
            Meta[PokemonId.Geodude] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0075_POKEMON_GRAVELER";
            metap.Family = PokemonFamilyId.FamilyGeodude;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.697;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 142;
            metap.DiskRadiusM = 1.0455;
            metap.CollisionRadiusM = 0.492;
            metap.PokedexWeightKg = 105;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.369;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.82;
            metap.UniqueId = "V0075_POKEMON_GRAVELER";
            metap.BaseDefense = 156;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 13.125;
            metap.CylHeightM = 0.82;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 0.697;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.2;
            metap.ParentId = PokemonId.Geodude;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.RockThrowFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.RockSlide,
                    PokemonMove.StoneEdge
            };
            metap.Number = 75;
            Meta[PokemonId.Graveler] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0076_POKEMON_GOLEM";
            metap.Family = PokemonFamilyId.FamilyGeodude;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.63;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 176;
            metap.DiskRadiusM = 0.945;
            metap.CollisionRadiusM = 0.63;
            metap.PokedexWeightKg = 300;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.315;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.84;
            metap.UniqueId = "V0076_POKEMON_GOLEM";
            metap.BaseDefense = 198;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 37.5;
            metap.CylHeightM = 1.092;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.092;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.1;
            metap.ParentId = PokemonId.Graveler;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.RockThrowFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.Earthquake,
                    PokemonMove.AncientPower
            };
            metap.Number = 76;
            Meta[PokemonId.Golem] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0077_POKEMON_PONYTA";
            metap.Family = PokemonFamilyId.FamilyPonyta;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.3788;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 168;
            metap.DiskRadiusM = 0.5681;
            metap.CollisionRadiusM = 0.2525;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.202;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 0.95;
            metap.ModelScale = 1.01;
            metap.UniqueId = "V0077_POKEMON_PONYTA";
            metap.BaseDefense = 138;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 1.2625;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.63125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.EmberFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FlameWheel,
                    PokemonMove.FlameCharge,
                    PokemonMove.FireBlast
            };
            metap.Number = 77;
            Meta[PokemonId.Ponyta] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0078_POKEMON_RAPIDASH";
            metap.Family = PokemonFamilyId.FamilyPonyta;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.405;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 200;
            metap.DiskRadiusM = 0.6075;
            metap.CollisionRadiusM = 0.324;
            metap.PokedexWeightKg = 95;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.243;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.81;
            metap.UniqueId = "V0078_POKEMON_RAPIDASH";
            metap.BaseDefense = 170;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 11.875;
            metap.CylHeightM = 1.701;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.891;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Ponyta;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.LowKickFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.HeatWave,
                    PokemonMove.DrillRun,
                    PokemonMove.FireBlast
            };
            metap.Number = 78;
            Meta[PokemonId.Rapidash] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0079_POKEMON_SLOWPOKE";
            metap.Family = PokemonFamilyId.FamilySlowpoke;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.5925;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 1.185;
            metap.CollisionRadiusM = 0.316;
            metap.PokedexWeightKg = 36;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.29625;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.79;
            metap.UniqueId = "V0079_POKEMON_SLOWPOKE";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 4.5;
            metap.CylHeightM = 0.94800007;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.5135;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.WaterPulse,
                    PokemonMove.Psyshock
            };
            metap.Number = 79;
            Meta[PokemonId.Slowpoke] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0080_POKEMON_SLOWBRO";
            metap.Family = PokemonFamilyId.FamilySlowpoke;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 190;
            metap.CylRadiusM = 0.4675;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 184;
            metap.DiskRadiusM = 0.7013;
            metap.CollisionRadiusM = 0.425;
            metap.PokedexWeightKg = 78.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.255;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.85;
            metap.UniqueId = "V0080_POKEMON_SLOWBRO";
            metap.BaseDefense = 198;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 9.8125;
            metap.CylHeightM = 1.275;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.85;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Slowpoke;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.WaterPulse,
                    PokemonMove.IceBeam
            };
            metap.Number = 80;
            Meta[PokemonId.Slowbro] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0081_POKEMON_MAGNEMITE";
            metap.Family = PokemonFamilyId.FamilyMagnemite;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Steel;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 50;
            metap.CylRadiusM = 0.456;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 128;
            metap.DiskRadiusM = 0.684;
            metap.CollisionRadiusM = 0.456;
            metap.PokedexWeightKg = 6;
            metap.MovementType = MovementType.Electric;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.228;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.52;
            metap.UniqueId = "V0081_POKEMON_MAGNEMITE";
            metap.BaseDefense = 138;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.75;
            metap.CylHeightM = 0.456;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.456;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.912;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SparkFast,
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.MagnetBomb,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 81;
            Meta[PokemonId.Magnemite] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0082_POKEMON_MAGNETON";
            metap.Family = PokemonFamilyId.FamilyMagnemite;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Steel;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.44;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 186;
            metap.DiskRadiusM = 0.66;
            metap.CollisionRadiusM = 0.44;
            metap.PokedexWeightKg = 60;
            metap.MovementType = MovementType.Electric;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.22;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0082_POKEMON_MAGNETON";
            metap.BaseDefense = 180;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 7.5;
            metap.CylHeightM = 1.1;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.825;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Magnemite;
            metap.CylGroundM = 0.44;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SparkFast,
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.MagnetBomb,
                    PokemonMove.FlashCannon,
                    PokemonMove.Discharge
            };
            metap.Number = 82;
            Meta[PokemonId.Magneton] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0083_POKEMON_FARFETCHD";
            metap.Family = PokemonFamilyId.FamilyFarfetchd;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 104;
            metap.CylRadiusM = 0.452;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 138;
            metap.DiskRadiusM = 0.678;
            metap.CollisionRadiusM = 0.2825;
            metap.PokedexWeightKg = 15;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.2825;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.13;
            metap.UniqueId = "V0083_POKEMON_FARFETCHD";
            metap.BaseDefense = 132;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 1.875;
            metap.CylHeightM = 0.8475;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.42375;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.FuryCutterFast,
                    PokemonMove.CutFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AerialAce,
                    PokemonMove.LeafBlade,
                    PokemonMove.AirCutter
            };
            metap.Number = 83;
            Meta[PokemonId.Farfetchd] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0084_POKEMON_DODUO";
            metap.Family = PokemonFamilyId.FamilyDoduo;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.396;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 126;
            metap.DiskRadiusM = 0.594;
            metap.CollisionRadiusM = 0.352;
            metap.PokedexWeightKg = 39.2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.198;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.88;
            metap.UniqueId = "V0084_POKEMON_DODUO";
            metap.BaseDefense = 96;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 4.9;
            metap.CylHeightM = 1.232;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 1.232;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.QuickAttackFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AerialAce,
                    PokemonMove.DrillPeck,
                    PokemonMove.Swift
            };
            metap.Number = 84;
            Meta[PokemonId.Doduo] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0085_POKEMON_DODRIO";
            metap.Family = PokemonFamilyId.FamilyDoduo;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.8;
            metap.HeightStdDev = 0.225;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.5148;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 182;
            metap.DiskRadiusM = 0.7722;
            metap.CollisionRadiusM = 0.39;
            metap.PokedexWeightKg = 85.2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.2574;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.78;
            metap.UniqueId = "V0085_POKEMON_DODRIO";
            metap.BaseDefense = 150;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 10.65;
            metap.CylHeightM = 1.287;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.287;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Doduo;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.FeintAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AerialAce,
                    PokemonMove.DrillPeck,
                    PokemonMove.AirCutter
            };
            metap.Number = 85;
            Meta[PokemonId.Dodrio] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0086_POKEMON_SEEL";
            metap.Family = PokemonFamilyId.FamilySeel;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.275;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 104;
            metap.DiskRadiusM = 0.4125;
            metap.CollisionRadiusM = 0.275;
            metap.PokedexWeightKg = 90;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.22;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 0.9;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0086_POKEMON_SEEL";
            metap.BaseDefense = 138;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 11.25;
            metap.CylHeightM = 0.55;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.4125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast,
                    PokemonMove.IceShardFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.AquaTail,
                    PokemonMove.AquaJet
            };
            metap.Number = 86;
            Meta[PokemonId.Seel] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0087_POKEMON_DEWGONG";
            metap.Family = PokemonFamilyId.FamilySeel;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Ice;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.525;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 156;
            metap.DiskRadiusM = 0.7875;
            metap.CollisionRadiusM = 0.315;
            metap.PokedexWeightKg = 120;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.13125;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.05;
            metap.UniqueId = "V0087_POKEMON_DEWGONG";
            metap.BaseDefense = 192;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 15;
            metap.CylHeightM = 0.84;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.63;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Seel;
            metap.CylGroundM = 0.39375;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.IceShardFast,
                    PokemonMove.FrostBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.Blizzard,
                    PokemonMove.AquaJet
            };
            metap.Number = 87;
            Meta[PokemonId.Dewgong] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0088_POKEMON_GRIMER";
            metap.Family = PokemonFamilyId.FamilyGrimer;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.588;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 124;
            metap.DiskRadiusM = 0.882;
            metap.CollisionRadiusM = 0.49;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.294;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.98;
            metap.UniqueId = "V0088_POKEMON_GRIMER";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 0.98;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.83300012;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.AcidFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.MudBomb,
                    PokemonMove.Sludge
            };
            metap.Number = 88;
            Meta[PokemonId.Grimer] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0089_POKEMON_MUK";
            metap.Family = PokemonFamilyId.FamilyGrimer;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 210;
            metap.CylRadiusM = 0.86;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 180;
            metap.DiskRadiusM = 1.14;
            metap.CollisionRadiusM = 0.76;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.38;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.76;
            metap.UniqueId = "V0089_POKEMON_MUK";
            metap.BaseDefense = 188;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 0.912;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.57;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Grimer;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.PoisonJabFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DarkPulse,
                    PokemonMove.GunkShot,
                    PokemonMove.SludgeWave
            };
            metap.Number = 89;
            Meta[PokemonId.Muk] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0090_POKEMON_SHELLDER";
            metap.Family = PokemonFamilyId.FamilyShellder;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.3864;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 120;
            metap.DiskRadiusM = 0.5796;
            metap.CollisionRadiusM = 0.336;
            metap.PokedexWeightKg = 4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.294;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 1.68;
            metap.UniqueId = "V0090_POKEMON_SHELLDER";
            metap.BaseDefense = 112;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 0.5;
            metap.CylHeightM = 0.504;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.504;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.IceShardFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.WaterPulse,
                    PokemonMove.BubbleBeam
            };
            metap.Number = 90;
            Meta[PokemonId.Shellder] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0091_POKEMON_CLOYSTER";
            metap.Family = PokemonFamilyId.FamilyShellder;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Ice;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.63;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 196;
            metap.DiskRadiusM = 0.945;
            metap.CollisionRadiusM = 0.42;
            metap.PokedexWeightKg = 132.5;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.54599988;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.84;
            metap.UniqueId = "V0091_POKEMON_CLOYSTER";
            metap.BaseDefense = 196;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 16.5625;
            metap.CylHeightM = 1.05;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.05;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Shellder;
            metap.CylGroundM = 0.42;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.IceShardFast,
                    PokemonMove.FrostBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.Blizzard,
                    PokemonMove.HydroPump
            };
            metap.Number = 91;
            Meta[PokemonId.Cloyster] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0092_POKEMON_GASTLY";
            metap.Family = PokemonFamilyId.FamilyGastly;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.45;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 136;
            metap.DiskRadiusM = 0.675;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 0.1;
            metap.MovementType = MovementType.Psychic;
            metap.Type1 = PokemonType.Ghost;
            metap.CollisionHeadRadiusM = 0.3;
            metap.MovementTimerS = 29;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1;
            metap.UniqueId = "V0092_POKEMON_GASTLY";
            metap.BaseDefense = 82;
            metap.AttackTimerS = 10;
            metap.WeightStdDev = 0.0125;
            metap.CylHeightM = 0.8;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.6;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.6;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SuckerPunchFast,
                    PokemonMove.LickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.DarkPulse,
                    PokemonMove.OminousWind
            };
            metap.Number = 92;
            Meta[PokemonId.Gastly] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0093_POKEMON_HAUNTER";
            metap.Family = PokemonFamilyId.FamilyGastly;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.51;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 172;
            metap.DiskRadiusM = 0.765;
            metap.CollisionRadiusM = 0.442;
            metap.PokedexWeightKg = 0.1;
            metap.MovementType = MovementType.Psychic;
            metap.Type1 = PokemonType.Ghost;
            metap.CollisionHeadRadiusM = 0.442;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.68;
            metap.UniqueId = "V0093_POKEMON_HAUNTER";
            metap.BaseDefense = 118;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 0.0125;
            metap.CylHeightM = 1.088;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 1.156;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Gastly;
            metap.CylGroundM = 0.34;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ShadowClawFast,
                    PokemonMove.LickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.ShadowBall,
                    PokemonMove.DarkPulse
            };
            metap.Number = 93;
            Meta[PokemonId.Haunter] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0094_POKEMON_GENGAR";
            metap.Family = PokemonFamilyId.FamilyGastly;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Poison;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.462;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 204;
            metap.DiskRadiusM = 0.693;
            metap.CollisionRadiusM = 0.462;
            metap.PokedexWeightKg = 40.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ghost;
            metap.CollisionHeadRadiusM = 0.504;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.3;
            metap.ModelScale = 0.84;
            metap.UniqueId = "V0094_POKEMON_GENGAR";
            metap.BaseDefense = 156;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 5.0625;
            metap.CylHeightM = 1.176;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.092;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Haunter;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SuckerPunchFast,
                    PokemonMove.ShadowClawFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.ShadowBall,
                    PokemonMove.DarkPulse,
                    PokemonMove.SludgeWave
            };
            metap.Number = 94;
            Meta[PokemonId.Gengar] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0095_POKEMON_ONIX";
            metap.Family = PokemonFamilyId.FamilyOnix;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Ground;
            metap.PokedexHeightM = 8.8;
            metap.HeightStdDev = 1.1;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.658;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 90;
            metap.DiskRadiusM = 0.987;
            metap.CollisionRadiusM = 0.658;
            metap.PokedexWeightKg = 210;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.376;
            metap.MovementTimerS = 17;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.47;
            metap.UniqueId = "V0095_POKEMON_ONIX";
            metap.BaseDefense = 186;
            metap.AttackTimerS = 6;
            metap.WeightStdDev = 26.25;
            metap.CylHeightM = 1.41;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.175;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.RockThrowFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IronHead,
                    PokemonMove.StoneEdge,
                    PokemonMove.RockSlide
            };
            metap.Number = 95;
            Meta[PokemonId.Onix] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0096_POKEMON_DROWZEE";
            metap.Family = PokemonFamilyId.FamilyDrowzee;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.42;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 104;
            metap.DiskRadiusM = 0.63;
            metap.CollisionRadiusM = 0.3675;
            metap.PokedexWeightKg = 32.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.2625;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1.05;
            metap.UniqueId = "V0096_POKEMON_DROWZEE";
            metap.BaseDefense = 140;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 4.05;
            metap.CylHeightM = 1.05;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.63;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.PoundFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.Psyshock,
                    PokemonMove.Psybeam
            };
            metap.Number = 96;
            Meta[PokemonId.Drowzee] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0097_POKEMON_HYPNO";
            metap.Family = PokemonFamilyId.FamilyDrowzee;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 170;
            metap.CylRadiusM = 0.6225;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 162;
            metap.DiskRadiusM = 0.9338;
            metap.CollisionRadiusM = 0.332;
            metap.PokedexWeightKg = 75.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.332;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 0.8;
            metap.ModelScale = 0.83;
            metap.UniqueId = "V0097_POKEMON_HYPNO";
            metap.BaseDefense = 196;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 9.45;
            metap.CylHeightM = 1.328;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.83;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Drowzee;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.ShadowBall,
                    PokemonMove.Psyshock
            };
            metap.Number = 97;
            Meta[PokemonId.Hypno] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0098_POKEMON_KRABBY";
            metap.Family = PokemonFamilyId.FamilyKrabby;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.522;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 116;
            metap.DiskRadiusM = 0.783;
            metap.CollisionRadiusM = 0.522;
            metap.PokedexWeightKg = 6.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.261;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.16;
            metap.UniqueId = "V0098_POKEMON_KRABBY";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 0.8125;
            metap.CylHeightM = 0.87;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.87;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.WaterPulse,
                    PokemonMove.ViceGrip,
                    PokemonMove.BubbleBeam
            };
            metap.Number = 98;
            Meta[PokemonId.Krabby] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0099_POKEMON_KINGLER";
            metap.Family = PokemonFamilyId.FamilyKrabby;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.6525;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 178;
            metap.DiskRadiusM = 0.9788;
            metap.CollisionRadiusM = 0.6525;
            metap.PokedexWeightKg = 60;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.32625;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 0.8;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0099_POKEMON_KINGLER";
            metap.BaseDefense = 168;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 7.5;
            metap.CylHeightM = 1.0005;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.0005;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Krabby;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.MetalClawFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.WaterPulse,
                    PokemonMove.XScissor,
                    PokemonMove.ViceGrip
            };
            metap.Number = 99;
            Meta[PokemonId.Kingler] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0100_POKEMON_VOLTORB";
            metap.Family = PokemonFamilyId.FamilyVoltorb;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.3375;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 102;
            metap.DiskRadiusM = 0.5063;
            metap.CollisionRadiusM = 0.3375;
            metap.PokedexWeightKg = 10.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.16875;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 1.35;
            metap.UniqueId = "V0100_POKEMON_VOLTORB";
            metap.BaseDefense = 124;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1.3;
            metap.CylHeightM = 0.675;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.675;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SparkFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SignalBeam,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 100;
            Meta[PokemonId.Voltorb] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0101_POKEMON_ELECTRODE";
            metap.Family = PokemonFamilyId.FamilyVoltorb;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.552;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 150;
            metap.DiskRadiusM = 0.828;
            metap.CollisionRadiusM = 0.552;
            metap.PokedexWeightKg = 66.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.276;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.92;
            metap.UniqueId = "V0101_POKEMON_ELECTRODE";
            metap.BaseDefense = 174;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 8.325;
            metap.CylHeightM = 1.104;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.104;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Voltorb;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SparkFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.HyperBeam,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 101;
            Meta[PokemonId.Electrode] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0102_POKEMON_EXEGGCUTE";
            metap.Family = PokemonFamilyId.FamilyExeggcute;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.515;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 0.7725;
            metap.CollisionRadiusM = 0.515;
            metap.PokedexWeightKg = 2.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.2575;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.03;
            metap.UniqueId = "V0102_POKEMON_EXEGGCUTE";
            metap.BaseDefense = 132;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.3125;
            metap.CylHeightM = 0.412;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.412;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.SeedBomb,
                    PokemonMove.AncientPower
            };
            metap.Number = 102;
            Meta[PokemonId.Exeggcute] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0103_POKEMON_EXEGGUTOR";
            metap.Family = PokemonFamilyId.FamilyExeggcute;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 2;
            metap.HeightStdDev = 0.25;
            metap.BaseStamina = 190;
            metap.CylRadiusM = 0.507;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 232;
            metap.DiskRadiusM = 0.7605;
            metap.CollisionRadiusM = 0.507;
            metap.PokedexWeightKg = 120;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.2535;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.78;
            metap.UniqueId = "V0103_POKEMON_EXEGGUTOR";
            metap.BaseDefense = 164;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 15;
            metap.CylHeightM = 1.365;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.365;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Exeggcute;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.SeedBomb,
                    PokemonMove.SolarBeam
            };
            metap.Number = 103;
            Meta[PokemonId.Exeggutor] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0104_POKEMON_CUBONE";
            metap.Family = PokemonFamilyId.FamilyCubone;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.296;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 102;
            metap.DiskRadiusM = 0.444;
            metap.CollisionRadiusM = 0.222;
            metap.PokedexWeightKg = 6.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.222;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0104_POKEMON_CUBONE";
            metap.BaseDefense = 150;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.8125;
            metap.CylHeightM = 0.592;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.37;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.BoneClub,
                    PokemonMove.Bulldoze
            };
            metap.Number = 104;
            Meta[PokemonId.Cubone] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0105_POKEMON_MAROWAK";
            metap.Family = PokemonFamilyId.FamilyCubone;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.35;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 140;
            metap.DiskRadiusM = 0.525;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 45;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.25;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 0.85;
            metap.ModelScale = 1;
            metap.UniqueId = "V0105_POKEMON_MAROWAK";
            metap.BaseDefense = 202;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 5.625;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.75;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Cubone;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.Earthquake,
                    PokemonMove.BoneClub
            };
            metap.Number = 105;
            Meta[PokemonId.Marowak] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0106_POKEMON_HITMONLEE";
            metap.Family = PokemonFamilyId.FamilyHitmonlee;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.415;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 148;
            metap.DiskRadiusM = 0.6225;
            metap.CollisionRadiusM = 0.415;
            metap.PokedexWeightKg = 49.8;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.2075;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 0.8;
            metap.ModelScale = 0.83;
            metap.UniqueId = "V0106_POKEMON_HITMONLEE";
            metap.BaseDefense = 172;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 6.225;
            metap.CylHeightM = 1.245;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.245;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.LowKickFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Stomp,
                    PokemonMove.StoneEdge,
                    PokemonMove.LowSweep
            };
            metap.Number = 106;
            Meta[PokemonId.Hitmonlee] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0107_POKEMON_HITMONCHAN";
            metap.Family = PokemonFamilyId.FamilyHitmonchan;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 100;
            metap.CylRadiusM = 0.459;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 138;
            metap.DiskRadiusM = 0.6885;
            metap.CollisionRadiusM = 0.3315;
            metap.PokedexWeightKg = 50.2;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fighting;
            metap.CollisionHeadRadiusM = 0.255;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.1;
            metap.ModelScale = 1.02;
            metap.UniqueId = "V0107_POKEMON_HITMONCHAN";
            metap.BaseDefense = 204;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 6.275;
            metap.CylHeightM = 1.428;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.02;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BulletPunchFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.ThunderPunch,
                    PokemonMove.FirePunch,
                    PokemonMove.BrickBreak,
                    PokemonMove.IcePunch
            };
            metap.Number = 107;
            Meta[PokemonId.Hitmonchan] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0108_POKEMON_LICKITUNG";
            metap.Family = PokemonFamilyId.FamilyLickitung;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.46;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 126;
            metap.DiskRadiusM = 0.69;
            metap.CollisionRadiusM = 0.46;
            metap.PokedexWeightKg = 65.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.253;
            metap.MovementTimerS = 23;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.92;
            metap.UniqueId = "V0108_POKEMON_LICKITUNG";
            metap.BaseDefense = 160;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 8.1875;
            metap.CylHeightM = 1.104;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.92;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ZenHeadbuttFast,
                    PokemonMove.LickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Stomp,
                    PokemonMove.PowerWhip,
                    PokemonMove.HyperBeam
            };
            metap.Number = 108;
            Meta[PokemonId.Lickitung] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0109_POKEMON_KOFFING";
            metap.Family = PokemonFamilyId.FamilyKoffing;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.48;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 136;
            metap.DiskRadiusM = 0.72;
            metap.CollisionRadiusM = 0.36;
            metap.PokedexWeightKg = 1;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.6;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.2;
            metap.UniqueId = "V0109_POKEMON_KOFFING";
            metap.BaseDefense = 142;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.125;
            metap.CylHeightM = 0.72;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.66;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.6;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.DarkPulse,
                    PokemonMove.Sludge
            };
            metap.Number = 109;
            Meta[PokemonId.Koffing] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0110_POKEMON_WEEZING";
            metap.Family = PokemonFamilyId.FamilyKoffing;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.62;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 190;
            metap.DiskRadiusM = 0.93;
            metap.CollisionRadiusM = 0.682;
            metap.PokedexWeightKg = 9.5;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Poison;
            metap.CollisionHeadRadiusM = 0.465;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.24;
            metap.UniqueId = "V0110_POKEMON_WEEZING";
            metap.BaseDefense = 198;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 1.1875;
            metap.CylHeightM = 0.744;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.744;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Koffing;
            metap.CylGroundM = 0.62;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.AcidFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.SludgeBomb,
                    PokemonMove.ShadowBall,
                    PokemonMove.DarkPulse
            };
            metap.Number = 110;
            Meta[PokemonId.Weezing] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0111_POKEMON_RHYHORN";
            metap.Family = PokemonFamilyId.FamilyRhyhorn;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Rock;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.5;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 0.75;
            metap.CollisionRadiusM = 0.5;
            metap.PokedexWeightKg = 115;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.3;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1;
            metap.UniqueId = "V0111_POKEMON_RHYHORN";
            metap.BaseDefense = 116;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 14.375;
            metap.CylHeightM = 0.85;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.85;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Stomp,
                    PokemonMove.Bulldoze,
                    PokemonMove.HornAttack
            };
            metap.Number = 111;
            Meta[PokemonId.Rhyhorn] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0112_POKEMON_RHYDON";
            metap.Family = PokemonFamilyId.FamilyRhyhorn;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Rock;
            metap.PokedexHeightM = 1.9;
            metap.HeightStdDev = 0.2375;
            metap.BaseStamina = 210;
            metap.CylRadiusM = 0.79;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 166;
            metap.DiskRadiusM = 1.185;
            metap.CollisionRadiusM = 0.5925;
            metap.PokedexWeightKg = 120;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ground;
            metap.CollisionHeadRadiusM = 0.395;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.79;
            metap.UniqueId = "V0112_POKEMON_RHYDON";
            metap.BaseDefense = 160;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 15;
            metap.CylHeightM = 1.343;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.185;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Rhyhorn;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.Earthquake,
                    PokemonMove.Megahorn
            };
            metap.Number = 112;
            Meta[PokemonId.Rhydon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0113_POKEMON_CHANSEY";
            metap.Family = PokemonFamilyId.FamilyChansey;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 500;
            metap.CylRadiusM = 0.48;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 40;
            metap.DiskRadiusM = 0.72;
            metap.CollisionRadiusM = 0.48;
            metap.PokedexWeightKg = 34.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.24;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.96;
            metap.UniqueId = "V0113_POKEMON_CHANSEY";
            metap.BaseDefense = 60;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 4.325;
            metap.CylHeightM = 1.056;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.056;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.DazzlingGleam,
                    PokemonMove.Psybeam
            };
            metap.Number = 113;
            Meta[PokemonId.Chansey] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0114_POKEMON_TANGELA";
            metap.Family = PokemonFamilyId.FamilyTangela;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.73;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 164;
            metap.DiskRadiusM = 1.095;
            metap.CollisionRadiusM = 0.5;
            metap.PokedexWeightKg = 35;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Grass;
            metap.CollisionHeadRadiusM = 0.365;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1;
            metap.UniqueId = "V0114_POKEMON_TANGELA";
            metap.BaseDefense = 152;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 4.375;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.9;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.VineWhipFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PowerWhip,
                    PokemonMove.SludgeBomb,
                    PokemonMove.SolarBeam
            };
            metap.Number = 114;
            Meta[PokemonId.Tangela] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0115_POKEMON_KANGASKHAN";
            metap.Family = PokemonFamilyId.FamilyKangaskhan;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 2.2;
            metap.HeightStdDev = 0.275;
            metap.BaseStamina = 210;
            metap.CylRadiusM = 0.576;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 142;
            metap.DiskRadiusM = 0.864;
            metap.CollisionRadiusM = 0.504;
            metap.PokedexWeightKg = 80;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.36;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 0.7;
            metap.ModelScale = 0.72;
            metap.UniqueId = "V0115_POKEMON_KANGASKHAN";
            metap.BaseDefense = 178;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 10;
            metap.CylHeightM = 1.584;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.26;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudSlapFast,
                    PokemonMove.LowKickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Stomp,
                    PokemonMove.Earthquake,
                    PokemonMove.BrickBreak
            };
            metap.Number = 115;
            Meta[PokemonId.Kangaskhan] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0116_POKEMON_HORSEA";
            metap.Family = PokemonFamilyId.FamilyHorsea;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.25;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 122;
            metap.DiskRadiusM = 0.2775;
            metap.CollisionRadiusM = 0.148;
            metap.PokedexWeightKg = 8;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.185;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0116_POKEMON_HORSEA";
            metap.BaseDefense = 100;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1;
            metap.CylHeightM = 0.74;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.444;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.185;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast,
                    PokemonMove.BubbleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FlashCannon,
                    PokemonMove.BubbleBeam,
                    PokemonMove.DragonPulse
            };
            metap.Number = 116;
            Meta[PokemonId.Horsea] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0117_POKEMON_SEADRA";
            metap.Family = PokemonFamilyId.FamilyHorsea;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.2;
            metap.HeightStdDev = 0.15;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.46;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 176;
            metap.DiskRadiusM = 0.69;
            metap.CollisionRadiusM = 0.322;
            metap.PokedexWeightKg = 25;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.414;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.92;
            metap.UniqueId = "V0117_POKEMON_SEADRA";
            metap.BaseDefense = 150;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 3.125;
            metap.CylHeightM = 1.15;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.46;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Horsea;
            metap.CylGroundM = 0.46;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.DragonBreathFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Blizzard,
                    PokemonMove.HydroPump,
                    PokemonMove.DragonPulse
            };
            metap.Number = 117;
            Meta[PokemonId.Seadra] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0118_POKEMON_GOLDEEN";
            metap.Family = PokemonFamilyId.FamilyGoldeen;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.6;
            metap.HeightStdDev = 0.075;
            metap.BaseStamina = 90;
            metap.CylRadiusM = 0.27;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 112;
            metap.DiskRadiusM = 0.405;
            metap.CollisionRadiusM = 0.135;
            metap.PokedexWeightKg = 15;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.16875;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.35;
            metap.UniqueId = "V0118_POKEMON_GOLDEEN";
            metap.BaseDefense = 126;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 1.875;
            metap.CylHeightM = 0.3375;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.16875;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.3375;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.WaterPulse,
                    PokemonMove.HornAttack,
                    PokemonMove.AquaTail
            };
            metap.Number = 118;
            Meta[PokemonId.Goldeen] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0119_POKEMON_SEAKING";
            metap.Family = PokemonFamilyId.FamilyGoldeen;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.396;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 172;
            metap.DiskRadiusM = 0.594;
            metap.CollisionRadiusM = 0.044;
            metap.PokedexWeightKg = 39;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.242;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.88;
            metap.UniqueId = "V0119_POKEMON_SEAKING";
            metap.BaseDefense = 160;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 4.875;
            metap.CylHeightM = 0.748;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.044;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Goldeen;
            metap.CylGroundM = 0.33;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoisonJabFast,
                    PokemonMove.PeckFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.Megahorn,
                    PokemonMove.DrillRun
            };
            metap.Number = 119;
            Meta[PokemonId.Seaking] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0120_POKEMON_STARYU";
            metap.Family = PokemonFamilyId.FamilyStaryu;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.4125;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 130;
            metap.DiskRadiusM = 0.6188;
            metap.CollisionRadiusM = 0.4125;
            metap.PokedexWeightKg = 34.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.20625;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.35;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0120_POKEMON_STARYU";
            metap.BaseDefense = 128;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 4.3125;
            metap.CylHeightM = 0.88000011;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.88000011;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.4;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.PowerGem,
                    PokemonMove.BubbleBeam,
                    PokemonMove.Swift
            };
            metap.Number = 120;
            Meta[PokemonId.Staryu] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0121_POKEMON_STARMIE";
            metap.Family = PokemonFamilyId.FamilyStaryu;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.485;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 194;
            metap.DiskRadiusM = 0.7275;
            metap.CollisionRadiusM = 0.485;
            metap.PokedexWeightKg = 80;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.2425;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1.6;
            metap.ModelScale = 0.97;
            metap.UniqueId = "V0121_POKEMON_STARMIE";
            metap.BaseDefense = 192;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 10;
            metap.CylHeightM = 1.067;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.067;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Staryu;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psybeam,
                    PokemonMove.HydroPump,
                    PokemonMove.PowerGem
            };
            metap.Number = 121;
            Meta[PokemonId.Starmie] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0122_POKEMON_MR_MIME";
            metap.Family = PokemonFamilyId.FamilyMrMime;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Fairy;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 80;
            metap.CylRadiusM = 0.445;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 154;
            metap.DiskRadiusM = 0.6675;
            metap.CollisionRadiusM = 0.267;
            metap.PokedexWeightKg = 54.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.267;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.89;
            metap.UniqueId = "V0122_POKEMON_MR_MIME";
            metap.BaseDefense = 196;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 6.8125;
            metap.CylHeightM = 1.157;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.6675;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.ZenHeadbuttFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.ShadowBall,
                    PokemonMove.Psybeam
            };
            metap.Number = 122;
            Meta[PokemonId.MrMime] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0123_POKEMON_SCYTHER";
            metap.Family = PokemonFamilyId.FamilyScyther;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.76;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 176;
            metap.DiskRadiusM = 1.14;
            metap.CollisionRadiusM = 0.4;
            metap.PokedexWeightKg = 56;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.2;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.8;
            metap.UniqueId = "V0123_POKEMON_SCYTHER";
            metap.BaseDefense = 180;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 7;
            metap.CylHeightM = 1.2;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.4;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.FuryCutterFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.BugBuzz,
                    PokemonMove.XScissor,
                    PokemonMove.NightSlash
            };
            metap.Number = 123;
            Meta[PokemonId.Scyther] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0124_POKEMON_JYNX";
            metap.Family = PokemonFamilyId.FamilyJynx;
            metap.PokemonClass = PokemonClass.Common;
            metap.Type2 = PokemonType.Psychic;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.6525;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 172;
            metap.DiskRadiusM = 0.9788;
            metap.CollisionRadiusM = 0.435;
            metap.PokedexWeightKg = 40.6;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Ice;
            metap.CollisionHeadRadiusM = 0.522;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0124_POKEMON_JYNX";
            metap.BaseDefense = 134;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 5.075;
            metap.CylHeightM = 1.218;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.87;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast,
                    PokemonMove.FrostBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psyshock,
                    PokemonMove.DrainingKiss,
                    PokemonMove.IcePunch
            };
            metap.Number = 124;
            Meta[PokemonId.Jynx] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0125_POKEMON_ELECTABUZZ";
            metap.Family = PokemonFamilyId.FamilyElectabuzz;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.1;
            metap.HeightStdDev = 0.1375;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.5635;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 198;
            metap.DiskRadiusM = 0.8453;
            metap.CollisionRadiusM = 0.392;
            metap.PokedexWeightKg = 30;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.28175;
            metap.MovementTimerS = 6;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.98;
            metap.UniqueId = "V0125_POKEMON_ELECTABUZZ";
            metap.BaseDefense = 160;
            metap.AttackTimerS = 17;
            metap.WeightStdDev = 3.75;
            metap.CylHeightM = 0.98;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.735;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.LowKickFast,
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.ThunderPunch,
                    PokemonMove.Thunder,
                    PokemonMove.Thunderbolt
            };
            metap.Number = 125;
            Meta[PokemonId.Electabuzz] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0126_POKEMON_MAGMAR";
            metap.Family = PokemonFamilyId.FamilyMagmar;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.66;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 214;
            metap.DiskRadiusM = 0.99;
            metap.CollisionRadiusM = 0.44;
            metap.PokedexWeightKg = 44.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.33;
            metap.MovementTimerS = 14;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.88;
            metap.UniqueId = "V0126_POKEMON_MAGMAR";
            metap.BaseDefense = 158;
            metap.AttackTimerS = 5;
            metap.WeightStdDev = 5.5625;
            metap.CylHeightM = 1.144;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.88;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.KarateChopFast,
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.FirePunch,
                    PokemonMove.Flamethrower,
                    PokemonMove.FireBlast
            };
            metap.Number = 126;
            Meta[PokemonId.Magmar] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0127_POKEMON_PINSIR";
            metap.Family = PokemonFamilyId.FamilyPinsir;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.5;
            metap.HeightStdDev = 0.1875;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.348;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 184;
            metap.DiskRadiusM = 0.522;
            metap.CollisionRadiusM = 0.348;
            metap.PokedexWeightKg = 55;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Bug;
            metap.CollisionHeadRadiusM = 0.348;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0127_POKEMON_PINSIR";
            metap.BaseDefense = 186;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 6.875;
            metap.CylHeightM = 1.131;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.87;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.FuryCutterFast,
                    PokemonMove.RockSmashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Submission,
                    PokemonMove.XScissor,
                    PokemonMove.ViceGrip
            };
            metap.Number = 127;
            Meta[PokemonId.Pinsir] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0128_POKEMON_TAUROS";
            metap.Family = PokemonFamilyId.FamilyTauros;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.4;
            metap.HeightStdDev = 0.175;
            metap.BaseStamina = 150;
            metap.CylRadiusM = 0.5742;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 148;
            metap.DiskRadiusM = 0.8613;
            metap.CollisionRadiusM = 0.435;
            metap.PokedexWeightKg = 88.4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.2871;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0128_POKEMON_TAUROS";
            metap.BaseDefense = 184;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 11.05;
            metap.CylHeightM = 1.19625;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.19625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.24;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ZenHeadbuttFast,
                    PokemonMove.TackleFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IronHead,
                    PokemonMove.Earthquake,
                    PokemonMove.HornAttack
            };
            metap.Number = 128;
            Meta[PokemonId.Tauros] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0129_POKEMON_MAGIKARP";
            metap.Family = PokemonFamilyId.FamilyMagikarp;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 40;
            metap.CylRadiusM = 0.428;
            metap.BaseFleeRate = 0.15;
            metap.BaseAttack = 42;
            metap.DiskRadiusM = 0.642;
            metap.CollisionRadiusM = 0.2675;
            metap.PokedexWeightKg = 10;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.321;
            metap.MovementTimerS = 3600;
            metap.JumpTimeS = 1.3;
            metap.ModelScale = 1.07;
            metap.UniqueId = "V0129_POKEMON_MAGIKARP";
            metap.BaseDefense = 84;
            metap.AttackTimerS = 3600;
            metap.WeightStdDev = 1.25;
            metap.CylHeightM = 0.535;
            metap.CandyToEvolve = 400;
            metap.CollisionHeightM = 0.4815;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.56;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SplashFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 129;
            Meta[PokemonId.Magikarp] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0130_POKEMON_GYARADOS";
            metap.Family = PokemonFamilyId.FamilyMagikarp;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 6.5;
            metap.HeightStdDev = 0.8125;
            metap.BaseStamina = 190;
            metap.CylRadiusM = 0.48;
            metap.BaseFleeRate = 0.07;
            metap.BaseAttack = 192;
            metap.DiskRadiusM = 0.72;
            metap.CollisionRadiusM = 0.24;
            metap.PokedexWeightKg = 235;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.36;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.48;
            metap.UniqueId = "V0130_POKEMON_GYARADOS";
            metap.BaseDefense = 196;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 29.375;
            metap.CylHeightM = 1.2;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.48;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Magikarp;
            metap.CylGroundM = 0.48;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.DragonBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.HydroPump,
                    PokemonMove.DragonPulse
            };
            metap.Number = 130;
            Meta[PokemonId.Gyarados] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0131_POKEMON_LAPRAS";
            metap.Family = PokemonFamilyId.FamilyLapras;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Ice;
            metap.PokedexHeightM = 2.5;
            metap.HeightStdDev = 0.3125;
            metap.BaseStamina = 260;
            metap.CylRadiusM = 0.7;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 186;
            metap.DiskRadiusM = 1.05;
            metap.CollisionRadiusM = 0.525;
            metap.PokedexWeightKg = 220;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.35;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.7;
            metap.UniqueId = "V0131_POKEMON_LAPRAS";
            metap.BaseDefense = 190;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 27.5;
            metap.CylHeightM = 1.75;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.7;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.IceShardFast,
                    PokemonMove.FrostBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Blizzard,
                    PokemonMove.IceBeam,
                    PokemonMove.DragonPulse
            };
            metap.Number = 131;
            Meta[PokemonId.Lapras] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0132_POKEMON_DITTO";
            metap.Family = PokemonFamilyId.FamilyDitto;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 96;
            metap.CylRadiusM = 0.4025;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 110;
            metap.DiskRadiusM = 0.6038;
            metap.CollisionRadiusM = 0.4025;
            metap.PokedexWeightKg = 4;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.20125;
            metap.MovementTimerS = 3600;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.61;
            metap.UniqueId = "V0132_POKEMON_DITTO";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 3600;
            metap.WeightStdDev = 0.5;
            metap.CylHeightM = 0.52325;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.52325;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Struggle
            };
            metap.Number = 132;
            Meta[PokemonId.Ditto] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0133_POKEMON_EEVEE";
            metap.Family = PokemonFamilyId.FamilyEevee;
            metap.PokemonClass = PokemonClass.VeryCommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.3;
            metap.HeightStdDev = 0.0375;
            metap.BaseStamina = 110;
            metap.CylRadiusM = 0.42;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 114;
            metap.DiskRadiusM = 0.63;
            metap.CollisionRadiusM = 0.252;
            metap.PokedexWeightKg = 6.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.252;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 1.35;
            metap.ModelScale = 1.68;
            metap.UniqueId = "V0133_POKEMON_EEVEE";
            metap.BaseDefense = 128;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.8125;
            metap.CylHeightM = 0.504;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.336;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.TackleFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Dig,
                    PokemonMove.Swift,
                    PokemonMove.BodySlam
            };
            metap.Number = 133;
            Meta[PokemonId.Eevee] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0134_POKEMON_VAPOREON";
            metap.Family = PokemonFamilyId.FamilyEevee;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 260;
            metap.CylRadiusM = 0.3465;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 186;
            metap.DiskRadiusM = 0.5198;
            metap.CollisionRadiusM = 0.21;
            metap.PokedexWeightKg = 29;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Water;
            metap.CollisionHeadRadiusM = 0.2625;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.05;
            metap.UniqueId = "V0134_POKEMON_VAPOREON";
            metap.BaseDefense = 168;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 3.625;
            metap.CylHeightM = 0.94499987;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.525;
            metap.ShoulderModeScale = 0.4;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Eevee;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.WaterPulse,
                    PokemonMove.HydroPump,
                    PokemonMove.AquaTail
            };
            metap.Number = 134;
            Meta[PokemonId.Vaporeon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0135_POKEMON_JOLTEON";
            metap.Family = PokemonFamilyId.FamilyEevee;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.33;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 192;
            metap.DiskRadiusM = 0.495;
            metap.CollisionRadiusM = 0.22;
            metap.PokedexWeightKg = 24.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.22;
            metap.MovementTimerS = 4;
            metap.JumpTimeS = 1.3;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0135_POKEMON_JOLTEON";
            metap.BaseDefense = 174;
            metap.AttackTimerS = 11;
            metap.WeightStdDev = 3.0625;
            metap.CylHeightM = 0.88000011;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.55;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Eevee;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Thunder,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 135;
            Meta[PokemonId.Jolteon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0136_POKEMON_FLAREON";
            metap.Family = PokemonFamilyId.FamilyEevee;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.9;
            metap.HeightStdDev = 0.1125;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.3045;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 238;
            metap.DiskRadiusM = 0.4568;
            metap.CollisionRadiusM = 0.2175;
            metap.PokedexWeightKg = 25;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.19575;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1.35;
            metap.ModelScale = 0.87;
            metap.UniqueId = "V0136_POKEMON_FLAREON";
            metap.BaseDefense = 178;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 3.125;
            metap.CylHeightM = 0.783;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.522;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Eevee;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Flamethrower,
                    PokemonMove.HeatWave,
                    PokemonMove.FireBlast
            };
            metap.Number = 136;
            Meta[PokemonId.Flareon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0137_POKEMON_PORYGON";
            metap.Family = PokemonFamilyId.FamilyPorygon;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.8;
            metap.HeightStdDev = 0.1;
            metap.BaseStamina = 130;
            metap.CylRadiusM = 0.55;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 156;
            metap.DiskRadiusM = 0.825;
            metap.CollisionRadiusM = 0.385;
            metap.PokedexWeightKg = 36.5;
            metap.MovementType = MovementType.Hovering;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.33;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.1;
            metap.UniqueId = "V0137_POKEMON_PORYGON";
            metap.BaseDefense = 158;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 4.5625;
            metap.CylHeightM = 0.93500012;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.55;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.55;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.TackleFast,
                    PokemonMove.QuickAttackFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Discharge,
                    PokemonMove.Psybeam,
                    PokemonMove.SignalBeam
            };
            metap.Number = 137;
            Meta[PokemonId.Porygon] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0138_POKEMON_OMANYTE";
            metap.Family = PokemonFamilyId.FamilyOmanyte;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Water;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 70;
            metap.CylRadiusM = 0.222;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 132;
            metap.DiskRadiusM = 0.333;
            metap.CollisionRadiusM = 0.222;
            metap.PokedexWeightKg = 7.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.111;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.3;
            metap.ModelScale = 1.48;
            metap.UniqueId = "V0138_POKEMON_OMANYTE";
            metap.BaseDefense = 160;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 0.9375;
            metap.CylHeightM = 0.592;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.592;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.RockTomb,
                    PokemonMove.AncientPower,
                    PokemonMove.Brine
            };
            metap.Number = 138;
            Meta[PokemonId.Omanyte] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0139_POKEMON_OMASTAR";
            metap.Family = PokemonFamilyId.FamilyOmanyte;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Water;
            metap.PokedexHeightM = 1;
            metap.HeightStdDev = 0.125;
            metap.BaseStamina = 140;
            metap.CylRadiusM = 0.375;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 180;
            metap.DiskRadiusM = 0.5625;
            metap.CollisionRadiusM = 0.25;
            metap.PokedexWeightKg = 35;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.1875;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 1;
            metap.UniqueId = "V0139_POKEMON_OMASTAR";
            metap.BaseDefense = 202;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 4.375;
            metap.CylHeightM = 1;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.9;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Omanyte;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.RockThrowFast,
                    PokemonMove.WaterGunFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.HydroPump,
                    PokemonMove.AncientPower,
                    PokemonMove.RockSlide
            };
            metap.Number = 139;
            Meta[PokemonId.Omastar] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0140_POKEMON_KABUTO";
            metap.Family = PokemonFamilyId.FamilyKabuto;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.Water;
            metap.PokedexHeightM = 0.5;
            metap.HeightStdDev = 0.0625;
            metap.BaseStamina = 60;
            metap.CylRadiusM = 0.3375;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 148;
            metap.DiskRadiusM = 0.5063;
            metap.CollisionRadiusM = 0.3375;
            metap.PokedexWeightKg = 11.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.16875;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 0.9;
            metap.ModelScale = 1.35;
            metap.UniqueId = "V0140_POKEMON_KABUTO";
            metap.BaseDefense = 142;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 1.4375;
            metap.CylHeightM = 0.50625;
            metap.CandyToEvolve = 50;
            metap.CollisionHeightM = 0.50625;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.ScratchFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.AncientPower,
                    PokemonMove.AquaJet,
                    PokemonMove.RockTomb
            };
            metap.Number = 140;
            Meta[PokemonId.Kabuto] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0141_POKEMON_KABUTOPS";
            metap.Family = PokemonFamilyId.FamilyKabuto;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.Water;
            metap.PokedexHeightM = 1.3;
            metap.HeightStdDev = 0.1625;
            metap.BaseStamina = 120;
            metap.CylRadiusM = 0.455;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 190;
            metap.DiskRadiusM = 0.6825;
            metap.CollisionRadiusM = 0.364;
            metap.PokedexWeightKg = 40.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.3185;
            metap.MovementTimerS = 11;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.91;
            metap.UniqueId = "V0141_POKEMON_KABUTOPS";
            metap.BaseDefense = 190;
            metap.AttackTimerS = 4;
            metap.WeightStdDev = 5.0625;
            metap.CylHeightM = 1.1375;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.91;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.12;
            metap.ParentId = PokemonId.Kabuto;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.MudShotFast,
                    PokemonMove.FuryCutterFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.StoneEdge,
                    PokemonMove.WaterPulse,
                    PokemonMove.AncientPower
            };
            metap.Number = 141;
            Meta[PokemonId.Kabutops] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0142_POKEMON_AERODACTYL";
            metap.Family = PokemonFamilyId.FamilyAerodactyl;
            metap.PokemonClass = PokemonClass.VeryRare;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.8;
            metap.HeightStdDev = 0.225;
            metap.BaseStamina = 160;
            metap.CylRadiusM = 0.399;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 182;
            metap.DiskRadiusM = 0.5985;
            metap.CollisionRadiusM = 0.285;
            metap.PokedexWeightKg = 59;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Rock;
            metap.CollisionHeadRadiusM = 0.285;
            metap.MovementTimerS = 5;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.57;
            metap.UniqueId = "V0142_POKEMON_AERODACTYL";
            metap.BaseDefense = 162;
            metap.AttackTimerS = 14;
            metap.WeightStdDev = 7.375;
            metap.CylHeightM = 0.9975;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.9975;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.855;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.BiteFast,
                    PokemonMove.SteelWingFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IronHead,
                    PokemonMove.HyperBeam,
                    PokemonMove.AncientPower
            };
            metap.Number = 142;
            Meta[PokemonId.Aerodactyl] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0143_POKEMON_SNORLAX";
            metap.Family = PokemonFamilyId.FamilySnorlax;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 2.1;
            metap.HeightStdDev = 0.2625;
            metap.BaseStamina = 320;
            metap.CylRadiusM = 0.74;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 180;
            metap.DiskRadiusM = 1.11;
            metap.CollisionRadiusM = 0.74;
            metap.PokedexWeightKg = 460;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Normal;
            metap.CollisionHeadRadiusM = 0.481;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.74;
            metap.UniqueId = "V0143_POKEMON_SNORLAX";
            metap.BaseDefense = 180;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 57.5;
            metap.CylHeightM = 1.48;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.11;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.16;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ZenHeadbuttFast,
                    PokemonMove.LickFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Earthquake,
                    PokemonMove.HyperBeam,
                    PokemonMove.BodySlam
            };
            metap.Number = 143;
            Meta[PokemonId.Snorlax] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0144_POKEMON_ARTICUNO";
            metap.Family = PokemonFamilyId.FamilyArticuno;
            metap.PokemonClass = PokemonClass.Legendary;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.7;
            metap.HeightStdDev = 0.2125;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.396;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 198;
            metap.DiskRadiusM = 0.594;
            metap.CollisionRadiusM = 0.231;
            metap.PokedexWeightKg = 55.4;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Ice;
            metap.CollisionHeadRadiusM = 0.231;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.66;
            metap.UniqueId = "V0144_POKEMON_ARTICUNO";
            metap.BaseDefense = 242;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 6.925;
            metap.CylHeightM = 0.99;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.66;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.66;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.FrostBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.IcyWind,
                    PokemonMove.Blizzard,
                    PokemonMove.IceBeam
            };
            metap.Number = 144;
            Meta[PokemonId.Articuno] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0145_POKEMON_ZAPDOS";
            metap.Family = PokemonFamilyId.FamilyZapdos;
            metap.PokemonClass = PokemonClass.Legendary;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 1.6;
            metap.HeightStdDev = 0.2;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.5175;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 232;
            metap.DiskRadiusM = 0.7763;
            metap.CollisionRadiusM = 0.4485;
            metap.PokedexWeightKg = 52.6;
            metap.MovementType = MovementType.Electric;
            metap.Type1 = PokemonType.Electric;
            metap.CollisionHeadRadiusM = 0.276;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.69;
            metap.UniqueId = "V0145_POKEMON_ZAPDOS";
            metap.BaseDefense = 194;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 6.575;
            metap.CylHeightM = 1.035;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.759;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.8625;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ThunderShockFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Thunder,
                    PokemonMove.Thunderbolt,
                    PokemonMove.Discharge
            };
            metap.Number = 145;
            Meta[PokemonId.Zapdos] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0146_POKEMON_MOLTRES";
            metap.Family = PokemonFamilyId.FamilyMoltres;
            metap.PokemonClass = PokemonClass.Legendary;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 2;
            metap.HeightStdDev = 0.25;
            metap.BaseStamina = 180;
            metap.CylRadiusM = 0.62;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 242;
            metap.DiskRadiusM = 0.93;
            metap.CollisionRadiusM = 0.403;
            metap.PokedexWeightKg = 60;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Fire;
            metap.CollisionHeadRadiusM = 0.217;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.62;
            metap.UniqueId = "V0146_POKEMON_MOLTRES";
            metap.BaseDefense = 194;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 7.5;
            metap.CylHeightM = 1.395;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.93;
            metap.ShoulderModeScale = 0.25;
            metap.BaseCaptureRate = 0.00;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.93;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.EmberFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Flamethrower,
                    PokemonMove.HeatWave,
                    PokemonMove.FireBlast
            };
            metap.Number = 146;
            Meta[PokemonId.Moltres] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0147_POKEMON_DRATINI";
            metap.Family = PokemonFamilyId.FamilyDratini;
            metap.PokemonClass = PokemonClass.Uncommon;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 1.8;
            metap.HeightStdDev = 0.225;
            metap.BaseStamina = 82;
            metap.CylRadiusM = 0.2775;
            metap.BaseFleeRate = 0.09;
            metap.BaseAttack = 128;
            metap.DiskRadiusM = 0.4163;
            metap.CollisionRadiusM = 0.2775;
            metap.PokedexWeightKg = 3.3;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Dragon;
            metap.CollisionHeadRadiusM = 0.19425;
            metap.MovementTimerS = 10;
            metap.JumpTimeS = 0.85;
            metap.ModelScale = 1.11;
            metap.UniqueId = "V0147_POKEMON_DRATINI";
            metap.BaseDefense = 110;
            metap.AttackTimerS = 29;
            metap.WeightStdDev = 0.4125;
            metap.CylHeightM = 0.8325;
            metap.CandyToEvolve = 25;
            metap.CollisionHeightM = 0.555;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.32;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.DragonBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Twister,
                    PokemonMove.Wrap,
                    PokemonMove.AquaTail
            };
            metap.Number = 147;
            Meta[PokemonId.Dratini] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0148_POKEMON_DRAGONAIR";
            metap.Family = PokemonFamilyId.FamilyDratini;
            metap.PokemonClass = PokemonClass.Rare;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 4;
            metap.HeightStdDev = 0.5;
            metap.BaseStamina = 122;
            metap.CylRadiusM = 0.5625;
            metap.BaseFleeRate = 0.06;
            metap.BaseAttack = 170;
            metap.DiskRadiusM = 0.8438;
            metap.CollisionRadiusM = 0.375;
            metap.PokedexWeightKg = 16.5;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Dragon;
            metap.CollisionHeadRadiusM = 0.28125;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.25;
            metap.ModelScale = 0.75;
            metap.UniqueId = "V0148_POKEMON_DRAGONAIR";
            metap.BaseDefense = 152;
            metap.AttackTimerS = 23;
            metap.WeightStdDev = 2.0625;
            metap.CylHeightM = 1.5;
            metap.CandyToEvolve = 100;
            metap.CollisionHeightM = 1.125;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.08;
            metap.ParentId = PokemonId.Dratini;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.DragonBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Wrap,
                    PokemonMove.AquaTail,
                    PokemonMove.DragonPulse
            };
            metap.Number = 148;
            Meta[PokemonId.Dragonair] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0149_POKEMON_DRAGONITE";
            metap.Family = PokemonFamilyId.FamilyDratini;
            metap.PokemonClass = PokemonClass.Epic;
            metap.Type2 = PokemonType.Flying;
            metap.PokedexHeightM = 2.2;
            metap.HeightStdDev = 0.275;
            metap.BaseStamina = 182;
            metap.CylRadiusM = 0.42;
            metap.BaseFleeRate = 0.05;
            metap.BaseAttack = 250;
            metap.DiskRadiusM = 0.63;
            metap.CollisionRadiusM = 0.42;
            metap.PokedexWeightKg = 210;
            metap.MovementType = MovementType.Flying;
            metap.Type1 = PokemonType.Dragon;
            metap.CollisionHeadRadiusM = 0.245;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 0.7;
            metap.UniqueId = "V0149_POKEMON_DRAGONITE";
            metap.BaseDefense = 212;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 26.25;
            metap.CylHeightM = 1.47;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.05;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0.04;
            metap.ParentId = PokemonId.Dragonair;
            metap.CylGroundM = 0.595;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.SteelWingFast,
                    PokemonMove.DragonBreathFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.DragonClaw,
                    PokemonMove.HyperBeam,
                    PokemonMove.DragonPulse
            };
            metap.Number = 149;
            Meta[PokemonId.Dragonite] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0150_POKEMON_MEWTWO";
            metap.Family = PokemonFamilyId.FamilyMewtwo;
            metap.PokemonClass = PokemonClass.Legendary;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 2;
            metap.HeightStdDev = 0.25;
            metap.BaseStamina = 212;
            metap.CylRadiusM = 0.37;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 284;
            metap.DiskRadiusM = 0.555;
            metap.CollisionRadiusM = 0.37;
            metap.PokedexWeightKg = 122;
            metap.MovementType = MovementType.Jump;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.185;
            metap.MovementTimerS = 8;
            metap.JumpTimeS = 1.2;
            metap.ModelScale = 0.74;
            metap.UniqueId = "V0150_POKEMON_MEWTWO";
            metap.BaseDefense = 202;
            metap.AttackTimerS = 3;
            metap.WeightStdDev = 15.25;
            metap.CylHeightM = 1.48;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 1.184;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.ConfusionFast,
                    PokemonMove.PsychoCutFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Psychic,
                    PokemonMove.ShadowBall,
                    PokemonMove.HyperBeam
            };
            metap.Number = 150;
            Meta[PokemonId.Mewtwo] = metap;

            metap = new PokemonMeta();
            metap.TemplateId = " V0151_POKEMON_MEW";
            metap.Family = PokemonFamilyId.FamilyMewtwo;
            metap.PokemonClass = PokemonClass.Mythic;
            metap.Type2 = PokemonType.None;
            metap.PokedexHeightM = 0.4;
            metap.HeightStdDev = 0.05;
            metap.BaseStamina = 200;
            metap.CylRadiusM = 0.282;
            metap.BaseFleeRate = 0.1;
            metap.BaseAttack = 220;
            metap.DiskRadiusM = 0.423;
            metap.CollisionRadiusM = 0.141;
            metap.PokedexWeightKg = 4;
            metap.MovementType = MovementType.Psychic;
            metap.Type1 = PokemonType.Psychic;
            metap.CollisionHeadRadiusM = 0.17625;
            metap.MovementTimerS = 3;
            metap.JumpTimeS = 1;
            metap.ModelScale = 1.41;
            metap.UniqueId = "V0151_POKEMON_MEW";
            metap.BaseDefense = 220;
            metap.AttackTimerS = 8;
            metap.WeightStdDev = 0.5;
            metap.CylHeightM = 0.7755;
            metap.CandyToEvolve = 0;
            metap.CollisionHeightM = 0.564;
            metap.ShoulderModeScale = 0.5;
            metap.BaseCaptureRate = 0;
            metap.ParentId = PokemonId.Missingno;
            metap.CylGroundM = 0.0705;
            metap.QuickMoves = new PokemonMove[]{
                    PokemonMove.PoundFast
            };
            metap.CinematicMoves = new PokemonMove[]{
                    PokemonMove.Moonblast,
                    PokemonMove.FireBlast,
                    PokemonMove.SolarBeam,
                    PokemonMove.HyperBeam,
                    PokemonMove.Psychic,
                    PokemonMove.Hurricane,
                    PokemonMove.Earthquake,
                    PokemonMove.DragonPulse,
                    PokemonMove.Thunder
            };
            metap.Number = 151;
            Meta[PokemonId.Mew] = metap;

        }

        /**
	     * Return PokemonMeta object containing meta info about a pokemon.
	     *
	     * @param id the id of the pokemon
	     * @return PokemonMeta
	     */
        public static PokemonMeta GetMeta(PokemonId id)
        {
            return Meta[id];
        }

        /**
         * Return the highest evolution for given family ID.
         * !!! CARE TO EVEE THAT DOESNT HAVE BETTER EVOLUTION !!!
         *
         * @param family the id of the pokemon family
         * @return PokemonId
         */
        public static PokemonId GetHighestForFamily(PokemonFamilyId family)
        {
            return HighestForFamily[family];
        }

    }

}
