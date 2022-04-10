using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System.Linq;

namespace MouseAttack.Entity.Player.Inventory
{
    public class AddConsumableItemCommand : CommandBase
    {
        readonly PlayerInventory _inventory;

        public AddConsumableItemCommand(PlayerInventory inventory) =>
            _inventory = inventory;

        public override void Execute(object parameter)
        {
            var item = parameter as ConsumableItem;
            if (item == null)
                return;

            var invItem = _inventory.Consumables.FirstOrDefault(i => i.Name == item.Name);
            if (invItem == null)
                _inventory.OnItemAdded(item);
            else
                invItem.Count += item.Count;
        }
    }
}
