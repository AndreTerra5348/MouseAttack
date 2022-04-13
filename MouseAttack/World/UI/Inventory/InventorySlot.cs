using Godot;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Extensions;
using System.ComponentModel;

namespace MouseAttack.World.UI.Inventory
{
    public class InventorySlot : Slot<CommonItem>
    {
        public override bool CanDropData(CommonItem data) =>
            false;

        protected override void OnItemSet() =>
            Disabled = !IsEmpty && Item.IsSlotted;

        protected override void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CommonItem.IsSlotted))
                return;
            Disabled = Item.IsSlotted;
        }
    }
}
