using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Equip.Data;
using MouseAttack.Item.ActionMenu;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Equip.ActionMenu
{
    public class EquipActionMenu : ItemActionMenu
    {
        const string Unequip = "Unequip";
        const string Equip = "Equip";
        int _itemIndex = 0;
        PlayerEquip PlayerEquip => TreeSharer.GetNode<PlayerEquip>();

        public override void AddAction()
        {
            if (Item.IsSlotted)
                AddAction(Unequip, ToggleEquip);
            else
                AddAction(Equip, ToggleEquip);

            _itemIndex = GetPopup().GetItemCount()-1;
            base.AddAction();
            PlayerEquip.SlotChanged += OnSlotChanged;
        }

        private void OnSlotChanged(object sender, Item.Misc.CommonEquipEventArgs e)
        {
            if (e.Item != Item)
                return;
            UpdateItemText();
        }

        public override void _ExitTree() =>
            PlayerEquip.SlotChanged -= OnSlotChanged;

        private void ToggleEquip()
        {
            var equip = Item as CommonEquip;
            if (!equip.IsSlotted && PlayerEquip.IsTypeEquipped(equip.Type))
                PlayerEquip.Unslot(equip.Type);
            equip.IsSlotted = !equip.IsSlotted;
        }
            

        private void UpdateItemText() =>
            GetPopup().SetItemText(_itemIndex, Item.IsSlotted ? Unequip : Equip);
    }
}
