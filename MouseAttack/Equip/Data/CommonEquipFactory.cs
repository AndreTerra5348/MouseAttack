using Godot;
using MouseAttack.Item.Data;
using System;

namespace MouseAttack.Equip.Data
{
    public class CommonEquipFactory : CommonItemFactory
    {
        [AssignTo(nameof(CommonEquip.Type))]
        [Export]
        public EquipType Type { get; private set; } = EquipType.Offensive;

        protected override CommonItem GetNewItem() => new CommonEquip();

        public override T CreateItem<T>(int monsterLevel = 1)
        {
            var item = base.CreateItem<CommonEquip>(monsterLevel);
            item.GenerateStats();
            return item as T;
        }

    }
}
