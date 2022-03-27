using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
