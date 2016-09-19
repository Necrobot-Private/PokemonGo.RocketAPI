using POGOProtos.Inventory.Item;

namespace PokemonGo.RocketAPI.Api.InventoryModels
{
    public class Item
    {
        private ItemData proto;

        public int Count;

        public Item(ItemData proto)
        {
            this.proto = proto;
            this.Count = proto.Count;
        }

        public ItemId GetItemId()
        {
            return proto.ItemId;
        }

        public bool IsUnseen()
        {
            return proto.Unseen;
        }

        /**
         * Check if the item it's a potion
         *
         * @return true if the item it's a potion
         */
        public bool IsPotion()
        {
            return GetItemId() == ItemId.ItemPotion
                    || GetItemId() == ItemId.ItemSuperPotion
                    || GetItemId() == ItemId.ItemHyperPotion
                    || GetItemId() == ItemId.ItemMaxPotion
                    ;
        }

        /**
         * Check if the item it's a revive
         *
         * @return true if the item it's a revive
         */
        public bool IsRevive()
        {
            return GetItemId() == ItemId.ItemRevive
                    || GetItemId() == ItemId.ItemMaxRevive
                    ;
        }
    }

}
