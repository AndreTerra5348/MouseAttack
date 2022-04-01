using Godot;
using MouseAttack.Item.Data;

namespace MouseAttack.Equip.Data
{
    public class CommonEquipFactory : CommonItemFactory
    {
        [AssignTo(nameof(CommonEquip.Type))]
        [Export]
        public EquipType Type { get; private set; } = EquipType.Offensive;

        protected override CommonItem GetNewItem() => new CommonEquip();
    }
}
