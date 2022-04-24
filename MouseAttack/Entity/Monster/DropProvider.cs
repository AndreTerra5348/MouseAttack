using System.Collections.Generic;
using System.Linq;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Data;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Monster
{
    public class DropProvider : SharableNode
    {
        IEnumerable<CommonItemFactory> DropList =>
            GetChildren().OfType<CommonItemFactory>();

        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public List<CommonItem> GetDrop(int level)
        {
            List<CommonItem> drops = new List<CommonItem>();
            foreach (CommonItemFactory itemFactory in DropList)
            {
                if (!itemFactory.Dropped)
                    continue;

                var item = itemFactory.CreateItem<CommonItem>(level);
                drops.Add(item);
                PlayerInventory.Add(item);

                if (itemFactory.IsUnique)
                    itemFactory.QueueFree();
            }
            return drops;
        }
    }
}