using MouseAttack.Item.Data;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player.Inventory
{
    public class AddCommonItemCommand : CommandBase
    {
        readonly PlayerInventory _inventory;

        public AddCommonItemCommand(PlayerInventory inventory) =>
            _inventory = inventory;

        public override void Execute(object parameter)
        {
            var item = parameter as CommonItem;
            if (item == null)
                return;

            _inventory.OnItemAdded(item);
        }
    }
}
