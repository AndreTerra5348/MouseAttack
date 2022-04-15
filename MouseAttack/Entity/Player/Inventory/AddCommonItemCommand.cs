using MouseAttack.Item.Data;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player.Inventory
{
    public class AddCommonItemCommand : AddItemBaseCommand<CommonItem>
    {
        public AddCommonItemCommand(PlayerInventory inventory) : base(inventory)
        {
        }

        protected override void Execure(CommonItem item) =>
            Inventory.OnItemAdded(item);
    }
}
