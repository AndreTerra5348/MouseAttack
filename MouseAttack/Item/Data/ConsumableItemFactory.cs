using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class ConsumableItemFactory : UsableItemFactory
    {
        [AssignTo(nameof(ConsumableItem.MinBaseValue))]
        [Export]
        public int MinBaseValue { get; private set; } = 1;
        [AssignTo(nameof(ConsumableItem.MaxBaseValue))]
        [Export]
        public int MaxBaseValue { get; private set; } = 3;
        [AssignTo(nameof(ConsumableItem.Count))]
        [Export]
        public int Count { get; private set; } = 0;


        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public override T CreateItem<T>()
        {
            var item = PlayerInventory.CreatedConsumables.FirstOrDefault(i => i.Name == ItemName);

            if (item != null)
                return item as T;

            var createdItem = base.CreateItem<T>();
            PlayerInventory.CreatedConsumables.Add(createdItem as ConsumableItem);
            return createdItem;
        }
    }
}
