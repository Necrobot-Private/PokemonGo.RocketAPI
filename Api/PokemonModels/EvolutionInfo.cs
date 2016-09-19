using POGOProtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.PokemonModels
{
    class EvolutionInfo
    {
        private static PokemonId[] BULBASAUR_EVOLUTION = {PokemonId.Bulbasaur, PokemonId.Ivysaur, PokemonId.Venusaur};
        private static PokemonId[] CHARMANDER_EVOLUTION = {PokemonId.Charmander, PokemonId.Charmeleon, PokemonId.Charizard};
        private static PokemonId[] SQUIRTLE_EVOLUTION = {PokemonId.Squirtle, PokemonId.Wartortle, PokemonId.Blastoise};
	    private static PokemonId[] CATERPIE_EVOLUTION = {PokemonId.Caterpie, PokemonId.Metapod, PokemonId.Butterfree};
	    private static PokemonId[] WEEDLE_EVOLUTION = {PokemonId.Weedle, PokemonId.Kakuna, PokemonId.Beedrill};
	    private static PokemonId[] PIDGEY_EVOLUTION = {PokemonId.Pidgey, PokemonId.Pidgeotto, PokemonId.Pidgeot};
	    private static PokemonId[] RATTATA_EVOLUTION = {PokemonId.Rattata, PokemonId.Raticate};
	    private static PokemonId[] SPEAROW_EVOLUTION = {PokemonId.Spearow, PokemonId.Fearow};
	    private static PokemonId[] EKANS_EVOLUTION = {PokemonId.Ekans, PokemonId.Arbok};
	    private static PokemonId[] PIKACHU_EVOLUTION = {PokemonId.Pikachu, PokemonId.Raichu};
	    private static PokemonId[] SANDSHREW_EVOLUTION = {PokemonId.Sandshrew, PokemonId.Sandslash};
	    private static PokemonId[] NIDORAN_FEMALE_EVOLUTION = {PokemonId.NidoranFemale, PokemonId.Nidorina, PokemonId.Nidoqueen};
	    private static PokemonId[] NIDORAN_MALE_EVOLUTION = {PokemonId.NidoranMale, PokemonId.Nidorino, PokemonId.Nidoking};
	    private static PokemonId[] CLEFAIRY_EVOLUTION = {PokemonId.Clefairy, PokemonId.Clefable};
	    private static PokemonId[] VULPIX_EVOLUTION = {PokemonId.Vulpix, PokemonId.Ninetales};
	    private static PokemonId[] JIGGLYPUFF_EVOLUTION = {PokemonId.Jigglypuff, PokemonId.Wigglytuff};
	    private static PokemonId[] ZUBAT_EVOLUTION = {PokemonId.Zubat, PokemonId.Golbat};
	    private static PokemonId[] ODDISH_EVOLUTION = {PokemonId.Oddish, PokemonId.Gloom, PokemonId.Vileplume};
	    private static PokemonId[] PARAS_EVOLUTION = {PokemonId.Paras, PokemonId.Parasect};
	    private static PokemonId[] VENONAT_EVOLUTION = {PokemonId.Venonat, PokemonId.Venomoth};
	    private static PokemonId[] DIGLETT_EVOLUTION = {PokemonId.Diglett, PokemonId.Dugtrio};
	    private static PokemonId[] MEOWTH_EVOLUTION = {PokemonId.Meowth, PokemonId.Persian};
	    private static PokemonId[] PSYDUCK_EVOLUTION = {PokemonId.Psyduck, PokemonId.Golduck};
	    private static PokemonId[] MANKEY_EVOLUTION = {PokemonId.Mankey, PokemonId.Primeape};
	    private static PokemonId[] GROWLITHE_EVOLUTION = {PokemonId.Growlithe, PokemonId.Arcanine};
	    private static PokemonId[] POLIWAG_EVOLUTION = {PokemonId.Poliwag, PokemonId.Poliwhirl, PokemonId.Poliwrath};
	    private static PokemonId[] ABRA_EVOLUTION = {PokemonId.Abra, PokemonId.Kadabra, PokemonId.Alakazam};
	    private static PokemonId[] MACHOP_EVOLUTION = {PokemonId.Machop, PokemonId.Machoke, PokemonId.Machamp};
	    private static PokemonId[] BELLSPROUT_EVOLUTION = {PokemonId.Bellsprout, PokemonId.Weepinbell, PokemonId.Victreebel};
	    private static PokemonId[] TENTACOOL_EVOLUTION = {PokemonId.Tentacool, PokemonId.Tentacruel};
	    private static PokemonId[] GEODUDE_EVOLUTION = {PokemonId.Geodude, PokemonId.Graveler, PokemonId.Golem};
	    private static PokemonId[] PONYTA_EVOLUTION = {PokemonId.Ponyta, PokemonId.Rapidash};
	    private static PokemonId[] SLOWPOKE_EVOLUTION = {PokemonId.Slowpoke, PokemonId.Slowbro};
	    private static PokemonId[] MAGNEMITE_EVOLUTION = {PokemonId.Magnemite, PokemonId.Magneton};
	    private static PokemonId[] FARFETCHD_EVOLUTION = {PokemonId.Farfetchd};
	    private static PokemonId[] DODUO_EVOLUTION = {PokemonId.Doduo, PokemonId.Dodrio};
	    private static PokemonId[] SEEL_EVOLUTION = {PokemonId.Seel, PokemonId.Dewgong};
	    private static PokemonId[] GRIMER_EVOLUTION = {PokemonId.Grimer, PokemonId.Muk};
	    private static PokemonId[] SHELLDER_EVOLUTION = {PokemonId.Shellder, PokemonId.Cloyster};
	    private static PokemonId[] GASTLY_EVOLUTION = {PokemonId.Gastly, PokemonId.Haunter, PokemonId.Gengar};
	    private static PokemonId[] ONIX_EVOLUTION = {PokemonId.Onix};
	    private static PokemonId[] DROWZEE_EVOLUTION = {PokemonId.Drowzee, PokemonId.Hypno};
	    private static PokemonId[] KRABBY_EVOLUTION = {PokemonId.Krabby, PokemonId.Kingler};
	    private static PokemonId[] VOLTORB_EVOLUTION = {PokemonId.Voltorb, PokemonId.Electrode};
	    private static PokemonId[] EXEGGCUTE_EVOLUTION = {PokemonId.Exeggcute, PokemonId.Exeggutor};
	    private static PokemonId[] CUBONE_EVOLUTION = {PokemonId.Cubone, PokemonId.Marowak};
	    private static PokemonId[] HITMONLEE_EVOLUTION = {PokemonId.Hitmonlee, PokemonId.Hitmonchan};
	    private static PokemonId[] LICKITUNG_EVOLUTION = {PokemonId.Lickitung};
	    private static PokemonId[] KOFFING_EVOLUTION = {PokemonId.Koffing, PokemonId.Weezing};
	    private static PokemonId[] RHYHORN_EVOLUTION = {PokemonId.Rhyhorn, PokemonId.Rhydon};
	    private static PokemonId[] CHANSEY_EVOLUTION = {PokemonId.Chansey};
	    private static PokemonId[] TANGELA_EVOLUTION = {PokemonId.Tangela};
	    private static PokemonId[] KANGASKHAN_EVOLUTION = {PokemonId.Kangaskhan};
	    private static PokemonId[] HORSEA_EVOLUTION = {PokemonId.Horsea, PokemonId.Seadra};
	    private static PokemonId[] GOLDEEN_EVOLUTION = {PokemonId.Goldeen, PokemonId.Seaking};
	    private static PokemonId[] STARYU_EVOLUTION = {PokemonId.Staryu, PokemonId.Starmie};
	    private static PokemonId[] MR_MIME_EVOLUTION = {PokemonId.MrMime};
	    private static PokemonId[] SCYTHER_EVOLUTION = {PokemonId.Scyther};
	    private static PokemonId[] JYNX_EVOLUTION = {PokemonId.Jynx};
	    private static PokemonId[] ELECTABUZZ_EVOLUTION = {PokemonId.Electabuzz};
	    private static PokemonId[] MAGMAR_EVOLUTION = {PokemonId.Magmar};
	    private static PokemonId[] PINSIR_EVOLUTION = {PokemonId.Pinsir};
	    private static PokemonId[] TAUROS_EVOLUTION = {PokemonId.Tauros};
	    private static PokemonId[] MAGIKARP_EVOLUTION = {PokemonId.Magikarp, PokemonId.Gyarados};
	    private static PokemonId[] LAPRAS_EVOLUTION = {PokemonId.Lapras};
	    private static PokemonId[] DITTO_EVOLUTION = {PokemonId.Ditto};

	    // needs to be handled exceptionally
	    private static PokemonId[] EEVEE_EVOLUTION = {PokemonId.Eevee, PokemonId.Vaporeon, PokemonId.Jolteon, PokemonId.Flareon };
	    private static List<PokemonId> EEVEE_FINAL_EVOLUTIONS = new List<PokemonId>(new PokemonId[] { PokemonId.Vaporeon, PokemonId.Jolteon, PokemonId.Flareon });

        private static PokemonId[] PORYGON_EVOLUTION = {PokemonId.Porygon};
	    private static PokemonId[] OMANYTE_EVOLUTION = {PokemonId.Omanyte, PokemonId.Omastar};
	    private static PokemonId[] KABUTO_EVOLUTION = {PokemonId.Kabuto, PokemonId.Kabutops};
	    private static PokemonId[] AERODACTYL_EVOLUTION = {PokemonId.Aerodactyl};
	    private static PokemonId[] SNORLAX_EVOLUTION = {PokemonId.Snorlax};
	    private static PokemonId[] ARTICUNO_EVOLUTION = {PokemonId.Articuno};
	    private static PokemonId[] ZAPDOS_EVOLUTION = {PokemonId.Zapdos};
	    private static PokemonId[] MOLTRES_EVOLUTION = {PokemonId.Moltres};
	    private static PokemonId[] DRATINI_EVOLUTION = {PokemonId.Dratini, PokemonId.Dragonair, PokemonId.Dragonite};
	    private static PokemonId[] MEWTWO_EVOLUTION = {PokemonId.Mewtwo};
	    private static PokemonId[] MEW_EVOLUTION = {PokemonId.Mew};

        private static Dictionary<PokemonId, PokemonId[]> EVOLUTION_INFO = new Dictionary<PokemonId, PokemonId[]>();

        static EvolutionInfo()
        {
		    EVOLUTION_INFO[PokemonId.Bulbasaur] = BULBASAUR_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Ivysaur] = BULBASAUR_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Venusaur] = BULBASAUR_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Charmander] = CHARMANDER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Charmeleon] = CHARMANDER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Charizard] = CHARMANDER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Squirtle] = SQUIRTLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Wartortle] = SQUIRTLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Blastoise] = SQUIRTLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Caterpie] = CATERPIE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Metapod] = CATERPIE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Butterfree] = CATERPIE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Weedle] = WEEDLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kakuna] = WEEDLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Beedrill] = WEEDLE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Pidgey] = PIDGEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Pidgeotto] = PIDGEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Pidgeot] = PIDGEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Rattata] = RATTATA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Raticate] = RATTATA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Spearow] = SPEAROW_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Fearow] = SPEAROW_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Ekans] = EKANS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Arbok] = EKANS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Pikachu] = PIKACHU_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Raichu] = PIKACHU_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Sandshrew] = SANDSHREW_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Sandslash] = SANDSHREW_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.NidoranFemale] = NIDORAN_FEMALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Nidorina] = NIDORAN_FEMALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Nidoqueen] = NIDORAN_FEMALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.NidoranMale] = NIDORAN_MALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Nidorino] = NIDORAN_MALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Nidoking] = NIDORAN_MALE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Clefairy] = CLEFAIRY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Clefable] = CLEFAIRY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Vulpix] = VULPIX_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Ninetales] = VULPIX_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Jigglypuff] = JIGGLYPUFF_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Wigglytuff] = JIGGLYPUFF_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Zubat] = ZUBAT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Golbat] = ZUBAT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Oddish] = ODDISH_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Gloom] = ODDISH_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Vileplume] = ODDISH_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Paras] = PARAS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Parasect] = PARAS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Venonat] = VENONAT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Venomoth] = VENONAT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Diglett] = DIGLETT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dugtrio] = DIGLETT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Meowth] = MEOWTH_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Persian] = MEOWTH_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Psyduck] = PSYDUCK_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Golduck] = PSYDUCK_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Mankey] = MANKEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Primeape] = MANKEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Growlithe] = GROWLITHE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Arcanine] = GROWLITHE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Poliwag] = POLIWAG_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Poliwhirl] = POLIWAG_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Poliwrath] = POLIWAG_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Abra] = ABRA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kadabra] = ABRA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Alakazam] = ABRA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Machop] = MACHOP_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Machoke] = MACHOP_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Machamp] = MACHOP_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Bellsprout] = BELLSPROUT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Weepinbell] = BELLSPROUT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Victreebel] = BELLSPROUT_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Tentacool] = TENTACOOL_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Tentacruel] = TENTACOOL_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Geodude] = GEODUDE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Graveler] = GEODUDE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Golem] = GEODUDE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Ponyta] = PONYTA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Rapidash] = PONYTA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Slowpoke] = SLOWPOKE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Slowbro] = SLOWPOKE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Magnemite] = MAGNEMITE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Magneton] = MAGNEMITE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Farfetchd] = FARFETCHD_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Doduo] = DODUO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dodrio] = DODUO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Seel] = SEEL_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dewgong] = SEEL_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Grimer] = GRIMER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Muk] = GRIMER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Shellder] = SHELLDER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Cloyster] = SHELLDER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Gastly] = GASTLY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Haunter] = GASTLY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Gengar] = GASTLY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Onix] = ONIX_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Drowzee] = DROWZEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Hypno] = DROWZEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Krabby] = KRABBY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kingler] = KRABBY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Voltorb] = VOLTORB_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Electrode] = VOLTORB_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Exeggcute] = EXEGGCUTE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Exeggutor] = EXEGGCUTE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Cubone] = CUBONE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Marowak] = CUBONE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Hitmonlee] = HITMONLEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Hitmonchan] = HITMONLEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Lickitung] = LICKITUNG_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Koffing] = KOFFING_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Weezing] = KOFFING_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Rhyhorn] = RHYHORN_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Rhydon] = RHYHORN_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Chansey] = CHANSEY_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Tangela] = TANGELA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kangaskhan] = KANGASKHAN_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Horsea] = HORSEA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Seadra] = HORSEA_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Goldeen] = GOLDEEN_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Seaking] = GOLDEEN_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Staryu] = STARYU_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Starmie] = STARYU_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.MrMime] = MR_MIME_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Scyther] = SCYTHER_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Jynx] = JYNX_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Electabuzz] = ELECTABUZZ_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Magmar] = MAGMAR_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Pinsir] = PINSIR_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Tauros] = TAUROS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Magikarp] = MAGIKARP_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Gyarados] = MAGIKARP_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Lapras] = LAPRAS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Ditto] = DITTO_EVOLUTION;

		    // needs to be handled exceptionally
		    EVOLUTION_INFO[PokemonId.Eevee] = EEVEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Vaporeon] = EEVEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Jolteon] = EEVEE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Flareon] = EEVEE_EVOLUTION;

		    EVOLUTION_INFO[PokemonId.Porygon] = PORYGON_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Omanyte] = OMANYTE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Omastar] = OMANYTE_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kabuto] = KABUTO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Kabutops] = KABUTO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Aerodactyl] = AERODACTYL_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Snorlax] = SNORLAX_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Articuno] = ARTICUNO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Zapdos] = ZAPDOS_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Moltres] = MOLTRES_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dratini] = DRATINI_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dragonair] = DRATINI_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Dragonite] = DRATINI_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Mewtwo] = MEWTWO_EVOLUTION;
		    EVOLUTION_INFO[PokemonId.Mew] = MEW_EVOLUTION;
	    }

	    /**
	     * Get evolution forms
	     *
	     * @param pokemonId pokemon id
	     * @return ordered evolution forms
	     */
	    public static List<EvolutionForm> GetEvolutionForms(PokemonId pokemonId)
        {
            List<EvolutionForm> evolutionForms = new List<EvolutionForm>();
            foreach (PokemonId id in EVOLUTION_INFO[pokemonId])
            {
                evolutionForms.Add(new EvolutionForm(id));
            }
            return evolutionForms;
        }

        /**
         * Tell if a pokemon is fully evolved
         *
         * @param pokemonId pokemon id
         * @return true if a pokemon is fully evolved, false otherwise
         */
        public static bool IsFullyEvolved(PokemonId pokemonId)
        {
            if (EEVEE_FINAL_EVOLUTIONS.Contains(pokemonId))
            {
                return true;
            }
            else
            {
                PokemonId[] info = EVOLUTION_INFO[pokemonId];
                return info[info.Length - 1] == pokemonId;
            }
        }

        /**
         * Get evolution stage number
         *
         * @param pokemonId pokemon id
         * @return 0 based evolution stage number
         */
        public static int GetEvolutionStage(PokemonId pokemonId)
        {
            return EEVEE_FINAL_EVOLUTIONS.Contains(pokemonId)
                    ? 1
                    : new List<PokemonId>(EVOLUTION_INFO[pokemonId]).IndexOf(pokemonId);
        }
    }

}
