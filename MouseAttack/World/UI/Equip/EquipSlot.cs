using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Equip
{
    public class EquipSlot : Slot<CommonEquip>
    {
        [Export]
        EquipType Type = EquipType.Offensive;
        PlayerEquip PlayerEquip => TreeSharer.GetNode<PlayerEquip>();
        public override void _Ready()
        {
            base._Ready();

            PlayerEquip.SlotChanged += (s, e) =>
            {
                if (Type != e.Item.Type)
                    return;

                if (e.Item.IsSlotted)
                {
                    DuplicationCheck(e.Item);
                    if (Item != null)
                        UnslotItem();
                    Item = e.Item;
                }
                else if (Item == e.Item)
                    UnsetItem();
            };
        }

        public override bool CanDropData(CommonEquip data) =>
            data != null && IsType(data);

        public bool IsType(CommonEquip item) =>
            item.Type == Type;

        protected override void ItemDropped(CommonEquip item)
        {
            DuplicationCheck(item);
            SlotItem(item);
        }
    }
}
