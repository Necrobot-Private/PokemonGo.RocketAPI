namespace PokemonGo.RocketAPI.Api.SettingsModels
{
    public class InventorySettings
    {
        public int BaseBagItems;
        public int MaxBagItems;
        public int BaseEggs;
        public int BasePokemon;
        public int MaxPokemon;

        public void Update(POGOProtos.Settings.InventorySettings inventorySettings)
        {
            BaseBagItems = inventorySettings.BaseBagItems;
            MaxBagItems = inventorySettings.MaxBagItems;
            BaseEggs = inventorySettings.BaseEggs;
            MaxPokemon = inventorySettings.MaxPokemon;
            BasePokemon = inventorySettings.BasePokemon;
        }
    }

}
