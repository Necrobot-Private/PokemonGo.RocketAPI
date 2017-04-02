using Google.Protobuf.Collections;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings.Master;
using System.Collections.Generic;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse.Types;

namespace PokemonGo.RocketAPI.Helpers
{
    public class PokemonMeta
    {
        public static List<ItemTemplate> templates = new List<ItemTemplate>();
        public static Dictionary<PokemonId, PokemonSettings> PokemonSettings = new Dictionary<PokemonId, PokemonSettings>();
        public static Dictionary<PokemonMove, MoveSettings> MoveSettings = new Dictionary<PokemonMove, MoveSettings>();
        public static Dictionary<BadgeType, BadgeSettings> BadgeSettings = new Dictionary<BadgeType, BadgeSettings>();
        public static Dictionary<ItemId, ItemSettings> ItemSettings = new Dictionary<ItemId, ItemSettings>();

        public static GymBattleSettings BattleSettings;
        public static PokemonUpgradeSettings UpgradeSettings;
        
        public static void Update(DownloadItemTemplatesResponse templatesResponse)
        {
            RepeatedField<ItemTemplate> templates = templatesResponse.ItemTemplates;
            PokemonMeta.templates.Clear();
            PokemonMeta.templates.AddRange(templates);
		    foreach (ItemTemplate template in templates)
            {
                if (template.PokemonSettings != null)
                {
                    PokemonSettings pokemonSettings = template.PokemonSettings;
                    PokemonSettings[pokemonSettings.PokemonId] = pokemonSettings;
                }
                else if (template.MoveSettings != null)
                {
                    MoveSettings moveSettings = template.MoveSettings;
                    MoveSettings[moveSettings.MovementId] = moveSettings;
                }
                else if (template.BadgeSettings != null)
                {
                    BadgeSettings badgeSettings = template.BadgeSettings;
                    BadgeSettings[badgeSettings.BadgeType] = badgeSettings;
                }
                else if (template.ItemSettings != null)
                {
                    ItemSettings itemSettings = template.ItemSettings;
                    ItemSettings[itemSettings.ItemId] = itemSettings;
                }
                else if (template.BattleSettings != null)
                {
                    BattleSettings = template.BattleSettings;
                }
                else if (template.PokemonUpgrades != null)
                {
                    UpgradeSettings = template.PokemonUpgrades;
                }
            }
            //Evolutions.initialize(templates);
            PokemonCpUtils.Initialize(templates);
        }

        public static PokemonSettings GetPokemonSettings(PokemonId pokemon)
        {
            return PokemonSettings[pokemon];
        }
        
        public static MoveSettings GetMoveSettings(PokemonMove move)
        {
            return MoveSettings[move];
        }

        public static BadgeSettings GetBadgeSettings(BadgeType badge)
        {
            return BadgeSettings[badge];
        }

        public static ItemSettings GetItemSettings(ItemId item)
        {
            return ItemSettings[item];
        }
    }
}
