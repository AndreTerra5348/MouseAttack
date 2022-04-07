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
        public override bool CanDropData(CommonItem data) =>
            false;

        protected override void OnRightClick()
        {
        }
    }
}
