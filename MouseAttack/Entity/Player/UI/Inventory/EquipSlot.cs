using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Character
{
    public class EquipSlot : Slot
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
                    if(Item != null)
                        UnslotItem();
                    SetItem(e.Item);
                }
                else if (Item == e.Item)                
                    UnsetItem();
            };
        }

        public override bool CanDropData(CommonItem data) =>
            data is CommonEquip && IsType(data as CommonEquip);

        public bool IsType(CommonEquip item) =>
            item.Type == Type;

        protected override void ItemDropped(CommonItem item)
        {
            DuplicationCheck(item);
            SlotItem(item);
        }
    }
}
