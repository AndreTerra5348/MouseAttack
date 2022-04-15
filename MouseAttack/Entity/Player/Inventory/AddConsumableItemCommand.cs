using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System.Linq;

namespace MouseAttack.Entity.Player.Inventory
{
    public class AddConsumableItemCommand : AddItemBaseCommand<ConsumableItem>
    {
        public AddConsumableItemCommand(PlayerInventory inventory) : base(inventory)
        {
        }

        protected override void Execure(ConsumableItem item)
        {
            if (item == null)
                return;

            var invItem = Inventory.Consumables.FirstOrDefault(i => i.Name == item.Name);
            if (invItem != null)
            {
                invItem.Count += item.Count;
                return;
            }

            var createdItem = Inventory.CreatedConsumables.FirstOrDefault(i => i.Name == item.Name);

            if (createdItem != null)
            {
                createdItem.Count += item.Count;
                Inventory.OnItemAdded(createdItem);
                return;
            }

            Inventory.CreatedConsumables.Add(item);
            Inventory.OnItemAdded(item);
        }
    }
}
