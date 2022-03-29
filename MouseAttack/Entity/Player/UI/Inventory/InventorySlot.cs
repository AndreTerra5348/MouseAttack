using Godot;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Extensions;

namespace MouseAttack.Entity.Player.UI.Inventory
{
    public class InventorySlot : Slot
    {
        public override bool CanDropData(SlotDragData data) =>
            false;

        protected override void OnRightClick()
        {
            base.OnRightClick();
        }

        // Show comparision with slloted item when item is equip
        public override Control _MakeCustomTooltip(string forText) =>
            base._MakeCustomTooltip(forText);
    }
}
