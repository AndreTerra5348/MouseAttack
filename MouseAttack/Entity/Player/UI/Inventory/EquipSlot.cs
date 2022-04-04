using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Character
{
    public class EquipSlot : Slot
    {
        [Export]
        EquipType Type = EquipType.Offensive;

        public override bool CanDropData(SlotDragData data) =>
            data?.Item is CommonEquip && data?.GetItem<CommonEquip>().Type == Type;

        protected override void ItemDragged()
        {
            Item.IsSlotted = false;
            RemoveItem();
        }

        protected override void ItemDropped(CommonItem item)
        {
            GetParent()
                .GetChildren()
                .Cast<EquipSlot>()
                .FirstOrDefault(x => x.Item == item)
                ?.RemoveItem();
        }
    }
}
